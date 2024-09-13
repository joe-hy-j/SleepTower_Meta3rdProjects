using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using Photon.Pun;

public class ChatManager : MonoBehaviourPun
{
    // Input Field
    public TMP_InputField inputChat;

    // ChatItem Prefab
    public GameObject chatItemFactory;

    // Content 의 Transform
    public RectTransform trContent;

    // ChatView 의 Transform
    public RectTransform trChatView;

    // 채팅이 추가되기 전의 Content의 H(높이)값을 가지고 있는 변수
    float prevContentH;

    // 닉네임 색상
    //Color nickNameColor;

    // 말풍선
    public GameObject speechBubble;
    // 말풍선 text
    public TMP_Text text_speechBubble;
    // 시간초
    float currentTime = 0;
    // 말풍선 활성화 여부
    bool onBubble = false;


    void Start()
    {
        // 닉네임 색상 랜덤하게 설정
        //nickNameColor = Random.ColorHSV();

        // inputChat 의 내용이 변경될 때 호출되는 함수 등록
        inputChat.onValueChanged.AddListener(OnValueChanged);
        // inputChat 엔터를 쳤을 때 호출되는 함수 등록
        inputChat.onSubmit.AddListener(OnSummit);
        // inputChat 포커싱을 잃을 때 호출되는 함수 등록
        inputChat.onEndEdit.AddListener(OnEndEdit);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 말풍선이 활성화 되어 있을 경우
            if (onBubble)
            {
                currentTime += Time.deltaTime;  // 시간 흐름

                // 시간이 3초를 넘을경우
                if (currentTime >= 3)
                {
                    speechBubble.SetActive(false);  // 말풍선 비활성화
                    onBubble = false;  // 말풍선 비활성화 상태
                    currentTime = 0;  // 시간 초기화
                }
            }
        }
    }

    public void OnSummit(string s)
    {
        // 만약에 s 의 길이가 0 이면 함수를 나가자.
        if (s.Length == 0) return;

        // 채팅 내용을 NickName : 채팅 내용
        // "<color=#ffffff> 원하는 내용 </color>"
        //string nick = "<color=#" + ColorUtility.ToHtmlStringRGB(nickNameColor) + ">" + PhotonNetwork.NickName + "</color>";
        //string chat = nick + " : " + s;

        if (photonView.IsMine)
        {
            currentTime = 0;  // 시간 초기화
            onBubble = true;  // 말풍선 활성화 상태
            speechBubble.SetActive(true);  // 말풍선 활성화
            text_speechBubble.text = s;  // 말풍선 텍스트를 채팅에 입력한 텍스트로 출력
        }

        // AddChat Rpc 함수 호출
        photonView.RPC(nameof(AddChat), RpcTarget.All, s);

        // 강제로 inputChat을 활성화하자
        inputChat.ActivateInputField();
    }

    [PunRPC]
    void AddChat(string chat)
    {
        // 새로운 채팅이 추가되기 전의 Content의 H 값을 저장
        prevContentH = trContent.sizeDelta.y;

        // ChatItem 하나 만들자 (부모를 ChatView 의 Content 로 하자)
        GameObject go = Instantiate(chatItemFactory, trContent);
        // ChatItem 컴포넌트 가져오자.
        ChatItem chatItem = go.GetComponent<ChatItem>();
        // 가져온 컴포넌트의 SetText 함수 실행
        chatItem.SetText(chat);
        // 가져온 컴포넌트의 onAutoScroll 변수에 AutoScrollBottom을 설정
        chatItem.onAutoScroll = AutoScrollBottom;
        // inputChat에 있는 내용 초기화
        inputChat.text = "";
    }

    // 채팅이 추가 되었을 떄 맨밑으로 Content 위치를 옮기는 함수
    public void AutoScrollBottom()
    {
        // chatView의 H보다 content의 H값이 크다면 (스크롤이 가능한 상태라면)
        if (trContent.sizeDelta.y > trChatView.sizeDelta.y)
        {
            //trChatView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

            // 이전 바닥에 닿아있었다면
            if (prevContentH - trChatView.sizeDelta.y <= trContent.anchoredPosition.y)
            {
                // content의 y값을 재설정한다.
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trChatView.sizeDelta.y);
            }
        }
    }

    void OnValueChanged(string s)
    {
        //print("변경 중 : " + s);
    }

    void OnEndEdit(string s)
    {
        //print("작성 끝 : " + s);
    }
}
