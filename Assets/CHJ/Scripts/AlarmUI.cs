using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviour
{
    [Header("�ð��� �� Input Field")]
    public TMP_InputField hourInput;
    public TMP_InputField minuteInput;

    public AlarmManager alarmManager;

    public event EventHandler onAlarmUI;
    public event EventHandler offAlarmUI;

    private void Update()
    {
        if(alarmManager.alarmCount > 0)
        {
            //�˶��� ��ȴ��� üũ�մϴ�.
            if (alarmManager.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
            {
                if(onAlarmUI != null)
                    onAlarmUI.Invoke(this, EventArgs.Empty);
            }           
        }
    }
    /// <summary>
    /// ��ư�� ���ؼ� ȣ���ϴ� �Լ��Դϴ�.
    /// </summary>
    public void AddAlarm()
    {
        //���� �ִ� �˶��� ���۴ϴ�.
        alarmManager.DeleteAllAlarm();
        //alarm manager���� alarm�� �����մϴ�.
        alarmManager.SetAlarm(Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
        print("�˶��� �߰��Ǿ����ϴ�");
    }
    /// <summary>
    /// �˶��� ���� �Լ��Դϴ�. ��ư�� OnClick�� ���� ȣ���ϴ� �Լ��Դϴ�
    /// </summary>
    public void DisableAlarm()
    {
        if (offAlarmUI != null)
        {
            offAlarmUI.Invoke(this, EventArgs.Empty);
        }
    }
}
