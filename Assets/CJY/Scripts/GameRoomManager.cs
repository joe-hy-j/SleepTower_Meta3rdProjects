using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.EditorTools;
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
    [HideInInspector]
    public GameObject myPlayer;
    [HideInInspector]
    public EmogeSystem myEmoge;


    private void Awake()
    {
        myPlayer = PhotonNetwork.Instantiate("Player", spawnCenter.position, Quaternion.identity);
        myEmoge = myPlayer.GetComponentInChildren<EmogeSystem>();
    }
    void Start()
    {
        isSleepAll = false;  // ���� �ʱ�ȭ
        AlarmManager.instance.offAlarmUI += ResetSleepCount;
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
            isSleepAll = false;
            if (sleepCount < 0)
            {
                sleepCount = 0;
            }
        }

        // �ڴ� �ο��� �ʿ��� �ο����� ������ ���
        if(isSleepAll )
        {
            if (AlarmManager.instance.alarmCount > 0)
            {
                //sleepCount = 0;
                img_TimerUI.SetActive(true);  // �˶� UI Ȱ��ȭ
            }
            else
            {
                print("�˶��� �����ϴ�... �˶��� �������ּ���");
            }
        }

        print(sleepCount+"/"+needSleepCount);
    }

    public void ResetSleepCount(object sender, EventArgs e)
    {
        sleepCount=0;
        isSleepAll = false;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (!photonView.IsMine) return;

        // ���ο� Player�� ������ ��
        // ����� �ϴ� �ο��� �÷��ش�.
        needSleepCount = PhotonNetwork.CurrentRoom.PlayerCount;
        // needSleepCount������ �����ش�.
        photonView.RPC(nameof(SetSleepCount), RpcTarget.All, needSleepCount, sleepCount, isSleepAll);
        if (isSleepAll)
        {
            photonView.RPC(nameof(SetPlayerSleep), newPlayer);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        if (!photonView.IsMine) return;

        // ���ο� Player�� ������ ��
        // ����� �ϴ� �ο��� �÷��ش�.
        needSleepCount = PhotonNetwork.CurrentRoom.PlayerCount;
        // needSleepCount������ �����ش�.
        photonView.RPC(nameof(SetSleepCount), RpcTarget.All, needSleepCount, sleepCount);
    }
    [PunRPC]
    void SetSleepCount(int needSleepCount, int sleepCount, bool isSleepAll)
    {
        this.needSleepCount = needSleepCount;
        this.sleepCount = sleepCount;
        this.isSleepAll = isSleepAll;
    }
    [PunRPC]
    void SetPlayerSleep()
    {
        GameObject myBed = BedManager.instance.GetEmptyBed();
        if (myBed != null)
        {
            SleepBed sleepBed = myBed.GetComponent<SleepBed>();
            if (sleepBed != null)
            {
                sleepBed.SetPlayer(myPlayer);
                sleepBed.Sleeping();
            }
        }
    }
    public void ChangeSleepCount(int val)
    {
        photonView.RPC(nameof(RpcChangeSleepCount), RpcTarget.All, val);
    }

    [PunRPC]
    void RpcChangeSleepCount(int val)
    {
        sleepCount += val;
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
        ResetSleepCount(this, EventArgs.Empty);
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
