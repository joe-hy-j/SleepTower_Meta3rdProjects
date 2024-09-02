using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomManager : MonoBehaviour
{
    public int sleepCount = 0;  // 현재 자는 인원 수
    public int needSleepCount = 1;  // 필요한 총 인원
    public GameObject img_TimerSetting;  // 알람 시간설정 화면
    public GameObject img_TimerUI;  // 알람 화면
    public bool isSleepAll = false;  // 필요한 인원수가 충족 되었는지


    void Start()
    {
        isSleepAll = false;
    }

    void Update()
    {
        // 현재 자는 인원 수가 필요한 총 인원 수에 충족할 경우
        if (sleepCount >= needSleepCount)
        {
            isSleepAll = true;  // 필요한 인원수 충족

            // 현재 자는 인원 카운트가 필요 인원 수를 초과할 경우 한계치 조정
            if (sleepCount > needSleepCount)
            {
                sleepCount = needSleepCount;
            }
        }
        else
        {
            // 현재 자는 인원 카운트가 0 보다 작어질 경우 0 으로 조정
            if (sleepCount < 0)
            {
                sleepCount = 0;
            }
        }

        // 자는 인원이 필요한 인원수에 충족할 경우
        if(isSleepAll)
        {
            sleepCount = 0;
            img_TimerUI.SetActive(true);  // 알람 UI 활성화
        }
    }

    // 알람 시간설정 창 열기 버튼
    public void TimerSetting()
    {
        img_TimerSetting.SetActive(true);
    }

    // 알람 시간설정 창 닫기 버튼
    public void TimerSettingClose()
    {
        img_TimerSetting.SetActive(false);
    }

    // 알람 시간 설정 완료
    public void TimerConfirm()
    {

    }

    // 알람 UI
    public void StopTimer()
    {
        isSleepAll = false;
        img_TimerUI.SetActive(false);  // 알람 UI 비활성화
    }
}
