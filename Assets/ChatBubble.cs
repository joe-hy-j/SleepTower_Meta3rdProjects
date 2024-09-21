using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviourPun
{
    // ��ǳ��
    public GameObject speechBubble;
    // ��ǳ�� text
    public TMP_Text text_speechBubble;

    // �ð���
    float currentTime = 0;
    // ��ǳ�� Ȱ��ȭ ����
    bool onBubble = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        // ��ǳ���� Ȱ��ȭ �Ǿ� ���� ���
        if (onBubble)
        {
            currentTime += Time.deltaTime;  // �ð� �帧

            // �ð��� 3�ʸ� �������
            if (currentTime >= 3)
            {
                photonView.RPC(nameof(RpcOffBubble), RpcTarget.All);
                currentTime = 0;  // �ð� �ʱ�ȭ
            }
        }
        
    }

    //Input Field���� ȣ�����־�� �Ѵ�.
    public void OnSubmit(string s)
    {
        photonView.RPC(nameof(RpcOnSubmit), RpcTarget.All, s);
    }

    [PunRPC]
    public void RpcOnSubmit(string s)
    {
        onBubble = true;
        speechBubble.SetActive(true);
        text_speechBubble.text = s;
    }

    [PunRPC]
    public void RpcOffBubble()
    {
        speechBubble.SetActive(false);  // ��ǳ�� ��Ȱ��ȭ
        onBubble = false;  // ��ǳ�� ��Ȱ��ȭ ����
    }
}
