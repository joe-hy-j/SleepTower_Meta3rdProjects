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

    public GameRoomManager roomManager;
    public MiniGameUI gameUI;

    Coroutine alarmCoroutine;

    WaitForSeconds wfs1 = new WaitForSeconds(1);

    private void Start()
    {   
        alarmCoroutine = StartCoroutine(alarmOnCheckProcess());
    }

    IEnumerator alarmOnCheckProcess() {
        while (true)
        {
            if (AlarmManager.instance.alarmCount > 0 && roomManager.isSleepAll)
            {
                // ��� ����������
                //�˶��� ��ȴ��� üũ�մϴ�.
                if (AlarmManager.instance.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
                {
                    print("�˶��� ��Ƚ��ϴ�");
                    AlarmManager.instance.OnAlarm();
                }
                yield return wfs1;
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(alarmCoroutine);
    }
    /// <summary>
    /// ��ư�� ���ؼ� ȣ���ϴ� �Լ��Դϴ�.
    /// </summary>
    public void AddAlarm()
    {
        // ���� ���� ������ �ƴϸ�
        if (!PhotonNetwork.IsMasterClient)
        {
            // UI�� ���常 �˶��� ������ �� �ֽ��ϴ� ����
            Debug.LogError("���常 �˶��� ������ �� �ֽ��ϴ�");
            ToastExample.instance.ShowToastMessage("���常 �˶��� ������ �� �ֽ��ϴ�",ToastLength.Short);
            // return;
            return;
        }
        if (hourInput.text != "" && minuteInput.text != "")
        {
            if (gameUI.MiniGameSet())
            {
                photonView.RPC(nameof(RpcAddAlarm), RpcTarget.All, Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
                ToastExample.instance.ShowToastMessage("�˶��� �����Ǿ����ϴ�.");
            }
        }
        else
        {
            ToastExample.instance.ShowToastMessage("�˶� �ð��� �������ּ���");
            Debug.Log("�˶� �ð��� �������ּ���");
        }
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
        // �˶��� �������

        AlarmManager.instance.OffAlarm();
    }

    [PunRPC]
    public void InitAlarm(int hour, int minute)
    {
        print("�˶��� �ʱ�ȭ�Ǿ����ϴ�.");
        AlarmManager.instance.SetAlarm(hour, minute);
    }

    [PunRPC]
    public void InitOnAlarm()
    {
        // ��� ��������

        AlarmManager.instance.OnAlarm();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        //���常 �� �Լ��� ȣ���� �� �ֽ��ϴ�.
        if (!photonView.IsMine) return;

        print(" new player entered room!");

        // �˶��� ������!
        if (AlarmManager.instance.alarmCount > 0)
        {
            photonView.RPC(nameof(InitAlarm), newPlayer, AlarmManager.instance.GetAlarmByIndex(0).hour, AlarmManager.instance.GetAlarmByIndex(0).minute);
        }

        // ���� �˶��� �︮�� �߿� ��������...
        if(AlarmManager.instance.IsAlarmOn)
        {
            photonView.RPC(nameof(InitOnAlarm), newPlayer);
        }
    }

    
}
