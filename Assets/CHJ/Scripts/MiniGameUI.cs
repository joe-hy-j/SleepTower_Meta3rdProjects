using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    //�Է� �ʵ�
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
            Debug.LogError("Game Type�� �������� �ʾҽ��ϴ�.");
            ToastExample.instance.ShowToastMessage("���� ������ �������� �ʾҾ��!");
            return false;
        }
    }
}
