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

    private void Start()
    {
        StartCoroutine(alarmOnCheckProcess());
    }

    IEnumerator alarmOnCheckProcess() {
        while (true)
        {
            if (AlarmManager.instance.alarmCount > 0)
            {
                //�˶��� ��ȴ��� üũ�մϴ�.
                if (AlarmManager.instance.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
                {
                    print("�˶��� ��Ƚ��ϴ�");
                    AlarmManager.instance.OnAlarm();
                }
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// ��ư�� ���ؼ� ȣ���ϴ� �Լ��Դϴ�.
    /// </summary>
    public void AddAlarm()
    {
        //���� �ִ� �˶��� ���۴ϴ�.
        AlarmManager.instance.DeleteAllAlarm();
        //alarm manager���� alarm�� �����մϴ�.
        AlarmManager.instance.SetAlarm(Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
        print("�˶��� �߰��Ǿ����ϴ�");
    }
    /// <summary>
    /// �˶��� ���� �Լ��Դϴ�. ��ư�� OnClick�� ���� ȣ���ϴ� �Լ��Դϴ�
    /// </summary>
    public void DisableAlarm()
    {
        AlarmManager.instance.OffAlarm();
    }
}
