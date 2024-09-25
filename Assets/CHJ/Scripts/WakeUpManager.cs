using JHJ;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpManager : MonoBehaviourPun
{
    float currentTime = 0;

    // 자고 있는 사람들의 리스트
    List<string> sleepingPlayers = new List<string>();

    // UI 띄울 창
    public WakeUpUI wakeUpUI;

    public static WakeUpManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // 알람이 울리면 체크를 시작하자.
        AlarmManager.instance.onAlarmUI += StartCheckingWakeUp;
    }

    private void CheckSetUI()
    {
        if (sleepingPlayers.Count > 0)
        {
            // UI를 킨다.
            wakeUpUI.SetUI(FindPlayerByNickname(sleepingPlayers[sleepingPlayers.Count - 1]));
        }
        else
        {
            // UI를 끈다.
            wakeUpUI.DisableUI();
        }
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
                print("10초간 버튼 클릭이 있었습니다.");
                yield break;
            }

            currentTime += Time.deltaTime;

            yield return null;
        }
        print("10초간 버튼 클릭이 없었습니다.");
        // 10초가 지나도 일어나지 않으면...
        photonView.RPC(nameof(SetSleepingPlayer), RpcTarget.Others, PhotonNetwork.NickName);


        //currentTime은 다시 0으로 맞춘다.
        currentTime = 0;
    }

    [PunRPC]
    void SetSleepingPlayer(string player)
    {
        print("함수 Set sleeping player가 호출되었습니다: " + player);
        sleepingPlayers.Add(player);
        CheckSetUI();
    }

    Player FindPlayerByNickname(string nickName)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == nickName)
                return player;
        }
        print("player를 찾지 못했습니다.");
        return null;
    }
    
    public void RemoveSleepingPlayerList(string player)
    {
        if(sleepingPlayers.Contains(player))
            sleepingPlayers.Remove(player);
        CheckSetUI();
    }
}
