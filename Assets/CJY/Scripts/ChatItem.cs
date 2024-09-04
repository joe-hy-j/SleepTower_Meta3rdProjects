using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;


public class ChatItem : MonoBehaviour
{
    // Text
    public TMP_Text chatText;

    // �Ű����� �Լ� ���� ����
    public Action onAutoScroll;


    private void Awake()
    {
        // Text ������Ʈ ��������
        chatText = GetComponent<TMP_Text>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetText(string s)
    {
        // �ؽ�Ʈ ����
        chatText.text = s;

        // ������ ���� �ڸ�ƾ ����
        StartCoroutine(UpdateSize());
    }

    IEnumerator UpdateSize()
    {
        yield return null;

        // �ؽ�Ʈ�� ���뿡 ���缭 ũ�⸦ ����
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);

        yield return null;

        // ���࿡ onAutoScroll�� �Լ��� ����ִٸ�
        if (onAutoScroll != null)
        {
            onAutoScroll();
        }
    }
}
