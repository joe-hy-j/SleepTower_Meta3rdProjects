using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : MonoBehaviour
{
    public GameObject btn_sleep;  // 잠들기 버튼
    public GameObject btn_wakeUp;  // 기상 버튼
    public GameRoomManager grm;  // GameRoomManager 스크립트 저장
    PlayerMove pm;  // PlayerMove 스크립트 가져오기;
    public GameObject player;
    public Transform sleepPos;
    public Transform wakePos;


    private void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        // 침대 트리거에 닿은 대상이 Player일때
        if (other.gameObject.tag == "Player")
        {
            pm = other.GetComponent<PlayerMove>();  // 닿은 대상의 PlayerMove 컴포넌트를 가져온다
            btn_sleep.SetActive(true);  // 잠들기 버튼 활성화
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 침대 트리거에서 벗어난 대상이 Player일때
        if (other.gameObject.tag == "Player")
        {
            btn_sleep.SetActive(false);  // 잠들기 버튼 비활성화
        }
    }

    // 잠들기 버튼 기능
    public void Sleeping()
    {
        grm.sleepCount += 1;  // 현재 자는 인원수 1 증가
        pm.moveSpeed = 0;  // 플레이어의 움직임을 멈춘다

        player.transform.position = sleepPos.position;
        player.transform.rotation = sleepPos.rotation;

        btn_wakeUp.SetActive(true);  // 기상 버튼 활성화
        btn_sleep.SetActive(false);  // 잠들기 버튼 비활성화
    }

    // 기상 버튼 기능
    public void WakeUp()
    {
        grm.sleepCount -= 1;  // 현재 자는 인원수 1 감소

        player.transform.position = wakePos.position;
        player.transform.rotation = wakePos.rotation;

        btn_wakeUp.SetActive(false);  // 기상 버튼 비활성화
        pm.moveSpeed = 5;  // 플레이어 움직임 정상화
    }
}
