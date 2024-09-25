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

    // �����ִ� �����ΰ�
    bool isLying = false;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();  // ���̽�ƽ ã�Ƽ� ����
        cc = GetComponent<CharacterController>();  // cc�� �ڽ��� CharacterController ������Ʈ�� �����Ѵ�.
    }

    private void Update()
    {
        if (photonView.IsMine)
        {

            // ���� ���� ��� ���¶��
            if (isLying) return;
            // ���̽�ƽ�� ����/�¿�� �������� ���� ���
            if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            {
                Vector3 dir = transform.forward;  // �ڽ��� ���� ��������

                photonView.RPC(nameof(RpcSetBool), RpcTarget.All,"IsRun",true);

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
                photonView.RPC(nameof(RpcSetBool), RpcTarget.All, "IsRun", false);
            }
        }
    }

    public void SetLying()
    {
        isLying = true;
    }

    public void SetWakeUp()
    {
        isLying = false;
    }

    [PunRPC]
    void RpcSetBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }
}
