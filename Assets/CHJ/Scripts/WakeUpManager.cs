using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpManager : MonoBehaviour
{
    float currentTime = 0;
    private void Start()
    {
        AlarmManager.instance.offAlarmUI += StartCheckingWakeUp;
    }

    public void StartCheckingWakeUp(object sender, EventArgs e)
    {
        StartCoroutine(CheckWakeUpProcess(10.0f));
    }

    IEnumerator CheckWakeUpProcess(float wakeUpTime)
    {
        while(currentTime < wakeUpTime)
        {
            if (Input.GetMouseButton(0))
            {
                yield break;
            }

            currentTime += Time.deltaTime;

            yield return null;
        }

        // 10초가 지나도 일어나지 않으면...
        // UI를 띄우자.

        //currentTime은 다시 0으로 맞춘다.
        currentTime = 0;
    }


    
}
