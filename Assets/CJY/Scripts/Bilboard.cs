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
        // 캔버스의 회전을 카메라 시점으로 고정
        transform.forward = Camera.main.transform.forward;
    }
}
