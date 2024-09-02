using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : MonoBehaviour
{
    public GameObject btn_sleep;  // 잠들기 버튼
    public GameObject btn_wakeUp;  // 기상 버튼
    public GameRoomManager grm;
    PlayerMove pm;


    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 침대 트리거에 Player가 닿을 경우
        if(other.gameObject.tag == "Player")
        {
            pm = other.GetComponent<PlayerMove>();  // 닿은 대상의 PlayerMove 컴포넌트를 가져온다
            btn_sleep.SetActive(true);  // 잠들기 버튼 활성화
        }
        else
        {
            btn_sleep.SetActive(false);  // 잠들기 버튼 비활성화
        }
    }

    // 잠들기 버튼 기능
    public void Sleeping()
    {
        pm.moveSpeed = 0;  // 플레이어의 움직임을 멈춘다
        grm.sleepCount += 1;  // 현재 자는 인원수 1 증가
        btn_wakeUp.SetActive(true);  // 기상 버튼 활성화
        btn_sleep.SetActive(false);  // 잠들기 버튼 비활성화
    }

    // 기상 버튼 기능
    public void WakeUp()
    {
        grm.sleepCount -= 1;  // 현재 자는 인원수 1 감소
        btn_wakeUp.SetActive(false);  // 기상 버튼 비활성화
        pm.moveSpeed = 5;  // 플레이어 움직임 정상화
    }
}
