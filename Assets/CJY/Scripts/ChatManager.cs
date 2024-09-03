using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    // Input Field
    public TMP_InputField inputChat;

    // ChatItem Prefab
    public GameObject chatItemFactory;

    // Content �� Transform
    public Transform trContent;


    void Start()
    {
        // inputChat �� ������ ����� �� ȣ��Ǵ� �Լ� ���
        inputChat.onValueChanged.AddListener(OnValueChanged);
        // inputChat ���͸� ���� �� ȣ��Ǵ� �Լ� ���
        inputChat.onSubmit.AddListener(OnSummit);
        // inputChat ��Ŀ���� ���� �� ȣ��Ǵ� �Լ� ���
        inputChat.onEndEdit.AddListener(OnEndEdit);
    }

    void Update()
    {

    }

    void OnSummit(string s)
    {
        // ChatItem �ϳ� ������ (�θ� ChatView �� Content �� ����)
        GameObject go = Instantiate(chatItemFactory, trContent);
        // ChatItem ������Ʈ ��������.
        ChatItem chatItem = go.GetComponent<ChatItem>();
        // ������ ������Ʈ�� SetText �Լ� ����
        chatItem.SetText(s);

        inputChat.text = "";
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
