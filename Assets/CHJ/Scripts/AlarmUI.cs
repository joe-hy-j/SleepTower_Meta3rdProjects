using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviour
{
    public TMP_InputField hourInput;
    public TMP_InputField minuteInput;
    public TextMeshProUGUI alarmText;
    public AlarmManager alarmManager;
    
    public void AddAlarm()
    {
        alarmManager.SetAlarm(Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
        Alarm myAlarm = alarmManager.GetAlarmByIndex(alarmManager.alarmCount - 1);
        alarmText.text = myAlarm.ToString();
    }
}
