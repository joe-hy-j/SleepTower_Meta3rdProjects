using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    //미니 게임 매니저 (싱글턴)
    public static MiniGameManager instance;

    //게임 매니저 오브젝트
    public MemoryGameUIManager memoryGM;
    public PillowGameManager pillowGM;

    public enum GameType
    {
        None,
        MemoryGame,
        PillowGame
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

    //미니게임 타입을 결정한다.
    public void SetGameType(GameType gameType)
    {
        this.gameType = gameType;
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
            default:
                break;
        }
    }
}
