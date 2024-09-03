using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ChatManager : MonoBehaviour
{
    //inputField
    public TMP_InputField Inputchat;

    public GameObject ChatItemFactory;

    public Transform trContent;

    public RectTransform rtScrollView;

    // Start is called before the first frame update
    void Start()
    {
        // inputChat 의 내용이 변경될 대 호출되는 함수 등록
        Inputchat.onValueChanged.AddListener(OnValuechanged);
        // 엔터키를 누르면 호출되는 함수 등록
        Inputchat.onSubmit.AddListener(OnSubmit);
        // inputChat 포커싱을 잃을 때 호출되는 함수 등록
        Inputchat.onEndEdit.AddListener(OnEndEdit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSubmit(string s)
    {
        // ChatItem 하나 만들자 (ChatView의 content를 부모로하자)
        GameObject go = Instantiate(ChatItemFactory, trContent);

        // 닉네임을 붙여서 채팅내용을 만들자
        string chat = PhotonNetwork.NickName + ":" + s;

        // ChatItem 컴포넌트 가져오기
        ChatItem chatItem = go.GetComponent<ChatItem>();
        // 가져온 컴포넌트의 SetText 함수 실행
        chatItem.SetText(s);
        // inputChat에 있는 내용을 초기화
        Inputchat.text = "";


        print("작성 끝 :" + s);
    }

    void AutoScrollBottom()
    {
        // scrollView 의 Height 보다 content 의 Height 값이 크다면 (스크롤이 가능한 상태라면) 
        //if(trContent.sizeDelta.y > rtScrollView.sizeDelta.y)
        //{
        //// 채팅이 하나도 없었다면

        ////content 의 y 값을 재설정한다.

        //}

    }


    void OnValuechanged(string s)
    {
        print("작성 끝 :" + s);
    }

    void OnEndEdit(string s)
    {
        print("작성 끝 :" + s);
    }

}
