using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;  // 플레이어 이동속도

    JoystickMove joystickMove;  // JoystickMove 스크립트 지정
    CharacterController cc;  // CharacterController 컴포넌트 지정

    float gravity = -9.8f;  // 중력
    float yVelocity;  // y속력


    void Start()
    {
        // 각 스크립트 및 컴포넌트 저장하기
        joystickMove = FindObjectOfType<JoystickMove>();
        cc = GetComponent<CharacterController>();

        StartCoroutine(PlayerMoveRoutine());
    }

    IEnumerator PlayerMoveRoutine()
    {
        while (true)  // 계속
        {
            // JoystickMove에서 반환 받은 horizontal, vetical값을 사용하여 캐릭터를 이동
            Vector3 dir = new Vector3(joystickMove.Horizontal, 0, joystickMove.Vertical);
            
            dir = transform.TransformDirection(dir);  // 월드 기준 방향을 플레이어 기준으로 변경
            dir.Normalize();

            // 지상에 닿아있을 경우 yVelocity값 초기화
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }
            yVelocity += gravity * Time.deltaTime;  // 중력구현
            dir.y = yVelocity;

            cc.Move(dir * moveSpeed * Time.deltaTime);  // CharacterController를 이용한 이동
            yield return null;
        }
    }
}
