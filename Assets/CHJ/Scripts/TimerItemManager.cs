using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerItemManager : MonoBehaviour
{
    public TMP_InputField hourInputField;
    public TMP_InputField minuteInputField;

    public RectTransform trContent;
    public GameObject timerItemFactory;

    private void Update()
    {
        // 만약 enter가 눌렸으면
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 만약 hourInputField와 minuteInputField 값이 validate이면 1. ""이 아님 2. 0과 24사이, 0과 60사이 숫자
            if (CheckValidateInput(hourInputField.text, minuteInputField.text))
            {
                // Timer Item을 만든다.
                GameObject go = Instantiate(timerItemFactory, trContent);
                TimerItem timerItem = go.GetComponent<TimerItem>();
                // Timer Item에 SetTime을 한다.
                timerItem.SetAlarm(int.Parse(hourInputField.text), int.Parse(minuteInputField.text));
                // Timer Item에 event에 등록한다.
                timerItem.setTimeOnInputField += OnTimerItemClick;
            }
        }
    }

    void OnTimerItemClick(int alarmHour, int alarmMinute)
    {
        hourInputField.text = alarmHour.ToString();
        minuteInputField.text = alarmMinute.ToString();
    }

    bool CheckValidateInput(string hourInput, string minuteInput)
    {
        if (hourInput.Length == 0 || minuteInput.Length == 0) return false;

        int hour = int.Parse(hourInput);
        int minute = int.Parse(minuteInput);

        if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60)
            return true;
        else return false;
    }
}
