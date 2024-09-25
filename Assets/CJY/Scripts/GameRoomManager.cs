using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.EditorTools;
using UnityEngine;


public class GameRoomManager : MonoBehaviourPunCallbacks
{
    public int sleepCount = 0;  // 현재 자는 인원 수
    public int needSleepCount = 1;  // 필요한 총 인원
    public GameObject img_TimerSetting;  // 알람 시간설정 화면
    public GameObject img_TimerUI;  // 알람 화면
    public bool isSleepAll = false;  // 필요한 인원수가 충족 되었는지
    public GameObject img_Chatting;  // 채팅 입력 창
    public GameObject btn_ChatOn;  // 채팅 활성화 버튼
    public GameObject btn_ChatOff;  // 채팅 비활성화 버튼

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
        isSleepAll = false;  // 변수 초기화
        AlarmManager.instance.offAlarmUI += ResetSleepCount;
    }

    void Update()
    {
        // 현재 자는 인원 수가 필요한 총 인원 수에 충족할 경우
        if (sleepCount >= needSleepCount)
        {
            isSleepAll = true;  // 필요한 인원수 충족

            // 현재 자는 인원 카운트가 필요 인원 수를 초과할 경우 한계치 보정
            if (sleepCount > needSleepCount)
            {
                sleepCount = needSleepCount;
            }
        }
        // 현재 자는 인원 수가 0 보다 작아질 경우 0 으로 보정
        else
        {
            isSleepAll = false;
            if (sleepCount < 0)
            {
                sleepCount = 0;
            }
        }

        // 자는 인원이 필요한 인원수에 충족할 경우
        if(isSleepAll )
        {
            if (AlarmManager.instance.alarmCount > 0)
            {
                //sleepCount = 0;
                img_TimerUI.SetActive(true);  // 알람 UI 활성화
            }
            else
            {
                print("알람이 없습니다... 알람을 세팅해주세요");
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

        // 새로운 Player가 들어왔을 때
        // 잠들어야 하는 인원수 올려준다.
        needSleepCount = PhotonNetwork.CurrentRoom.PlayerCount;
        // needSleepCount정보를 보내준다.
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

        // 새로운 Player가 들어왔을 때
        // 잠들어야 하는 인원수 올려준다.
        needSleepCount = PhotonNetwork.CurrentRoom.PlayerCount;
        // needSleepCount정보를 보내준다.
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
    // 알람 시간설정 창 열기 버튼
    public void TimerSetting()
    {
        img_TimerSetting.SetActive(true);
    }

    // 알람 시간설정 창 닫기 버튼
    public void TimerSettingClose()
    {
        img_TimerSetting.SetActive(false);
    }

    // 알람 시간 설정 완료
    public void TimerConfirm()
    {
        
    }

    // 알람 UI
    public void StopTimer()
    {
        ResetSleepCount(this, EventArgs.Empty);
        img_TimerUI.SetActive(false);  // 알람 UI 비활성화
    }

    // 방 나가기 버튼
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

    // 채팅 입력창 활성화 버튼
    public void ChatInputOn()
    {
        img_Chatting.SetActive(true);  // 채팅 입력창 활성화
        btn_ChatOn.SetActive(false);  // 채팅 활성화 버튼 비활성화
        btn_ChatOff.SetActive(true);  // 채팅 비활성화 버튼 활성화
    }

    // 채팅 입력창 비활성화 버튼
    public void ChatInputOff()
    {
        img_Chatting.SetActive(false);  // 채팅 입력창 비활성화
        btn_ChatOn.SetActive(true);  // 채팅 활성화 버튼 활성화
        btn_ChatOff.SetActive(false);  // 채팅 비활성화 버튼 비활성화
    }
}
