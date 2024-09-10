using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TownMGR : MonoBehaviourPunCallbacks
{
    public GameObject img_Chatting;
    public GameObject btn_ChatOn;
    public GameObject btn_ChatOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 방나가기
    public void ExitTown()
    {
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
