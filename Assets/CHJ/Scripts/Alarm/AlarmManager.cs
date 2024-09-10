using JHJ;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm
{
    public int hour;
    public int minute;
    public string name;
    public bool isActive;

    public Alarm(int hour, int minute, string name = "", bool isActive = true)
    {
        this.hour = hour;
        this.minute = minute;
        this.name = name;
        this.isActive = isActive;
    }
    public override string ToString()
    {
        return (hour%12).TimeToPrettyString() +" : "+minute.TimeToPrettyString();
    }
}

public class AlarmManager : MonoBehaviourPun
{
    List<Alarm> alarmList = new List<Alarm>();

    public event EventHandler onAlarmUI;
    public event EventHandler offAlarmUI;

    public static AlarmManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int alarmCount
    {
        get { return alarmList.Count; }
    }

    //public event EventHandler onAlarmCall;

    /// <summary>
    /// �˶��� �߰��մϴ�.
    /// </summary>
    /// <param name="hour">�˶� �︮�� �ð�</param>
    /// <param name="minute">�˶� �︮�� ��</param>
    /// <param name="name">�˶� �̸�</param>
    /// <param name="isActive">�˶��� ų�� ����</param>
    public void SetAlarm(int hour, int minute, string name ="", bool isActive = true) 
    {
        Alarm alarm = new Alarm(hour, minute, name, isActive);
        alarmList.Add(alarm);
    }
    /// <summary>
    /// �˶��̸��� ���� �˶��� �����մϴ�.
    /// </summary>
    /// <param name="name">�˶� �̸�</param>
    public void DeleteAlarm(string name)
    {
        for (int i = 0; i < alarmList.Count; i++)
        {
            if(alarmList[i].name == name)
            {
                alarmList.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// �˶� ����Ʈ���� ��ȣ�� �����ϴ� �˶��� �����մϴ�.
    /// </summary>
    /// <param name="alarmIndex">�˶� ����Ʈ�� ����, 0������ alarmCount-1������ �ֽ��ϴ�.</param>
    public void DeleteAlarm(int alarmIndex)
    {
        alarmList.RemoveAt(alarmIndex);
    }

    public void DeleteAllAlarm()
    {
        for (int i = 0; i < alarmList.Count; i++)
        {
            alarmList.RemoveAt(0);
        }

    }
    /// <summary>
    /// �˶��� �ε����� ����Ͽ� �˶� ������ �޽��ϴ�.
    /// </summary>
    /// <param name="index">�˶� ����Ʈ�� ����, 0������ alarmCount-1������ �ֽ��ϴ�.</param>
    /// <returns></returns>
    public Alarm GetAlarmByIndex(int index) 
    {
        if (index >= alarmList.Count || index < 0) {
            Debug.LogError("Alarm index out of range...");
            return null;
        }
        return alarmList[index];
    }
    /// <summary>
    /// �̹� �����ϴ� �˶��� �����մϴ�.
    /// </summary>
    /// <param name="index">�˶� ����Ʈ�� ����, 0������ alarmCount-1������ �ֽ��ϴ�.</param>
    /// <param name="newHour">�ٲ� �˶��� �ð�</param>
    /// <param name="newMinue">�ٲ� �˶��� ��</param>
    /// <param name="newName">�˶��� �̸�</param>
    /// <param name="isActive">�˶��� ���� ���� ����</param>
    public void ChangeAlarmSetting(int index, int newHour, int newMinue, string newName, bool isActive)
    {
        alarmList[index].hour = newHour;
        alarmList[index].minute = newMinue;
        alarmList[index].name = newName;
        alarmList[index].isActive = isActive;
    }

    public bool CheckAlarmOn(int hour, int minute)
    {
        for (int i = 0; i < alarmCount; i++)
        {
            if (hour == alarmList[i].hour && minute == alarmList[i].minute)
            {
                //onAlarmCall.Invoke(this, EventArgs.Empty);
                return true;
            }
        }
        return false;
    }

    public void OnAlarm()
    {
        if(onAlarmUI != null)
            onAlarmUI.Invoke(this, EventArgs.Empty);
    }
    
    public void OffAlarm()
    {
        if(offAlarmUI != null)  
            offAlarmUI.Invoke(this, EventArgs.Empty);
    }
}
