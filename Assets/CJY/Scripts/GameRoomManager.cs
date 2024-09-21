using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class GameRoomManager : MonoBehaviourPunCallbacks
{
    public int sleepCount = 0;  // ���� �ڴ� �ο� ��
    public int needSleepCount = 1;  // �ʿ��� �� �ο�
    public GameObject img_TimerSetting;  // �˶� �ð����� ȭ��
    public GameObject img_TimerUI;  // �˶� ȭ��
    public bool isSleepAll = false;  // �ʿ��� �ο����� ���� �Ǿ�����
    public GameObject img_Chatting;  // ä�� �Է� â
    public GameObject btn_ChatOn;  // ä�� Ȱ��ȭ ��ư
    public GameObject btn_ChatOff;  // ä�� ��Ȱ��ȭ ��ư

    public Transform spawnCenter;


    void Start()
    {
        isSleepAll = false;  // ���� �ʱ�ȭ
        PhotonNetwork.Instantiate("Player", spawnCenter.position, Quaternion.identity);
    }

    void Update()
    {
        // ���� �ڴ� �ο� ���� �ʿ��� �� �ο� ���� ������ ���
        if (sleepCount >= needSleepCount)
        {
            isSleepAll = true;  // �ʿ��� �ο��� ����

            // ���� �ڴ� �ο� ī��Ʈ�� �ʿ� �ο� ���� �ʰ��� ��� �Ѱ�ġ ����
            if (sleepCount > needSleepCount)
            {
                sleepCount = needSleepCount;
            }
        }
        // ���� �ڴ� �ο� ���� 0 ���� �۾��� ��� 0 ���� ����
        else
        {
            if (sleepCount < 0)
            {
                sleepCount = 0;
            }
        }

        // �ڴ� �ο��� �ʿ��� �ο����� ������ ���
        if(isSleepAll)
        {
            sleepCount = 0;
            img_TimerUI.SetActive(true);  // �˶� UI Ȱ��ȭ
        }
    }

    // �˶� �ð����� â ���� ��ư
    public void TimerSetting()
    {
        img_TimerSetting.SetActive(true);
    }

    // �˶� �ð����� â �ݱ� ��ư
    public void TimerSettingClose()
    {
        img_TimerSetting.SetActive(false);
    }

    // �˶� �ð� ���� �Ϸ�
    public void TimerConfirm()
    {
        
    }

    // �˶� UI
    public void StopTimer()
    {
        isSleepAll = false;  // ���� ��ħ �� �ƴ�
        img_TimerUI.SetActive(false);  // �˶� UI ��Ȱ��ȭ
    }

    // �� ������ ��ư
    public void ExitRoom()
    {
        //PhotonNetwork.LoadLevel("LobbyScene");
        //SceneManager.LoadScene("LobbyScene");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        //SceneManager.LoadScene("LobbyScene");
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    // ä�� �Է�â Ȱ��ȭ ��ư
    public void ChatInputOn()
    {
        img_Chatting.SetActive(true);  // ä�� �Է�â Ȱ��ȭ
        btn_ChatOn.SetActive(false);  // ä�� Ȱ��ȭ ��ư ��Ȱ��ȭ
        btn_ChatOff.SetActive(true);  // ä�� ��Ȱ��ȭ ��ư Ȱ��ȭ
    }

    // ä�� �Է�â ��Ȱ��ȭ ��ư
    public void ChatInputOff()
    {
        img_Chatting.SetActive(false);  // ä�� �Է�â ��Ȱ��ȭ
        btn_ChatOn.SetActive(true);  // ä�� Ȱ��ȭ ��ư Ȱ��ȭ
        btn_ChatOff.SetActive(false);  // ä�� ��Ȱ��ȭ ��ư ��Ȱ��ȭ
    }
}
