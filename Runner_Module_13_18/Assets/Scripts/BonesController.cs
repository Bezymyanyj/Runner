using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesController : MonoBehaviour
{
    public Collider[] colliders;



    public void TurnOnTrigers()
    {
        for (int i = 0; i < colliders.Length - 1; i++)
        {
            colliders[i].isTrigger = false;
        }
    }

}
