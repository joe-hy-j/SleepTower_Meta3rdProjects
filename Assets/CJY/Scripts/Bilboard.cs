using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // ĵ������ ȸ���� ī�޶� �������� ����
        transform.forward = Camera.main.transform.forward;
    }
}
