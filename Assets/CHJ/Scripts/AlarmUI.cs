using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviour
{
    [Header("시간과 분 Input Field")]
    public TMP_InputField hourInput;
    public TMP_InputField minuteInput;

    public AlarmManager alarmManager;

    public event EventHandler onAlarmUI;
    public event EventHandler offAlarmUI;

    private void Update()
    {
        if(alarmManager.alarmCount > 0)
        {
            //알람이 울렸는지 체크합니다.
            if (alarmManager.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
            {
                if(onAlarmUI != null)
                    onAlarmUI.Invoke(this, EventArgs.Empty);
            }           
        }
    }
    /// <summary>
    /// 버튼을 통해서 호출하는 함수입니다.
    /// </summary>
    public void AddAlarm()
    {
        //원래 있던 알람을 없앱니다.
        alarmManager.DeleteAllAlarm();
        //alarm manager에서 alarm을 설정합니다.
        alarmManager.SetAlarm(Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
        print("알람이 추가되었습니다");
    }
    /// <summary>
    /// 알람을 끄는 함수입니다. 버튼의 OnClick을 통해 호출하는 함수입니다
    /// </summary>
    public void DisableAlarm()
    {
        if (offAlarmUI != null)
        {
            offAlarmUI.Invoke(this, EventArgs.Empty);
        }
    }
}
