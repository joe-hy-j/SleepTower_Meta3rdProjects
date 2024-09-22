using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
{
    public float moveSpeed = 5;  // �÷��̾� �̵��ӵ�
    Joystick joystick;  // ������ ���̽�ƽ
    CharacterController cc;  // CharacterController ������Ʈ ��������
    public Animator anim;

    float gravity = -9.8f;  // �߷�
    float yVelocity;  // y�ӷ�


    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();  // ���̽�ƽ ã�Ƽ� ����
        cc = GetComponent<CharacterController>();  // cc�� �ڽ��� CharacterController ������Ʈ�� �����Ѵ�.
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // ���̽�ƽ�� ����/�¿�� �������� ���� ���
            if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            {
                Vector3 dir = transform.forward;  // �ڽ��� ���� ��������

                anim.SetBool("IsRun", true);

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
            else
            {
                anim.SetBool("IsRun", false);
            }
        }
    }
}
