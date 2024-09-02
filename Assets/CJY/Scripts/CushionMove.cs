using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionMove : MonoBehaviour
{
    float speed = 30;  // 베게 던지는 속도
    float currentTime;  // 현재시간
    Rigidbody rb;  // RigidBody 컴포넌트 지정변수


    void Start()
    {
        rb = GetComponent<Rigidbody>();  // rb에 자신의 Rigidbody 컴포넌트를 지정
        rb.velocity = transform.forward * speed;  // 자신의 앞쪽 방향으로 베게의 리지드 바디에 지정 속력만큼 힘을 준다
        currentTime = 0;  // 시간 초기화
    }

    void Update()
    {
        currentTime += Time.deltaTime;  // 시간 흐름

        // 현재 시간이 3 이상일 경우
        if (currentTime >= 3)
        {
            // 생성된 베게를 제거한다.
            Destroy(gameObject);
        }
    }
}
