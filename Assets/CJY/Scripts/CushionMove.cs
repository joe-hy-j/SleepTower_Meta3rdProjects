using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionMove : MonoBehaviour
{
    float speed = 30;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        
    }
}
