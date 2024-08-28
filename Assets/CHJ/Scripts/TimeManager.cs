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

    //deltaTime의 합을 구하는 변수입니다.
    public float millisecond = 0;

    public int startHour = 5;
    public int startMinute = 0;
    public int startSecond = 0;

    public float timeScale = 60;

    //싱글톤으로 구현했습니다. TimeManager.Time으로 접근할 수 있습니다.
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
        //현재 가상시간을 설정합니다.
        SetCurrentTime();
    }

    void Update()
    {
        millisecond += UnityEngine.Time.deltaTime * timeScale;
        if(millisecond > 1)
        {
            //millisecond의 정수값을 second에 넣고 싶다.
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
    
    //시작할 때 가상 시간을 설정합니다.
    void SetCurrentTime()
    {
        //년, 월, 일은 현재 년월일로 설정합니다.
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
