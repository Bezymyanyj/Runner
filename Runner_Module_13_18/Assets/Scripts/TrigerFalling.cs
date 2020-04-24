using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerFalling : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            playerController.Falling();
        }
    }
}
