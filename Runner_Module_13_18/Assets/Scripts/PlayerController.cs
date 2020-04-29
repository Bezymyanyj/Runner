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
    public float dizzyDuration = 10f;

    public CameraController cameraController;
    public GameObject body;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private AnimationsController anim;
    private Animator animator;
    private BonesController bones;

    private float currentDirection;
    private float currentHeight;
    private float startPossition;
    private float speedIndex = 1f;
    private float speedRunning;

    private bool isMoving = false;
    private bool isDizzy = false;
    private bool isFall = false;
    private bool isGrounded;
    private Vector3 move;


    public float currentDistance { get; private set; }
    public bool IsJumping { get; private set; }
    private void Start()
    {
        anim = GetComponent<AnimationsController>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        bones = GetComponent<BonesController>();
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
        speedRunning = step * speedIndex / anim.RunTimeAnimation;
        float distancePerFrame = speedRunning * Time.deltaTime;
        move = Vector3.forward * distancePerFrame;

        // Движение в стороны
        if (!isMoving && !isFall &&!IsJumping && direction != 0)
        {
            isMoving = true;
            anim.IsAimated = false;
            currentDirection = direction;
            currentDistance = distance;
            startPossition = transform.position.x;
        }
        if (isMoving)
        {
            StartCoroutine(anim.AnimateSidesMovment(currentDirection));
            StartCoroutine(MoveSides());
        }

        // Прыжок
        if (!IsJumping && !isFall &&!isMoving && Input.GetKeyDown(KeyCode.Space))
        {
            anim.IsAimated = false;
            IsJumping = true;
            currentHeight = height;
        }        
        if (IsJumping)
        {
            StartCoroutine(anim.AnimateJump());
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
        if (currentDistance <= 0.1)
        {
            isMoving = false;
            yield break;
        }
        // Вычесляем скорость от анимаций переката
        float speed = distance;
        if (currentDirection < 0)
            speed /= anim.LeftTimeAnimation;
        if(currentDirection > 0)
            speed /= anim.RightTimeAnimation;

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

        if (currentHeight <= jumpStopHeight)
        {
            speedIndex = 1;
            IsJumping = false;
            yield break;
        }
        // float HighOfJump устанавливается кривой в анимации прыжка; 
        // speedIndex снижает скорость движения вперед во время прыжка
        speedIndex = animator.GetFloat("HighOfJump") * slowingRunDuringJump;
        float speed = animator.GetFloat("HighOfJump") * height / anim.JumpTimeAnimation;
        float distancePerFrame = speed * Time.deltaTime;
        controller.Move(Vector3.up * distancePerFrame);
        currentHeight -= distancePerFrame;
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

        StartCoroutine(anim.animateDizzy(dizzyDuration));

        currentDistance = 0;

        float distanceBack = distance - offset;

        controller.Move(Vector3.right * distanceBack * -currentDirection);

        yield return new WaitForSeconds(dizzyDuration);

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
        animator.enabled = false;
        yield return null;
        bones.TurnOnTrigers();
        cameraController.player = body;
        //StartCoroutine(anim.animateFalling());

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Runner");

    }
}
