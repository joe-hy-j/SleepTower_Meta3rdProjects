using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class ConnectSever : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //Photon 환경설정을 기반으로 마스터 서버에 접속을 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 마스터 서버에 접속이 되면 호출되는 함수

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        print("마스터 서버에 접속");

        //로비 접속
        JoinLobby();
    }

    public void JoinLobby()
    {
        // 닉네임 설정
        PhotonNetwork.NickName = "Block_Stand";
        // 기본 Lobby 입장
        PhotonNetwork.JoinLobby();

    }

    //로비에 참여가 성공하면 호출되는 함수
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        JoinOrCreateRoom();
        print("로비 입장");
    }


    // Room 을 참여하자. 만약에 해당 Room이 없으면 Room 만들겠다

    public void JoinOrCreateRoom()
    {
        // 방 생성 옵션
        RoomOptions roomOption = new RoomOptions();
        // 방 최대인원 설정
        roomOption.MaxPlayers = 20;
        // 기본값이 트루이긴한데 혹시몰라서 일단 두개 다 추가함
        roomOption.IsVisible = true;
        roomOption.IsOpen = true;


        // Room 참여 or 생성 -- Room 의 이름!
        PhotonNetwork.JoinOrCreateRoom("SleepTower_Room1", roomOption,TypedLobby.Default);

        
    }

    // 방 생성 성공 했을 때 호출되는 함수

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");
    }

    // 방 입장 성공 했을 때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 입장 완료");

        //멀티플레이 컨텐츠 즐길 수 있는 상태 -- 여기까지가 로비(광장)이라는 룸에 입장한 상태여서 -- 여기를 로비씬으로 만들면 될듯

        //GameScene으로 이동 --- 이러면 방으로 가는것이고

        PhotonNetwork.LoadLevel("GameScene"); // 방으로 가는것 , 아직 덜 구현
    }
}
