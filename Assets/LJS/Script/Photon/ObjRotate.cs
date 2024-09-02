using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    //회전 속력
    public float rotSpeed = 200;
    // 회전 값
    float rotX;
    float rotY;

    // 회전 가능 여부
    public bool useRotX;
    public bool useRotY;
    //photonView
    public PhotonView pv;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine)
        {
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            if(useRotX)
            {
                rotX += my * rotSpeed * Time.deltaTime;
            }
            if(useRotY)
            {
                rotY += mx * rotSpeed * Time.deltaTime;
            }

            rotX = Mathf.Clamp(rotX, -80, 80);

            transform.localEulerAngles = new Vector3(-rotX, rotY, 0);

        }
    }
}
