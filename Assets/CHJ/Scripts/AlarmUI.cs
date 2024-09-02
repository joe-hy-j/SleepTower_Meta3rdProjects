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

    [Header("알람 설정 버튼")]
    public Button alarmSetButton;

    [Header("알람 시간 보여주는 UI")]
    public GameObject alarmUI;

    TextMeshProUGUI alarmText;

    public AlarmManager alarmManager;

    public event EventHandler onAlarmUI;
    public event EventHandler offAlarmUI;

    private void Start()
    {
        alarmUI.SetActive(false);
        try
        {
            alarmText = alarmUI.GetComponentInChildren<TextMeshProUGUI>();
        }
        catch(NullReferenceException e)
        {
            Debug.LogError("alarm UI에 들어온 gameObject는 자식으로 TMP,를 가지고 있어야 합니다.");
            Debug.LogError(e.ToString());
        }

        
    }

    private void Update()
    {
        //알람 UI를 표시합니다.
        if(alarmManager.alarmCount > 0)
        {
            //알람 표시 UI를 표시합니다.
            alarmUI.SetActive(true);
            //첫번째 알람을 표시합니다.
            alarmText.text = alarmManager.GetAlarmByIndex(0).ToString();

            //알람이 울렸는지 체크합니다.
            if (alarmManager.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
            {
                onAlarmUI.Invoke(this, EventArgs.Empty);
            }
            
        }
        else
        {
            //만약 알람이 없으면 아무것도 표시하지 않습니다.
            alarmUI.SetActive(false);
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
    }
    /// <summary>
    /// 알람을 끄는 함수입니다. 버튼의 OnClick을 통해 호출하는 함수입니다
    /// </summary>
    public void DisableAlarm()
    {
        offAlarmUI.Invoke(this, EventArgs.Empty);
    }
}
