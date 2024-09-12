using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionFight : MonoBehaviour
{
    public GameObject cushion;  // 베게 프리팹
    public Transform throwPos;  // 던질 위치
    public Animator anim;
    

    public void CushionThrow()
    {
        anim.SetTrigger("Throw");
        // 베게 프리팹을 던질 위치에서 자신의 회전 값에 맞게 생성한다.
        Instantiate(cushion, throwPos.position, transform.rotation);
    }
}
