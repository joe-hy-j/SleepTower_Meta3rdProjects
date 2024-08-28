using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;

    JoystickMove joystickMove;
    CharacterController cc;

    // 중력
    float gravity = -9.8f;
    // y 속력
    float yVelocity;


    void Start()
    {
        joystickMove = GameObject.FindObjectOfType<JoystickMove>();
        cc = GetComponent<CharacterController>();

        StartCoroutine(PlayerMoveRoutine());
    }

    private void Update()
    {
        
    }

    IEnumerator PlayerMoveRoutine()
    {
        while (true)
        {
            //JoystickMove에서 반환 받은 horizontal, vetical값을 사용하여 캐릭터를 이동시킵니다.
            Vector3 dir = new Vector3(joystickMove.Horizontal, 0, joystickMove.Vertical);
            dir = transform.TransformDirection(dir);
            dir.Normalize();

            if (cc.isGrounded)
            {
                yVelocity = 0;
            }
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;

            cc.Move(dir * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
