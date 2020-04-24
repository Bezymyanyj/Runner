using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerDizzy : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dizzy") && !playerController.IsJumping)
        {
            float offset = playerController.currentDistance;
            StartCoroutine(playerController.Dizzing(offset));
        }
        else if (other.CompareTag("Walls"))
        {
            playerController.Falling();
        }
    }

    
}
