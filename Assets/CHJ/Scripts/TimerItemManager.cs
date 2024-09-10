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
        // ���� enter�� ��������
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ���� hourInputField�� minuteInputField ���� validate�̸� 1. ""�� �ƴ� 2. 0�� 24����, 0�� 60���� ����
            if (CheckValidateInput(hourInputField.text, minuteInputField.text))
            {
                // Timer Item�� �����.
                GameObject go = Instantiate(timerItemFactory, trContent);
                TimerItem timerItem = go.GetComponent<TimerItem>();
                // Timer Item�� SetTime�� �Ѵ�.
                timerItem.SetAlarm(int.Parse(hourInputField.text), int.Parse(minuteInputField.text));
                // Timer Item�� event�� ����Ѵ�.
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
