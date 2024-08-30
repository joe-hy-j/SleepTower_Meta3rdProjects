using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionFight : MonoBehaviour
{
    public GameObject cushion;  // 베게 프리팹
    public Transform throwPos;  // 던질 위치
    

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void CushionThrow()
    {
        // 베게 프리팹을 던질 위치, 자신의 회전 값 에서 생성한다.
        Instantiate(cushion, throwPos.position, transform.rotation);
    }
}
