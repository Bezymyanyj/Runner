using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayAnimation : MonoBehaviour
{
    #region Fields
    private float startRunningTime;
    private float runningTime;
    private float runLeftTime;
    private float runRightTime;
    private float jumpTime;
    private float fallingTime;
    private float dizzyTime;
    #endregion
    
    #region Properties
    public float StartRunningTime { get; private set; }
    public float RunningTime { get; private set; }
    public float RunLeftTime { get; private set; }
    public float RunRightTime { get; private set; }
    public float JumpTime { get; private set; }
    public float FallingTime { get; private set; }
    public float DizzyTime { get; private set; }
    #endregion

    public float runningSpeed;
    //public float runLeftSpeed;
    //public float runRightSpeed;
    //public float jumpSpeed;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        GetAnimationLength();

        GetAnimationSpeed();
    }

    private void GetAnimationLength()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Idle To Sprint":
                    StartRunningTime = clip.length;
                    break;
                case "Running":
                    RunningTime = clip.length;
                    break;
                case "Running Left":
                    RunLeftTime = clip.length;
                    break;
                case "Running right":
                    RunRightTime = clip.length;
                    break;
                case "Jumping_2":
                    JumpTime = clip.length;
                    break;
                case "Fall":
                    FallingTime = clip.length;
                    break;
                case "Dizzy idle":
                    DizzyTime = clip.length;
                    break;
            }
        }
    }

    private void GetAnimationSpeed()
    {
        animator.SetFloat("Running", runningSpeed);
        //animator.SetFloat("RunningLeft", runLeftSpeed);
        //animator.SetFloat("RunningRight", runRightSpeed);
        //animator.SetFloat("Jumping", jumpSpeed);
    }
}
