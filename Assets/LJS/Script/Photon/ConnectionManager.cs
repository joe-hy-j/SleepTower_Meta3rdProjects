﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    // NickName InputField
    public TMP_InputField inputNickName;

    // Connect Button
    public Button btnConnect;


    // Start is called before the first frame update
    void Start()
    {
        // inputNickName 의 내용이 변경될 떄 호출되는 함수 등록

        inputNickName.onValueChanged.AddListener(OnValueChanged);

        // inputNickName 에서 엔터 쳤을 때 호출되는 함수 등록
        inputNickName.onSubmit.AddListener((string s) =>
        {

            //버튼이 활성화 되어있다면 
            if (btnConnect.interactable)
            {
                // OnClickConnect 호출
                OnClickConnect();

            }
        });

        // 버튼 비활성
        btnConnect.interactable = false;

    }

    void OnValueChanged(string s)
    {
        //만약에 s 의 길이가 0보다 크면 
        if (s.Length > 0)
        {
            //접속 버튼을 활성화 
            btnConnect.interactable = true;
        }
        //그렇지 않으면 (s의 길이가 0)
        else
        {
            //접속을 비활성화
            btnConnect.interactable = false;
        }
    }

    public void OnClickConnect()
    {
        // Photon 서버 접속 요청
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // 닉네임 설정

        PhotonNetwork.NickName = inputNickName.text;

        // 특정 Lobby 정보 셋팅 -- = new 타입로비(채널이름, 기본값)
        TypedLobby typedLobby = new TypedLobby("Block Lobby", LobbyType.Default);

        //로비 진입 요청
        PhotonNetwork.JoinLobby(typedLobby);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        // 로비 씬으로 이동


    }
}
