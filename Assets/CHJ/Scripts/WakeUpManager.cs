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

    // �ڰ� �ִ� ������� ����Ʈ
    List<string> sleepingPlayers = new List<string>();

    // UI ��� â
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
        // �˶��� �︮�� üũ�� ��������.
        AlarmManager.instance.onAlarmUI += StartCheckingWakeUp;
    }

    private void CheckSetUI()
    {
        if (sleepingPlayers.Count > 0)
        {
            // UI�� Ų��.
            wakeUpUI.SetUI(FindPlayerByNickname(sleepingPlayers[sleepingPlayers.Count - 1]));
        }
        else
        {
            // UI�� ����.
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
                print("10�ʰ� ��ư Ŭ���� �־����ϴ�.");
                yield break;
            }

            currentTime += Time.deltaTime;

            yield return null;
        }
        print("10�ʰ� ��ư Ŭ���� �������ϴ�.");
        // 10�ʰ� ������ �Ͼ�� ������...
        photonView.RPC(nameof(SetSleepingPlayer), RpcTarget.Others, PhotonNetwork.NickName);


        //currentTime�� �ٽ� 0���� �����.
        currentTime = 0;
    }

    [PunRPC]
    void SetSleepingPlayer(string player)
    {
        print("�Լ� Set sleeping player�� ȣ��Ǿ����ϴ�: " + player);
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
        print("player�� ã�� ���߽��ϴ�.");
        return null;
    }
    
    public void RemoveSleepingPlayerList(string player)
    {
        if(sleepingPlayers.Contains(player))
            sleepingPlayers.Remove(player);
        CheckSetUI();
    }
}
