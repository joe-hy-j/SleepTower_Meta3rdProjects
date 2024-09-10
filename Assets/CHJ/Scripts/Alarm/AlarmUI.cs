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

    private void Start()
    {   
        StartCoroutine(alarmOnCheckProcess());
    }

    IEnumerator alarmOnCheckProcess() {
        while (true)
        {
            if (AlarmManager.instance.alarmCount > 0)
            {
                //알람이 울렸는지 체크합니다.
                if (AlarmManager.instance.CheckAlarmOn(TimeManager.Time.Hour, TimeManager.Time.Minute))
                {
                    print("알람이 울렸습니다");
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
    /// 버튼을 통해서 호출하는 함수입니다.
    /// </summary>
    public void AddAlarm()
    {
        // 내가 만약 방장이 아니면
        if (!photonView.IsMine)
        {
            // UI에 방장만 알람을 설정할 수 있습니다 띄우기
            Debug.LogError("방장만 알람을 설정할 수 있습니다");
            // return;
            return;
        }
        photonView.RPC(nameof(RpcAddAlarm), RpcTarget.All, Convert.ToInt32(hourInput.text), Convert.ToInt32(minuteInput.text));
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
        AlarmManager.instance.OffAlarm();
    }

    [PunRPC]
    public void InitAlarm(int hour, int minute)
    {
        print("알람이 초기화되었습니다.");
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
