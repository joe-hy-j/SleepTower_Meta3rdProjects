using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    

    public int Year
    {
        get; private set;
    }
    public int Month
    {
        get; private set;
    }
    public int Day
    {
        get; private set;
    }
    public int Hour
    {
        get; private set;
    }
    public int Minute
    {
        get; private set;
    }
    public int Second
    {
        get; private set;
    }

    //deltaTime�� ���� ���ϴ� �����Դϴ�.
    public float millisecond = 0;

    public int startHour = 5;
    public int startMinute = 0;
    public int startSecond = 0;

    public float timeScale = 60;

    //�̱������� �����߽��ϴ�. TimeManager.Time���� ������ �� �ֽ��ϴ�.
    public static TimeManager Time;

    private void Awake()
    {
        if (Time == null)
        {
            Time = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //���� ����ð��� �����մϴ�.
        SetCurrentTime();
    }

    void Update()
    {
        millisecond += UnityEngine.Time.deltaTime * timeScale;
        if(millisecond > 1)
        {
            //millisecond�� �������� second�� �ְ� �ʹ�.
            Second += (int)(millisecond / 1);
            millisecond = millisecond%1;
            if(Second >= 60)
            {
                Minute += (int)(Second / 60);
                Second = Second % 60;
                if(Minute >= 60)
                {
                    Hour += (int)(Minute / 60);
                    Minute = Minute % 60;
                    if (Hour >= 24)
                    {
                        Day += (int)(Hour / 24);
                        Hour = Hour % 24;
                    }
                }
            }
        }
    }
    
    //������ �� ���� �ð��� �����մϴ�.
    void SetCurrentTime()
    {
        //��, ��, ���� ���� ����Ϸ� �����մϴ�.
        Year = DateTime.Now.Year;
        Month = DateTime.Now.Month;
        Day = DateTime.Now.Day;

        Hour = startHour;
        Minute = startMinute;
        Second = startSecond;

    }

    void PrintCurrentTime()
    {
        print("year: "+Year + " month: "+Month + " day: "+Day+" Hour: "+Hour+" Minute: "+Minute+" Seconds: " + Second);
    }
}
