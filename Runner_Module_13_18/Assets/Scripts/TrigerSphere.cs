using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerSphere : MonoBehaviour
{
    public GameObject obstacle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obstacle.SetActive(true);
        }
    }
}
