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
        return hour +":"+minute;
    }
}

public class AlarmManager : MonoBehaviour
{
    List<Alarm> alarmList = new List<Alarm>();

    public int alarmCount
    {
        get { return alarmList.Count; }
    }

    void Update()
    {
        for(int i = 0; i<alarmCount; i++)
        {
            if(TimeManager.Time.Hour == alarmList[i].hour && TimeManager.Time.Minute == alarmList[i].minute)
            {
                //알람을 울린다
                print("알람이 울렸습니다! : " + alarmList[i].ToString());
            }
        }
    }

    /// <summary>
    /// 알람을 추가합니다.
    /// </summary>
    /// <param name="hour">알람 울리는 시간</param>
    /// <param name="minute">알람 울리는 분</param>
    /// <param name="name">알람 이름</param>
    /// <param name="isActive">알람을 킬지 끌지</param>
    public void SetAlarm(int hour, int minute, string name ="", bool isActive = true) 
    {
        Alarm alarm = new Alarm(hour, minute, name, isActive);
        alarmList.Add(alarm);
    }
    /// <summary>
    /// 알람이름과 같은 알람을 제거합니다.
    /// </summary>
    /// <param name="name">알람 이름</param>
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
    /// 알람 리스트에서 번호에 존재하는 알람을 제거합니다.
    /// </summary>
    /// <param name="alarmIndex">알람 리스트의 순서, 0번부터 alarmCount-1번까지 있습니다.</param>
    public void DeleteAlarm(int alarmIndex)
    {
        alarmList.RemoveAt(alarmIndex);
    }
    /// <summary>
    /// 알람의 인덱스를 사용하여 알람 정보를 받습니다.
    /// </summary>
    /// <param name="index">알람 리스트의 순서, 0번부터 alarmCount-1번까지 있습니다.</param>
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
    /// 이미 존재하는 알람을 수정합니다.
    /// </summary>
    /// <param name="index">알람 리스트의 순서, 0번부터 alarmCount-1번까지 있습니다.</param>
    /// <param name="newHour">바꿀 알람의 시간</param>
    /// <param name="newMinue">바꿀 알람의 분</param>
    /// <param name="newName">알람의 이름</param>
    /// <param name="isActive">알람을 켤지 끌지 결정</param>
    public void ChangeAlarmSetting(int index, int newHour, int newMinue, string newName, bool isActive)
    {
        alarmList[index].hour = newHour;
        alarmList[index].minute = newMinue;
        alarmList[index].name = newName;
        alarmList[index].isActive = isActive;
    }
}
