using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator animator;
    private ArrayAnimation animations;
    private ArrayParticals arrayParticles;

    public float RunTimeAnimation { get; private set; }
    public float LeftTimeAnimation { get; private set; }
    public float RightTimeAnimation { get; private set; }
    public float JumpTimeAnimation { get; private set; }

    public bool IsAimated { get; set; }
    // Start is called before the first frame update
    void Start()
    {

        arrayParticles = GetComponent<ArrayParticals>();
        animations = GetComponent<ArrayAnimation>();
        animator = GetComponent<Animator>();
        // Определяем время проигрывания анимации
        RunTimeAnimation = animations.RunningTime / animations.runningSpeed;
        LeftTimeAnimation = animations.RunLeftTime / animator.GetFloat("RunningLeft");
        RightTimeAnimation = animations.RunRightTime / animator.GetFloat("RunningRight");
        JumpTimeAnimation = animations.JumpTime / animator.GetFloat("Jumping");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AnimateSidesMovment(float direction)
    {
        if (!IsAimated)
        {
            if (direction < 0)
            {
                animator.SetTrigger("Left");
            }

            if (direction > 0)
            {
                animator.SetTrigger("Right");
            }
            IsAimated = true;
        }
        //Вычисляем скорость анимации
        LeftTimeAnimation = animations.RunLeftTime / animator.GetFloat("RunningLeft");
        RightTimeAnimation = animations.RunRightTime / animator.GetFloat("RunningRight");

        if(direction < 0)
            yield return new WaitForSeconds(LeftTimeAnimation);
        if(direction > 0)
            yield return new WaitForSeconds(RightTimeAnimation);
    }
    public IEnumerator AnimateJump()
    {
        if (!IsAimated)
        {
            animator.SetTrigger("Jump");
            IsAimated = true;
        }
        //Вычисляем скорость анимации
        JumpTimeAnimation = animations.JumpTime / animator.GetFloat("Jumping");
        yield return new WaitForSeconds(JumpTimeAnimation);
    }

    public IEnumerator animateDizzy(float duration)
    {
        animator.SetTrigger("ReturnBack");

        animator.SetLayerWeight(1, 1);

        arrayParticles.particles[1].Play();

        yield return new WaitForSeconds(duration);

        animator.SetLayerWeight(1, 0);

        arrayParticles.particles[1].Stop();
    }

    public IEnumerator animateFalling()
    {
        animator.SetTrigger("Fall");
        animator.SetLayerWeight(1, 0);
        yield return null;
    }
}
