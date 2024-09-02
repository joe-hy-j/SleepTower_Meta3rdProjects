using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;  // �÷��̾� �̵��ӵ�
    Joystick joystick;  // ������ ���̽�ƽ
    CharacterController cc;  // CharacterController ������Ʈ ��������

    float gravity = -9.8f;  // �߷�
    float yVelocity;  // y�ӷ�


    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();  // ���̽�ƽ ã�Ƽ� ����
        cc = GetComponent<CharacterController>();  // cc�� �ڽ��� CharacterController ������Ʈ�� �����Ѵ�.
    }

    private void Update()
    {
        // ���̽�ƽ�� ����/�¿�� �������� ���� ���
        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            Vector3 dir = transform.forward;  // �ڽ��� ���� ��������

            // ���� ������� ��� yVelocity�� �ʱ�ȭ
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            yVelocity += gravity * Time.deltaTime;  // �߷±���
            dir.y = yVelocity;  // ���� ������ �Ʒ������� �߷��� ���� ����

            // CharacterController�� �̿��� �̵�
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // ���̽�ƽ�� �����¿� �̵� ���� �ڽ��� Y�� ȸ�� ���� ����
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0);
        }
    }
}
