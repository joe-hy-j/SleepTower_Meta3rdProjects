using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviourPunCallbacks
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
        // ���� ���� ������ �ƴϸ�
        if (!photonView.IsMine)
        {
            // UI�� ���常 �˶��� ������ �� �ֽ��ϴ� ����
            Debug.LogError("���常 �˶��� ������ �� �ֽ��ϴ�");
            // return;
            return;
        }
        photonView.RPC(nameof(RpcAddAlarm), RpcTarget.All, Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
    }

    [PunRPC]
    public void RpcAddAlarm(int hour, int minute)
    {
        //���� �ִ� �˶��� ���۴ϴ�.
        AlarmManager.instance.DeleteAllAlarm();
        //alarm manager���� alarm�� �����մϴ�.
        AlarmManager.instance.SetAlarm(hour, minute);
        print("�˶��� �߰��Ǿ����ϴ�");
    }
    /// <summary>
    /// �˶��� ���� �Լ��Դϴ�. ��ư�� OnClick�� ���� ȣ���ϴ� �Լ��Դϴ�
    /// </summary>
    public void DisableAlarm()
    {
        AlarmManager.instance.OffAlarm();
    }

    [PunRPC]
    public void InitAlarm(int hour, int minute)
    {
        print("�˶��� �ʱ�ȭ�Ǿ����ϴ�.");
        AlarmManager.instance.SetAlarm(hour, minute);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (!photonView.IsMine) return;
        print(" new player entered room!");

        photonView.RPC(nameof(InitAlarm), newPlayer, AlarmManager.instance.GetAlarmByIndex(0).hour, AlarmManager.instance.GetAlarmByIndex(0).minute);
    }

    
}
