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

    // Content �� Transform
    public RectTransform trContent;

    // ChatView �� Transform
    public RectTransform trChatView;

    // ä���� �߰��Ǳ� ���� Content�� H(����)���� ������ �ִ� ����
    float prevContentH;

    // �г��� ����
    //Color nickNameColor;

    // ��ǳ��
    public GameObject speechBubble;
    // ��ǳ�� text
    public TMP_Text text_speechBubble;
    // �ð���
    float currentTime = 0;
    // ��ǳ�� Ȱ��ȭ ����
    bool onBubble = false;


    void Start()
    {
        // �г��� ���� �����ϰ� ����
        //nickNameColor = Random.ColorHSV();

        // inputChat �� ������ ����� �� ȣ��Ǵ� �Լ� ���
        inputChat.onValueChanged.AddListener(OnValueChanged);
        // inputChat ���͸� ���� �� ȣ��Ǵ� �Լ� ���
        inputChat.onSubmit.AddListener(OnSummit);
        // inputChat ��Ŀ���� ���� �� ȣ��Ǵ� �Լ� ���
        inputChat.onEndEdit.AddListener(OnEndEdit);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // ��ǳ���� Ȱ��ȭ �Ǿ� ���� ���
            if (onBubble)
            {
                currentTime += Time.deltaTime;  // �ð� �帧

                // �ð��� 3�ʸ� �������
                if (currentTime >= 3)
                {
                    speechBubble.SetActive(false);  // ��ǳ�� ��Ȱ��ȭ
                    onBubble = false;  // ��ǳ�� ��Ȱ��ȭ ����
                    currentTime = 0;  // �ð� �ʱ�ȭ
                }
            }
        }
    }

    public void OnSummit(string s)
    {
        // ���࿡ s �� ���̰� 0 �̸� �Լ��� ������.
        if (s.Length == 0) return;

        // ä�� ������ NickName : ä�� ����
        // "<color=#ffffff> ���ϴ� ���� </color>"
        //string nick = "<color=#" + ColorUtility.ToHtmlStringRGB(nickNameColor) + ">" + PhotonNetwork.NickName + "</color>";
        //string chat = nick + " : " + s;

        if (photonView.IsMine)
        {
            currentTime = 0;  // �ð� �ʱ�ȭ
            onBubble = true;  // ��ǳ�� Ȱ��ȭ ����
            speechBubble.SetActive(true);  // ��ǳ�� Ȱ��ȭ
            text_speechBubble.text = s;  // ��ǳ�� �ؽ�Ʈ�� ä�ÿ� �Է��� �ؽ�Ʈ�� ���
        }

        // AddChat Rpc �Լ� ȣ��
        photonView.RPC(nameof(AddChat), RpcTarget.All, s);

        // ������ inputChat�� Ȱ��ȭ����
        inputChat.ActivateInputField();
    }

    [PunRPC]
    void AddChat(string chat)
    {
        // ���ο� ä���� �߰��Ǳ� ���� Content�� H ���� ����
        prevContentH = trContent.sizeDelta.y;

        // ChatItem �ϳ� ������ (�θ� ChatView �� Content �� ����)
        GameObject go = Instantiate(chatItemFactory, trContent);
        // ChatItem ������Ʈ ��������.
        ChatItem chatItem = go.GetComponent<ChatItem>();
        // ������ ������Ʈ�� SetText �Լ� ����
        chatItem.SetText(chat);
        // ������ ������Ʈ�� onAutoScroll ������ AutoScrollBottom�� ����
        chatItem.onAutoScroll = AutoScrollBottom;
        // inputChat�� �ִ� ���� �ʱ�ȭ
        inputChat.text = "";
    }

    // ä���� �߰� �Ǿ��� �� �ǹ����� Content ��ġ�� �ű�� �Լ�
    public void AutoScrollBottom()
    {
        // chatView�� H���� content�� H���� ũ�ٸ� (��ũ���� ������ ���¶��)
        if (trContent.sizeDelta.y > trChatView.sizeDelta.y)
        {
            //trChatView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

            // ���� �ٴڿ� ����־��ٸ�
            if (prevContentH - trChatView.sizeDelta.y <= trContent.anchoredPosition.y)
            {
                // content�� y���� �缳���Ѵ�.
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trChatView.sizeDelta.y);
            }
        }
    }

    void OnValueChanged(string s)
    {
        //print("���� �� : " + s);
    }

    void OnEndEdit(string s)
    {
        //print("�ۼ� �� : " + s);
    }
}
