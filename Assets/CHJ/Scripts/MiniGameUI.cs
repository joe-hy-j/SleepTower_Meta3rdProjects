using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    //입력 필드
    [Header("Buttons")]
    public Button memorySelectBtn;
    public Button pillowSelectBtn;
    public Button mediaPipeSelectBtn;
    public Button voiceSelectBtn;
    public Button alarmSetBtn;

    MiniGameManager.GameType selectedGameType;

    private void Start()
    {
        memorySelectBtn.onClick.AddListener(() => {
            selectedGameType = MiniGameManager.GameType.MemoryGame;
        });
        pillowSelectBtn.onClick.AddListener(() =>
        {
            selectedGameType = MiniGameManager.GameType.PillowGame;
        });
        mediaPipeSelectBtn.onClick.AddListener(() =>
        {
            selectedGameType = MiniGameManager.GameType.MediaPipeGame;
        });

        voiceSelectBtn.onClick.AddListener(() =>
        {
            selectedGameType = MiniGameManager.GameType.VoiceGame;
        });

    }

    public bool MiniGameSet()
    {
        if(selectedGameType != MiniGameManager.GameType.None)
        {
            MiniGameManager.instance.SetGameType(selectedGameType);
            return true;
        }
        else
        {
            Debug.LogError("Game Type이 결정되지 않았습니다.");
            ToastExample.instance.ShowToastMessage("게임 종류가 결정되지 않았어요!");
            return false;
        }
    }
}
