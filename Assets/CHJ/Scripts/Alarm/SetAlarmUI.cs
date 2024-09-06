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

    [Header("Alarm Manager")]
    public AlarmManager alarmManager;

    private void Update()
    {
        if (alarmManager.alarmCount > 0)
        {
            //alarm �ð��� �����ϴ� ��ü�� �����ش�.
            alarmUI.SetActive(true);
            Alarm alarm = alarmManager.GetAlarmByIndex(0);
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
            alarmText.text = alarmManager.GetAlarmByIndex(0).ToString();
        }
        else
        {
            //alarm �ð��� �����ϴ� ��ü�� �� ���̰� �Ѵ�.
            alarmUI.SetActive(false);
        }
    }
}
