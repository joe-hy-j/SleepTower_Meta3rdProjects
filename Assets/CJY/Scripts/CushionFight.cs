using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionFight : MonoBehaviourPun
{
    public GameObject cushion;  // ���� ������
    public Transform throwPos;  // ���� ��ġ
    public Animator anim;
    

    public void CushionThrow()
    {
        if (photonView.IsMine)
        {
            anim.SetTrigger("Throw");
            // ���� �������� ���� ��ġ���� �ڽ��� ȸ�� ���� �°� �����Ѵ�.
            Instantiate(cushion, throwPos.position, transform.rotation);
        }
    }
}
