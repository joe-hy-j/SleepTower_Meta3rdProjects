using JHJ;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerItem : MonoBehaviour
{
    int alarmHour;
    int alarmMinute;

    public delegate void SetTimeOnInputField(int hour, int minute);

    public event SetTimeOnInputField setTimeOnInputField;

    public void OnTimeItemClick()
    {
        if (setTimeOnInputField != null)
            setTimeOnInputField.Invoke(alarmHour, alarmMinute);
    }

    public void SetAlarm(int hour, int minute)
    {
        alarmHour = hour;
        alarmMinute = minute;

        TMP_Text itemText = GetComponentInChildren<TMP_Text>();
        //08 시 30 분
        itemText.text = alarmHour.TimeToPrettyString() + " 시 " + alarmMinute.TimeToPrettyString() + " 분 ";
    }
}
