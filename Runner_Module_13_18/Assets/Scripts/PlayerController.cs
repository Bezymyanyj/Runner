using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float distance = 3f;
    public float step = 2f;
    public float height = 1.8f;
    public float gravity = -9.8f;
    public float slowingRunDuringJump = 0.15f;
    public float jumpStopHeight = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    private ArrayAnimation animations;
    private ArrayParticals arrayParticles;
    private Animator animator;
    private CharacterController controller;
    private AnimationsController anim;

    private float runTimeAnimation;
    private float leftTimeAnimation;
    private float rightTimeAnimation;
    private float jumpTimeAnimation;

    private float currentDirection;
    private float currentHeight;
    private float startPossition;
    private float speedIndex = 1f;
    private float speedRunning;
    private float startJump;

    private bool isAimated = false;
    private bool isMoving = false;
    private bool isDizzy = false;
    private bool isFall = false;
    private bool isGrounded;
    private Vector3 move;
    private Coroutine changeParam;


    public float currentDistance { get; private set; }
    public bool IsJumping { get; private set; }
    public bool JumpDetect { get; set; }
    private void Start()
    {
        anim = GetComponent<AnimationsController>();
        animations = GetComponent<ArrayAnimation>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        arrayParticles = GetComponent<ArrayParticals>();
        // Определяем время проигрывания анимации
        runTimeAnimation = animations.RunningTime / animations.runningSpeed;
        leftTimeAnimation = animations.RunLeftTime / animator.GetFloat("RunningLeft");
        rightTimeAnimation = animations.RunRightTime / animator.GetFloat("RunningRight");
        jumpTimeAnimation = animations.JumpTime / animator.GetFloat("Jumping");
    }
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && move.y < 0)
        {
            move.y = 0f;
        }

        // Движение вперед
        speedRunning = step * speedIndex / runTimeAnimation;
        float distancePerFrame = speedRunning * Time.deltaTime;
        move = Vector3.forward * distancePerFrame;

        // Движение в стороны
        if (!isMoving && !isFall &&!IsJumping && direction != 0)
        {
            isMoving = true;
            isAimated = false;
            currentDirection = direction;
            currentDistance = distance;
            startPossition = transform.position.x;
        }
        if (isMoving)
        {
            StartCoroutine(MoveSides());
        }

        // Прыжок
        if (!IsJumping && !isFall &&!isMoving && Input.GetKeyDown(KeyCode.Space))
        {
            isAimated = false;
            IsJumping = true;
            JumpDetect = false;
            currentHeight = height;
        }        
        if (IsJumping)
        {
            StartCoroutine(Jump());
        }
        move.y += gravity * Time.deltaTime;
        controller.Move(move);
    }

    /// <summary>
    /// Движение в стороны
    /// </summary>
    private IEnumerator MoveSides()
    {
        if (!isAimated)
        {
            if (currentDirection > 0)
            {
                animator.SetTrigger("Right");
            }
            if (currentDirection < 0)
            {
                animator.SetTrigger("Left");
            }
            isAimated = true;
        }
        
        if (currentDistance <= 0.1)
        {
            isMoving = false;
            yield break;
        }
        // Вычесляем скорость от анимаций переката
        leftTimeAnimation = animations.RunLeftTime / animator.GetFloat("RunningLeft");
        rightTimeAnimation = animations.RunRightTime / animator.GetFloat("RunningRight");
        float speed = distance;
        if (currentDirection < 0)
            speed /= leftTimeAnimation;
        if(currentDirection > 0)
            speed /= rightTimeAnimation;

        float distancePerFrame = speed * Time.deltaTime;
        move += Vector3.right * currentDirection * distancePerFrame;
        currentDistance -= distancePerFrame;
    }

    /// <summary>
    /// Прыжок
    /// </summary>
    /// <returns></returns>
    private IEnumerator Jump()
    {
        // Расчитываем время от нажатия кнопки прыжка до точки где персонаж начинает взбираться на препятствие
        //float distanceToStart = startJump - transform.position.z;
        //float waitTime = distanceToStart / speedRunning;

        //yield return new WaitForSeconds(waitTime);

        // Возвращаем параметры которые изменили после проигрывания анимации 
        //changeParam = StartCoroutine(ChangeParam());

        // Вызываем анимацию прыжка
        if (!isAimated)
        {
            animator.SetTrigger("Jump");
            isAimated = true;
        }

        if (currentHeight <= jumpStopHeight)
        {
            speedIndex = 1;
            IsJumping = false;
            JumpDetect = false;
            yield break;
        }
        // Вычесляем скорость анимации
        jumpTimeAnimation = animations.JumpTime / animator.GetFloat("Jumping");
        // float HighOfJump устанавливается кривой в анимации прыжка; 
        // speedIndex снижает скорость движения вперед во время прыжка
        speedIndex = animator.GetFloat("HighOfJump") * slowingRunDuringJump;
        float speed = animator.GetFloat("HighOfJump") * height / jumpTimeAnimation;
        float distancePerFrame = speed * Time.deltaTime;
        controller.Move(Vector3.up * distancePerFrame);
        currentHeight -= distancePerFrame;
    }

    /// <summary>
    /// Срабатывает когда игрок попадаетв зону детекции прыжка
    /// </summary>
    /// <param name="startPoint">Точка включения анимации прыжка</param>
    /// <param name="playerDetection">Проверка попал ли игрок в колайдер</param>
    public void JumpDetection(float startPoint, bool playerDetection)
    {
        startJump = startPoint;
        JumpDetect = playerDetection; 
    }
    
    /// <summary>
    /// Включает анимацию дезориентации игрока на 10 секунд и возвращает на дорожку с которой игрок начал движение
    /// Если игрок врежется в препятствие, прежде чем пройдет 10 секунд, ещё раз то он упадет.
    /// </summary>
    /// <param name="offset"> Пройденная дистанция игроком до сталкновения</param>
    /// <returns></returns>
    public IEnumerator Dizzing(float offset)
    {
        if (isDizzy)
        {
            StartCoroutine(Falling());
        }
        isDizzy = true;

        animator.SetTrigger("ReturnBack");

        animator.SetLayerWeight(1, 1);

        arrayParticles.particles[1].Play();

        currentDistance = 0;

        float distanceBack = distance - offset;

        controller.Move(Vector3.right * distanceBack * -currentDirection);

        yield return new WaitForSeconds(10);

        animator.SetLayerWeight(1, 0);

        arrayParticles.particles[1].Stop();

        isDizzy = false;
    }

    /// <summary>
    /// Включает анимацию подения, отключает движение игрока вперед
    /// </summary>
    public IEnumerator Falling()
    {
        RoadsController.isFall = true;
        isFall = true;
        speedIndex = 0f;
        animator.SetTrigger("Fall");
        animator.SetLayerWeight(1, 0);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Runner");

    }
}
