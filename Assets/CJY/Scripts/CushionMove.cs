using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionMove : MonoBehaviour
{
    float speed = 30;  // ���� ������ �ӵ�
    float currentTime;  // ����ð�
    Rigidbody rb;  // RigidBody ������Ʈ ��������


    void Start()
    {
        rb = GetComponent<Rigidbody>();  // rb�� �ڽ��� Rigidbody ������Ʈ�� ����
        rb.velocity = transform.forward * speed;  // �ڽ��� ���� �������� ������ ������ �ٵ� ���� �ӷ¸�ŭ ���� �ش�
        currentTime = 0;  // �ð� �ʱ�ȭ
    }

    void Update()
    {
        currentTime += Time.deltaTime;  // �ð� �帧

        // ���� �ð��� 3 �̻��� ���
        if (currentTime >= 3)
        {
            // ������ ���Ը� �����Ѵ�.
            Destroy(gameObject);
        }
    }
}
