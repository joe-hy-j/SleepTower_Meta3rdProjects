using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float moveSpeed = 5;
    Joystick joystick;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();

    }

    private void Update()
    {
        if(joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0);
        }
    }
}
