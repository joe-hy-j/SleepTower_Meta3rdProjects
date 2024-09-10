using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomInfo;

    // 클릭 되었을 때 호출 해줄 함수를 담을 변수
    public Action<string> onChangeRoomName;

    public GameObject imgLock;

    //방 이름
    string realRoomName;

    public void SetContent(string roomName, int currentPlayer, int maxPlayer)
    {
        // roomName을 전역변수에 담기
        realRoomName = roomName;
        // 정보 입력
        roomInfo.text = roomName + "";
    }

    public void SetInfo(string roomName, int currPlayer, int maxPlayer)
    {
        //나의 게임오브젝트 이름을 방이름으로 하자
        realRoomName = roomName;

        // 방 정보를 Text에 설정
        //방 이름 (1/4)
        roomInfo.text = roomName + " ( " + currPlayer + " / " + maxPlayer + " ) ";
    }

    public void SetLockMode(bool isLock)
    {
        imgLock.SetActive(isLock);
    }

    public void OnClick()
    {

        // onChangeRoomName 가 null 이 아니라면 
        if (onChangeRoomName != null)
        {
            onChangeRoomName(realRoomName);

        }

        ////1. InputRoomName 게임오브젝트 찾자
        //GameObject go = GameObject.Find("InputRoomName");
        ////2. 찾은 게임오브젝트에서 InputField 컴포넌트 가져오자
        //InputField inputField = go.GetComponent<InputField>();
        ////3. 가져온 컴포넌트를 이용해서 Text 값 변경
        //inputField.text = name;

    }
}
