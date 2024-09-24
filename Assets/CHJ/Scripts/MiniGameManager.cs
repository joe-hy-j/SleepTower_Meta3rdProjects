using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviourPunCallbacks
{
    //�̴� ���� �Ŵ��� (�̱���)
    public static MiniGameManager instance;

    //���� �Ŵ��� ������Ʈ
    public MemoryGameUIManager memoryGM;
    public PillowGameManager pillowGM;
    public MediaPipeGameManager mediaPipeGM;

    // �� �÷��̾ �̴ϰ����� ���´��� üũ
    bool isMiniGameEnd = false;
    // ������ �����ϴ� ����: �̴� ���� ���� �÷��̾� ���� ����.
    int miniGameEndPlayerCount = 0;

    public Action OnMiniGameEnd;

    public enum GameType
    {
        None,
        MemoryGame,
        PillowGame,
        MediaPipeGame
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
            default:
                break;
                
        }
    }

    // �̴� ������ ������ �� ����Ǿ�� �ϴ� �Լ�
    public void MiniGameEnd()
    {
        isMiniGameEnd = true;

        //UI ����
        photonView.RPC(nameof(RpcOnePlayerEndGameUI), RpcTarget.All);

        // ���� �÷��־�� �Ѵ�. (RPCTarget.Master)
        photonView.RPC(nameof(EndPlayerCountUp), RpcTarget.MasterClient);

        if(OnMiniGameEnd != null)
            OnMiniGameEnd();
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
    void RpcOnePlayerEndGameUI()
    {
        // UI ���� (RPCTarget.All)
        print(photonView.Owner.NickName + "���� �̴ϰ����� ���½��ϴ�!");
    }

    // ��ΰ� �̴ϰ����� ���´��� üũ�Ѵ�.
    void CheckAllPlayerEndGame()
    {
        if(miniGameEndPlayerCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            // ��ΰ� ������ ���´�
            // ����� �˶��� ���� �Լ��� ��������.
            photonView.RPC(nameof(RpcEndAlarm), RpcTarget.All);
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

    // ���� ���� �̴ϰ����� ������ ��, ������ ������ �ø� �� �ְ� �ϴ� �Լ�
    public void OtherPlayerVolumeUp()
    {

    }
}
