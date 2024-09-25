using Photon.Pun;
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

    bool isSleeping;
    private void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        // 침대 트리거에 닿은 대상이 Player일때
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                SetPlayer(other.gameObject);  // 닿은 대상의 PlayerMove 컴포넌트를 가져온다
                btn_sleep.SetActive(true);  // 잠들기 버튼 활성화
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 침대 트리거에서 벗어난 대상이 Player일때
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                btn_sleep.SetActive(false);  // 잠들기 버튼 비활성화
            }
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
        pm = player.GetComponent<PlayerMove>();
    }

    // 잠들기 버튼 기능
    public void Sleeping()
    {
        grm.ChangeSleepCount(1);  // 현재 자는 인원수 1 증가
        pm.SetLying(); // 못움직이게 만든다.

        player.transform.position = sleepPos.position;
        player.transform.rotation = sleepPos.rotation;

        btn_wakeUp.SetActive(true);  // 기상 버튼 활성화
        btn_sleep.SetActive(false);  // 잠들기 버튼 비활성화

        BedManager.instance.SetSleepBedDicValue(gameObject.name, true);
        isSleeping = true;
    }

    // 기상 버튼 기능
    public void WakeUp()
    {
        grm.ChangeSleepCount(-1);  // 현재 자는 인원수 1 감소

        player.transform.position = wakePos.position;
        player.transform.rotation = wakePos.rotation;

        btn_wakeUp.SetActive(false);  // 기상 버튼 비활성화
        pm.SetWakeUp();

        BedManager.instance.SetSleepBedDicValue(gameObject.name, false);

        isSleeping = false;
    }
}
