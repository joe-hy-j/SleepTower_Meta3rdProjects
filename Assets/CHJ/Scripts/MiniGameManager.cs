using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MiniGameManager : MonoBehaviourPunCallbacks
{
    //�̴� ���� �Ŵ��� (�̱���)
    public static MiniGameManager instance;

    //���� �Ŵ��� ������Ʈ
    public MemoryGameUIManager memoryGM;
    public PillowGameManager pillowGM;
    public MediaPipeGameManager mediaPipeGM;
    public VoiceGameManager voiceGM;

    // �� �÷��̾ �̴ϰ����� ���´��� üũ
    bool isMiniGameEnd = false;
    // ������ �����ϴ� ����: �̴� ���� ���� �÷��̾� ���� ����.
    int miniGameEndPlayerCount = 0;

    public Action OnMiniGameEnd;
    public Action OnVideoEnd;

    public GameObject allClearImage;
    public GameObject myClearImage;

    public VideoPlayer clearVideo;
    public GameObject videoImage;

    public enum GameType
    {
        None,
        MemoryGame,
        PillowGame,
        MediaPipeGame,
        VoiceGame
    }

    GameType gameType;

    //�̱��� ����
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //�̴ϰ��� Ÿ���� �����Ѵ�. �� �Լ��� ���常 ȣ���� �� �ְ�, �ٸ� ����ڴ� RpcSetGameType���� ȣ���� �޴´�.
    public void SetGameType(GameType gameType)
    {
        photonView.RPC(nameof(RpcSetGameType),RpcTarget.All, gameType);
    }

    [PunRPC]
    public void RpcSetGameType(GameType gameType)
    {
        this.gameType = gameType;
    }

    // ���ο� �÷��̾ ������, �� �Լ��� ��������ش�.
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        photonView.RPC(nameof(RpcSetGameType),newPlayer, gameType);
    }

    //�̴ϰ����� �����Ѵ�.
    public void StartMiniGame()
    {
        // �˶��� �︱ ���� ����ȴ�.
        if (!AlarmManager.instance.IsAlarmOn) return;

        switch (gameType)
        {
            case GameType.MemoryGame:
                //Memory Game Manager�� StartGame�� �����Ѵ�.
                memoryGM.SetUIInterface();
                break;
            case GameType.PillowGame:
                //Pillow Game Manager�� StartGame�� �����Ѵ�.
                pillowGM.SetUIInterface();
                break;
            case GameType.MediaPipeGame:
                //MediaPipe Game Manager�� �����Ѵ�.
                mediaPipeGM.SetUIInterface();
                break;
            case GameType.VoiceGame:
                voiceGM.SetUIInterface();
                break;
            default:
                break;
                
        }
    }

    // �̴� ������ ������ �� ����Ǿ�� �ϴ� �Լ�
    public void MiniGameEnd()
    {
        isMiniGameEnd = true;

        ////UI ����
        //photonView.RPC(nameof(RpcOnePlayerEndGameUI), RpcTarget.All, PhotonNetwork.NickName);

        // ���� �÷��־�� �Ѵ�. (RPCTarget.Master)
        photonView.RPC(nameof(EndPlayerCountUp), RpcTarget.MasterClient);

        if(OnMiniGameEnd != null)
            OnMiniGameEnd();

        AlarmManager.instance.OffAlarm();

        StartCoroutine(MyClearUIProcess());
    }

    // �����Ϳ����� ����Ǵ� �Լ� ���� ��� �÷��̾ ������ ���´��� Ȯ���Ѵ�.
    [PunRPC]
    void EndPlayerCountUp()
    {
        miniGameEndPlayerCount++;
        CheckAllPlayerEndGame();
    }

    // �Ѹ��� �̴ϰ����� ������ �� ȣ��Ǵ� �Լ�, �� �� UI�� �ٲ��ش�.
    [PunRPC]
    void RpcOnePlayerEndGameUI(string nickname)
    {
        // UI ���� (RPCTarget.All)
        print(nickname + "���� �̴ϰ����� ���½��ϴ�!");
    }

    // ��ΰ� �̴ϰ����� ���´��� üũ�Ѵ�.
    void CheckAllPlayerEndGame()
    {
        if(miniGameEndPlayerCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            // ��ΰ� ������ ���´�
            // ����� �˶��� ���� �Լ��� ��������.
            //photonView.RPC(nameof(RpcEndAlarm), RpcTarget.All);
            photonView.RPC(nameof(RpcAllClearUIOn), RpcTarget.All);
            // ����� isMiniGameEnd = false�� �ٲ�����
        }
    }
    // �˶��� ����, UI�� ����.
    [PunRPC]
    void RpcEndAlarm()
    {
        // ���� �ʱ�ȭ����
        isMiniGameEnd = false;
        // �˶��� ����.
        AlarmManager.instance.OffAlarm();
        // UI�� ����.
        print(PhotonNetwork.NickName + "���� �˶��� �������ϴ�!");
    }

    [PunRPC]
    void RpcAllClearUIOn()
    {
        StartCoroutine(AllEndUIProcess());
    }

    IEnumerator AllEndUIProcess()
    {
        videoImage.SetActive(true);
        clearVideo.Play();
        yield return new WaitForSeconds(5.0f);
        videoImage.SetActive(false);
        if(OnVideoEnd != null)
            OnVideoEnd();
        allClearImage.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        allClearImage.SetActive(false);
    }

    IEnumerator MyClearUIProcess()
    {
        myClearImage.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        myClearImage.SetActive(false);
    }
}
