using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 5;  // 플레이어 이동속도
    Joystick joystick;  // 조작할 조이스틱
    CharacterController cc;  // CharacterController 컴포넌트 지정변수
    public Animator anim;

    float gravity = -9.8f;  // 중력
    float yVelocity;  // y속력


    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();  // 조이스틱 찾아서 지정
        cc = GetComponent<CharacterController>();  // cc에 자신의 CharacterController 컴포넌트를 지정한다.
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // 조이스틱에 상하/좌우로 움직임이 있을 경우
            if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            {
                Vector3 dir = transform.forward;  // 자신의 앞쪽 방향으로

                anim.SetBool("IsRun", true);

                // 지상에 닿아있을 경우 yVelocity값 초기화
                if (cc.isGrounded)
                {
                    yVelocity = 0;
                }

                yVelocity += gravity * Time.deltaTime;  // 중력구현
                dir.y = yVelocity;  // 현재 방향의 아래쪽으로 중력의 방향 지정

                // CharacterController를 이용한 이동
                cc.Move(dir * moveSpeed * Time.deltaTime);

                // 조이스틱의 상하좌우 이동 값을 자신의 Y축 회전 값에 적용
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0);
            }
            else
            {
                anim.SetBool("IsRun", false);
            }
        }
    }
}
