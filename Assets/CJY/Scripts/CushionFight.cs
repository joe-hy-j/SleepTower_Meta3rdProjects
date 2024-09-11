using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionFight : MonoBehaviour
{
    public GameObject cushion;  // ���� ������
    public Transform throwPos;  // ���� ��ġ
    public Animator anim;
    

    public void CushionThrow()
    {
        anim.SetTrigger("Throw");
        // ���� �������� ���� ��ġ���� �ڽ��� ȸ�� ���� �°� �����Ѵ�.
        Instantiate(cushion, throwPos.position, transform.rotation);
    }
}
