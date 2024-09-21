using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviourPun
{
    // 말풍선
    public GameObject speechBubble;
    // 말풍선 text
    public TMP_Text text_speechBubble;

    // 시간초
    float currentTime = 0;
    // 말풍선 활성화 여부
    bool onBubble = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        // 말풍선이 활성화 되어 있을 경우
        if (onBubble)
        {
            currentTime += Time.deltaTime;  // 시간 흐름

            // 시간이 3초를 넘을경우
            if (currentTime >= 3)
            {
                photonView.RPC(nameof(RpcOffBubble), RpcTarget.All);
                currentTime = 0;  // 시간 초기화
            }
        }
        
    }

    //Input Field에서 호출해주어야 한다.
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
        speechBubble.SetActive(false);  // 말풍선 비활성화
        onBubble = false;  // 말풍선 비활성화 상태
    }
}
