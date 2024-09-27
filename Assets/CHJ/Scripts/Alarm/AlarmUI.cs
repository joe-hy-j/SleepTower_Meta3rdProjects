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
    [Header("시간과 분 Input Field")]
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
                // 모두 누워있으면
                //알람이 울렸는지 체크합니다.
                if (AlarmManager.instance.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
                {
                    print("알람이 울렸습니다");
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
    /// 버튼을 통해서 호출하는 함수입니다.
    /// </summary>
    public void AddAlarm()
    {
        // 내가 만약 방장이 아니면
        if (!PhotonNetwork.IsMasterClient)
        {
            // UI에 방장만 알람을 설정할 수 있습니다 띄우기
            Debug.LogError("방장만 알람을 설정할 수 있습니다");
            ToastExample.instance.ShowToastMessage("방장만 알람을 설정할 수 있습니다",ToastLength.Short);
            // return;
            return;
        }
        if (hourInput.text != "" && minuteInput.text != "")
        {
            if (gameUI.MiniGameSet())
            {
                photonView.RPC(nameof(RpcAddAlarm), RpcTarget.All, Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
                ToastExample.instance.ShowToastMessage("알람이 설정되었습니다.");
            }
        }
        else
        {
            ToastExample.instance.ShowToastMessage("알람 시간을 설정해주세요");
            Debug.Log("알람 시간을 설정해주세요");
        }
    }

    [PunRPC]
    public void RpcAddAlarm(int hour, int minute)
    {
        //원래 있던 알람을 없앱니다.
        AlarmManager.instance.DeleteAllAlarm();
        //alarm manager에서 alarm을 설정합니다.
        AlarmManager.instance.SetAlarm(hour, minute);
        print("알람이 추가되었습니다");
    }
    /// <summary>
    /// 알람을 끄는 함수입니다. 버튼의 OnClick을 통해 호출하는 함수입니다
    /// </summary>
    public void DisableAlarm()
    {
        // 알람이 울렸으면

        AlarmManager.instance.OffAlarm();
    }

    [PunRPC]
    public void InitAlarm(int hour, int minute)
    {
        print("알람이 초기화되었습니다.");
        AlarmManager.instance.SetAlarm(hour, minute);
    }

    [PunRPC]
    public void InitOnAlarm()
    {
        // 모두 누웠으면

        AlarmManager.instance.OnAlarm();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        //방장만 이 함수를 호출할 수 있습니다.
        if (!photonView.IsMine) return;

        print(" new player entered room!");

        // 알람이 있으면!
        if (AlarmManager.instance.alarmCount > 0)
        {
            photonView.RPC(nameof(InitAlarm), newPlayer, AlarmManager.instance.GetAlarmByIndex(0).hour, AlarmManager.instance.GetAlarmByIndex(0).minute);
        }

        // 만약 알람이 울리는 중에 들어왔으면...
        if(AlarmManager.instance.IsAlarmOn)
        {
            photonView.RPC(nameof(InitOnAlarm), newPlayer);
        }
    }

    
}
