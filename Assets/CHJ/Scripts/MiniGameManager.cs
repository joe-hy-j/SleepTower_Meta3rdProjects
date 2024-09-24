using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviourPunCallbacks
{
    //미니 게임 매니저 (싱글턴)
    public static MiniGameManager instance;

    //게임 매니저 오브젝트
    public MemoryGameUIManager memoryGM;
    public PillowGameManager pillowGM;
    public MediaPipeGameManager mediaPipeGM;

    // 내 플레이어가 미니게임을 끝냈는지 체크
    bool isMiniGameEnd = false;
    // 방장이 관리하는 변수: 미니 게임 끝낸 플레이어 수를 센다.
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

    //싱글턴 구현
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

    //미니게임 타입을 결정한다. 이 함수는 방장만 호출할 수 있고, 다른 사용자는 RpcSetGameType으로 호출을 받는다.
    public void SetGameType(GameType gameType)
    {
        photonView.RPC(nameof(RpcSetGameType),RpcTarget.All, gameType);
    }

    [PunRPC]
    public void RpcSetGameType(GameType gameType)
    {
        this.gameType = gameType;
    }

    // 새로운 플레이어가 들어오면, 이 함수를 실행시켜준다.
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        photonView.RPC(nameof(RpcSetGameType),newPlayer, gameType);
    }

    //미니게임을 시작한다.
    public void StartMiniGame()
    {
        switch (gameType)
        {
            case GameType.MemoryGame:
                //Memory Game Manager의 StartGame을 실행한다.
                memoryGM.SetUIInterface();
                break;
            case GameType.PillowGame:
                //Pillow Game Manager의 StartGame을 실행한다.
                pillowGM.SetUIInterface();
                break;
            case GameType.MediaPipeGame:
                //MediaPipe Game Manager를 실행한다.
                mediaPipeGM.SetUIInterface();
                break;
            default:
                break;
                
        }
    }

    // 미니 게임이 끝났을 때 실행되어야 하는 함수
    public void MiniGameEnd()
    {
        isMiniGameEnd = true;

        //UI 변경
        photonView.RPC(nameof(RpcOnePlayerEndGameUI), RpcTarget.All);

        // 변수 올려주어야 한다. (RPCTarget.Master)
        photonView.RPC(nameof(EndPlayerCountUp), RpcTarget.MasterClient);

        if(OnMiniGameEnd != null)
            OnMiniGameEnd();
    }

    // 마스터에서만 실행되는 함수 현재 모든 플레이어가 게임을 끝냈는지 확인한다.
    [PunRPC]
    void EndPlayerCountUp()
    {
        miniGameEndPlayerCount++;
        CheckAllPlayerEndGame();
    }

    // 한명이 미니게임을 끝냈을 때 호출되는 함수, 한 명 UI만 바꿔준다.
    [PunRPC]
    void RpcOnePlayerEndGameUI()
    {
        // UI 변경 (RPCTarget.All)
        print(photonView.Owner.NickName + "님이 미니게임을 끝냈습니다!");
    }

    // 모두가 미니게임을 끝냈는지 체크한다.
    void CheckAllPlayerEndGame()
    {
        if(miniGameEndPlayerCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            // 모두가 게임을 끝냈다
            // 모두의 알람을 끄는 함수를 실행하자.
            photonView.RPC(nameof(RpcEndAlarm), RpcTarget.All);
            // 모두의 isMiniGameEnd = false로 바꿔주자
        }
    }
    // 알람을 끄고, UI를 끈다.
    [PunRPC]
    void RpcEndAlarm()
    {
        // 변수 초기화하자
        isMiniGameEnd = false;
        // 알람을 끈다.
        AlarmManager.instance.OffAlarm();
        // UI를 끈다.
        print(PhotonNetwork.NickName + "님의 알람이 꺼졌습니다!");
    }

    // 내가 만약 미니게임이 끝났을 때, 상대방의 볼륨을 올릴 수 있게 하는 함수
    public void OtherPlayerVolumeUp()
    {

    }
}
