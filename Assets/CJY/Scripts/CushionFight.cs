using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionFight : MonoBehaviour
{
    public GameObject cushion;  // ���� ������
    public Transform throwPos;  // ���� ��ġ
    

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void CushionThrow()
    {
        // ���� �������� ���� ��ġ, �ڽ��� ȸ�� �� ���� �����Ѵ�.
        Instantiate(cushion, throwPos.position, transform.rotation);
    }
}
