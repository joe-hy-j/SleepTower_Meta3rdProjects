using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;

    JoystickMove joystickMove;
    CharacterController cc;

    // �߷�
    float gravity = -9.8f;
    // y �ӷ�
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
            //JoystickMove���� ��ȯ ���� horizontal, vetical���� ����Ͽ� ĳ���͸� �̵���ŵ�ϴ�.
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
