using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmTextShow : MonoBehaviour
{
    public Text text;
    public AlarmUI alarmManager;
    void Start()
    {
        alarmManager.onAlarmUI += ShowText;
        alarmManager.offAlarmUI += HideText;
    }

    void ShowText(object sender, EventArgs e)
    {
        text.text = "알람이 울렸습니다";
    }

    void HideText(object sender, EventArgs e)
    {
        text.text = "";
    }
}
