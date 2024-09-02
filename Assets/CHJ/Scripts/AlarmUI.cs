using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviour
{
    [Header("�ð��� �� Input Field")]
    public TMP_InputField hourInput;
    public TMP_InputField minuteInput;

    [Header("�˶� ���� ��ư")]
    public Button alarmSetButton;

    [Header("�˶� �ð� �����ִ� UI")]
    public GameObject alarmUI;

    TextMeshProUGUI alarmText;

    public AlarmManager alarmManager;

    public event EventHandler onAlarmUI;
    public event EventHandler offAlarmUI;

    private void Start()
    {
        alarmUI.SetActive(false);
        try
        {
            alarmText = alarmUI.GetComponentInChildren<TextMeshProUGUI>();
        }
        catch(NullReferenceException e)
        {
            Debug.LogError("alarm UI�� ���� gameObject�� �ڽ����� TMP,�� ������ �־�� �մϴ�.");
            Debug.LogError(e.ToString());
        }

        
    }

    private void Update()
    {
        //�˶� UI�� ǥ���մϴ�.
        if(alarmManager.alarmCount > 0)
        {
            //�˶� ǥ�� UI�� ǥ���մϴ�.
            alarmUI.SetActive(true);
            //ù��° �˶��� ǥ���մϴ�.
            alarmText.text = alarmManager.GetAlarmByIndex(0).ToString();

            //�˶��� ��ȴ��� üũ�մϴ�.
            if (alarmManager.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
            {
                onAlarmUI.Invoke(this, EventArgs.Empty);
            }
            
        }
        else
        {
            //���� �˶��� ������ �ƹ��͵� ǥ������ �ʽ��ϴ�.
            alarmUI.SetActive(false);
        }
    }
    /// <summary>
    /// ��ư�� ���ؼ� ȣ���ϴ� �Լ��Դϴ�.
    /// </summary>
    public void AddAlarm()
    {
        //���� �ִ� �˶��� ���۴ϴ�.
        alarmManager.DeleteAllAlarm();
        //alarm manager���� alarm�� �����մϴ�.
        alarmManager.SetAlarm(Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
    }
    /// <summary>
    /// �˶��� ���� �Լ��Դϴ�. ��ư�� OnClick�� ���� ȣ���ϴ� �Լ��Դϴ�
    /// </summary>
    public void DisableAlarm()
    {
        offAlarmUI.Invoke(this, EventArgs.Empty);
    }
}
