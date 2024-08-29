using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;  // �÷��̾� �̵��ӵ�

    JoystickMove joystickMove;  // JoystickMove ��ũ��Ʈ ����
    CharacterController cc;  // CharacterController ������Ʈ ����

    float gravity = -9.8f;  // �߷�
    float yVelocity;  // y�ӷ�


    void Start()
    {
        // �� ��ũ��Ʈ �� ������Ʈ �����ϱ�
        joystickMove = FindObjectOfType<JoystickMove>();
        cc = GetComponent<CharacterController>();

        StartCoroutine(PlayerMoveRoutine());
    }

    IEnumerator PlayerMoveRoutine()
    {
        while (true)  // ���
        {
            // JoystickMove���� ��ȯ ���� horizontal, vetical���� ����Ͽ� ĳ���͸� �̵�
            Vector3 dir = new Vector3(joystickMove.Horizontal, 0, joystickMove.Vertical);
            
            dir = transform.TransformDirection(dir);  // ���� ���� ������ �÷��̾� �������� ����
            dir.Normalize();

            // ���� ������� ��� yVelocity�� �ʱ�ȭ
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }
            yVelocity += gravity * Time.deltaTime;  // �߷±���
            dir.y = yVelocity;

            cc.Move(dir * moveSpeed * Time.deltaTime);  // CharacterController�� �̿��� �̵�
            yield return null;
        }
    }
}
