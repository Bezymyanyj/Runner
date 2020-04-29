using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMoving : MonoBehaviour
{
    public float speed = 1f;

    private bool isWorking = false;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > 0.2)
        {
            transform.position -= new Vector3(0, 0, 0);
        }

        rb.MovePosition(transform.position + transform.up *speed * Time.fixedDeltaTime);
    }
}
