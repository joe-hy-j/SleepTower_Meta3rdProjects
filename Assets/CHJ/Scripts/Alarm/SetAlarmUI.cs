using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetAlarmUI : MonoBehaviour
{
    [Header("알람 시간 보여주는 UI")]
    public GameObject alarmUI;

    [Header("알람 시간을 보여주는 Text")]
    public Text daynightText;
    public Text alarmText;

    [Header("알람 중지 버튼")]
    public GameObject alarmStopBtn;

    private void Start()
    {
        alarmStopBtn.SetActive(false);
        AlarmManager.instance.onAlarmUI += ShowAlarmStopBtn;
        AlarmManager.instance.offAlarmUI += HideAlarmStopBtn;
    }

    public void ShowAlarmStopBtn(object sender, EventArgs e)
    {
        alarmStopBtn.SetActive(true);
    }
    public void HideAlarmStopBtn(object sender, EventArgs e)
    {
        alarmStopBtn.SetActive(false);
    }
    private void Update()
    {
        if (AlarmManager.instance.alarmCount > 0)
        {
            //alarm 시간을 포함하는 객체를 보여준다.
            alarmUI.SetActive(true);
            Alarm alarm = AlarmManager.instance.GetAlarmByIndex(0);
            //alarm의 오전 오후를 보여준다
            if (alarm.hour > 12)
            {
                daynightText.text = "오후";
            }
            else
            {
                daynightText.text = "오전";
            }
            //alarm 시간을 보여준다.
            alarmText.text = AlarmManager.instance.GetAlarmByIndex(0).ToString();
        }
        else
        {
            //alarm 시간을 포함하는 객체를 안 보이게 한다.
            alarmUI.SetActive(false);
        }
    }
}
