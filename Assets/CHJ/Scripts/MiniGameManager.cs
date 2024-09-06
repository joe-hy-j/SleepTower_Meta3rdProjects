using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    //�̴� ���� �Ŵ��� (�̱���)
    public static MiniGameManager instance;

    //���� �Ŵ��� ������Ʈ
    public MemoryGameUIManager memoryGM;
    public PillowGameManager pillowGM;

    public enum GameType
    {
        None,
        MemoryGame,
        PillowGame
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

    //�̴ϰ��� Ÿ���� �����Ѵ�.
    public void SetGameType(GameType gameType)
    {
        this.gameType = gameType;
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
            default:
                break;
        }
    }
}
