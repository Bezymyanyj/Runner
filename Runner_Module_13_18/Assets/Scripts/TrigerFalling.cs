using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerFalling : MonoBehaviour
{
    private PlayerController playerController;
    private AudioSource fun;
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        fun = GetComponent<AudioSource>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            fun.Play();
            StartCoroutine(playerController.Falling());
        }
    }
}
