using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    //Input Room Name
    public TMP_InputField inputRoomName;
    //Input Max Player
    public TMP_InputField inputMaxPlayer;
    //방 참여 버튼
    public Button btnJoinRoom;
    //방 생성 버튼
    public Button btnCreateRoom;


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
        btnCreateRoom.interactable= roomName.Length > 0 && inputMaxPlayer.text.Length > 0;
    }

    //생성 버튼에 관여
    void OnValueChagedMaxPlayer(string max)
    {
        btnCreateRoom.interactable = max.Length > 0 && inputRoomName.text.Length > 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
