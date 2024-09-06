using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    //Input Room Name
    public TMP_InputField inputRoomName;
    //Input Max Player
    public TMP_InputField inputMaxPlayer;
    //방 참여 버튼
    public Button btnJoinRoom;
    //방 생성 버튼
    public Button btnCreateRoom;

    // RoomItem Prefab
    public GameObject roomItemFactory;
    // RoomListView -> Content -> RectTransform
    public RectTransform rtContent;

    // 방 정보 가지고 있는 DIctionary
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();


    // Start is called before the first frame update
    void Start()
    {
        // 방 참여, 생성 비활성화
        btnJoinRoom.interactable = false;
        btnCreateRoom.interactable = false;
        // inputRoomName 의 내용이 변경될 떄 호출되는 함수
        inputRoomName.onValueChanged.AddListener(OnValueChangedRoomName);
        // inputMaxPlayer의 내용이 변경될 떄 호출되는 함수
        inputMaxPlayer.onValueChanged.AddListener(OnValueChagedMaxPlayer);
    }

    // 참여 & 생성 버튼에 관여
    void OnValueChangedRoomName(string roomName)
    {
        //참여 버튼 활성/ 비활성화
        btnJoinRoom.interactable = roomName.Length > 0;
        //생성 버튼 활성/ 비활성화
        btnCreateRoom.interactable = roomName.Length > 0 && inputMaxPlayer.text.Length > 0;
    }

    //생성 버튼에 관여
    void OnValueChagedMaxPlayer(string max)
    {
        btnCreateRoom.interactable = max.Length > 0 && inputRoomName.text.Length > 0;
    }

    public void CreateRoom()
    {
        // 방 옵션을 설정 (최대 인원)
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = int.Parse(inputMaxPlayer.text);
        // 방 목록에 보이게 하냐? 안하냐? 
        options.IsVisible = true; // false일때 비밀 방 검색해야 나온는 그런 거
        // 방 참여가능? 불가능?
        options.IsOpen = true; // 게임 중 참여불가능하게 하는거 true 일때

        // 특정 로비에 방 생성 요청
        //TypedLobby typedLobby = new TypedLobby("Block_Stand",LobbyType.Default);
        //PhotonNetwork.CreateRoom(inputRoomName.text, options,typedLobby);

        //기본 로비에 방 생성 요청
        PhotonNetwork.CreateRoom(inputRoomName.text, options);

    }

    // 방 생성 완료시 호출되는  함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");

    }

    // 방 생성 실패시 호출되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("방 생성 실패:" + message);
    }

    //
    public void JoinRoom()
    {
        // 방 입장 요청
        PhotonNetwork.JoinRoom(inputRoomName.text);

    }

    //방 입장 완료시 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 입장 완료");

        PhotonNetwork.LoadLevel("MainScene"); 
        
    }

    public void onJoinedTown()
    {
        base.OnJoinedRoom();
        print("Town 입장 완료");

        //GameScene으로 이동 -- 광장 가는 버튼에 클릭이벤트 적용
        PhotonNetwork.LoadLevel("TownScene");
    }

    // 방 입장 실패시 호출되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("방 입장 실패 : " + message);
    }

    void RemoveRoomList()
    {
        // rtContent 에 있는 자식 GameObject 를 모두 삭제
        for(int i =0; i < rtContent.childCount; i++)
        {
            Destroy(rtContent.GetChild(i).gameObject);
        }

    }
    void UpadateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            // roomCache 에 info의 방이름 으로 되어있는 Key 값 존재여부
            if (roomCache.ContainsKey(roomInfo.Name))
            {
                //삭제 해아하니?
                if(roomInfo.RemovedFromList)
                {
                    roomCache.Remove(roomInfo.Name);
                }
                //수정 
                else
                {
                    roomCache[roomInfo.Name] = roomInfo;
                }

            }
            else
            {
                // 추가
                roomCache[roomInfo.Name] = roomInfo;
            }
        }
    }
    void CreateRoomList()
    {
        foreach (RoomInfo info in roomCache.Values)
        {
            //roomItem prefav 을 이용해서 roomItem 을 만든다. -- 만듬과 동시에 부모설정 UI 크기때문
            GameObject goRoomItem = Instantiate(roomItemFactory, rtContent);
            print("방생성"); // 여기 제대로 안먹히고
            //만들어진 roomItem 의 부모를 ScrollView -> Content 의 transform 으로 한다.
            // goRoomItem.transform.parent = rtContent;
            // 만들어진 roomItem 에서 RoomItem 컴포넌트 가져온다 (스크립트컴포넌트가져온다는이야기)
            RoomItem roomItem = goRoomItem.GetComponent<RoomItem>();
            //가져온 컴포넌트가 가지고 있는 SetInfo를 함수 실행
            roomItem.SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
            //RoomItem 이 클릭 되었을 때 호출되는 함수 등록
            //roomItem.onChangeRoomName = OnChangeRoomName;

            //람다식 무명함수
            roomItem.onChangeRoomName = (string roomName) =>
            {
                inputRoomName.text = roomName;
            };
        }
    }

    // 누군가 방을 만들거나 수정했을때 호출되는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        // 전체 룸리스트 UI 삭제
        RemoveRoomList();
        //내가 따로 관리하는 룸 리스트 정보 갱신
        UpadateRoomList(roomList);
        //룸 리스트 정보를 가지고 UI를 다시 생성
        CreateRoomList();
        
    }

    public void OnChangeRoomName(string roomName)
    {
        inputRoomName.text = roomName;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }
}
