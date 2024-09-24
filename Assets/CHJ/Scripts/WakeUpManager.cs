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

        // 10�ʰ� ������ �Ͼ�� ������...
        // UI�� �����.

        //currentTime�� �ٽ� 0���� �����.
        currentTime = 0;
    }


    
}
