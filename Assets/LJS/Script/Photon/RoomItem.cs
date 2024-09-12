using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    // 내용 담는 Text
    public TMP_Text roomInfo;
    // 잠금 표시 image
    public GameObject imgLock;

    //방 이름
    string realRoomName;

    // 클릭 되었을 때 호출 해줄 함수를 담을 변수
    public Action<string> onChangeRoomName;
    public void SetContent(string roomName, int currentPlayer, int maxPlayer)
    {
        // roomName을 전역변수에 담기
        realRoomName = roomName;
        // 정보 입력
        roomInfo.text = roomName + " ( " + currentPlayer + " / " + maxPlayer + " ) ";
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


    }
}
