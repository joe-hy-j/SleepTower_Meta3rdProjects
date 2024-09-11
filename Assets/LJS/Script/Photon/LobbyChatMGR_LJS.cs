using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyChatMGR_LJS : MonoBehaviour, IChatClientListener
{
    // 채팅을 총괄하는 객체
    ChatClient ChatClient;

    // 채팅 입력 UI
    public TMP_InputField inputChat;

    // 채팅 채널
    string currentChannel = "Block_Stand";

    // 스크롤 뷰의 Content
    public RectTransform trContent;
    // ChatItem Prefab
    public GameObject chatItemFactory;
    
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Connect()
    {
        AppSettings photonSettings = PhotonNetwork.PhotonServerSettings.AppSettings;

        //위 설정을 가지고 ChatAppSettings셋팅
        ChatAppSettings chatAppSettings = new ChatAppSettings();
        chatAppSettings.AppIdChat = photonSettings.AppIdChat;
        chatAppSettings.AppVersion = photonSettings.AppVersion;
        chatAppSettings.FixedRegion = photonSettings.FixedRegion;
        chatAppSettings.NetworkLogging = photonSettings.NetworkLogging;
        chatAppSettings.Protocol = photonSettings.Protocol;
        chatAppSettings.EnableProtocolFallback = photonSettings.EnableProtocolFallback;
        chatAppSettings.Server = photonSettings.Server;
        chatAppSettings.Port = (ushort)photonSettings.Port;
        chatAppSettings.ProxyServer = photonSettings.ProxyServer;

        // ChatClient 만들자
        ChatClient = new ChatClient(this);
        // 닉네임
        ChatClient.AuthValues = new Photon.Chat.AuthenticationValues(PhotonNetwork.NickName);
        // 연결 시도
        ChatClient.ConnectUsingSettings(chatAppSettings);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnDisconnected()
    {
    }

    public void OnConnected()
    {
        print("채팅 서버 접속 성공!");
        // 전체 채널에 들어가자 (구독)
        ChatClient.Subscribe(currentChannel);
    }

    public void OnChatStateChange(ChatState state)
    {
    }

    // 특정 채널에 다른 사람(나)이 메시지를 보내고 나한테 응답이 올떄 호출 되는 함수
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            print(senders[i] + ":"+messages[i]);
            // chatItem 생성(Content 의 자식으로)
            GameObject go = Instantiate(chatItemFactory, trContent);
            // 생성된 게임 오브젝트에서 ChatItem 컴포넌트 가져온다.
            ChatItem chatItem = go.GetComponent<ChatItem>();
            // 가져온 컴포넌트에서 SetText 함수 실행
            chatItem.SetText(senders[i] + ":" + messages[i]);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
    }
    // 채팅 채널에 접속이 성공했을 때 들어오는 함수
    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            print(channels[i] + "채널에 접속이 성공 했습니다");
        }
    }
    // 채팅 채널에서 나갔을 때 들어오는 함수
    public void OnUnsubscribed(string[] channels)
    {
        for(int i = 0;i < channels.Length; i++)
        {
            print(channels[i]+"채널에서 나갔습니다");
        }
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }
}
