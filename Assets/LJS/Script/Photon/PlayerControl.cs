using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviourPun
{
    CharacterController cc;

    float gravity = -9.81f;

    float yVelocity;

    float jumpPower = 3;




    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        if(cc.isGrounded)
        {
            yVelocity = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space))
            {
            yVelocity = jumpPower;
        }

        yVelocity += gravity * Time.deltaTime;
    }
}
