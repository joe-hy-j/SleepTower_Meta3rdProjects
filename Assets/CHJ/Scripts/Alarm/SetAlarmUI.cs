using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetAlarmUI : MonoBehaviour
{
    [Header("�˶� �ð� �����ִ� UI")]
    public GameObject alarmUI;

    [Header("�˶� �ð��� �����ִ� Text")]
    public Text daynightText;
    public Text alarmText;

    private void Update()
    {
        if (AlarmManager.instance.alarmCount > 0)
        {
            //alarm �ð��� �����ϴ� ��ü�� �����ش�.
            alarmUI.SetActive(true);
            Alarm alarm = AlarmManager.instance.GetAlarmByIndex(0);
            //alarm�� ���� ���ĸ� �����ش�
            if (alarm.hour > 12)
            {
                daynightText.text = "����";
            }
            else
            {
                daynightText.text = "����";
            }
            //alarm �ð��� �����ش�.
            alarmText.text = AlarmManager.instance.GetAlarmByIndex(0).ToString();
        }
        else
        {
            //alarm �ð��� �����ϴ� ��ü�� �� ���̰� �Ѵ�.
            alarmUI.SetActive(false);
        }
    }
}
