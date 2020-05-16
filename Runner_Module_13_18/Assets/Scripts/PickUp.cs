using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<ArrayParticals>().particles[0].Play();
            Repository.Instance.CountBalls++;
            UIController.Instance.UpdateBallText();
            Destroy(gameObject);
        }
        
    }
}
