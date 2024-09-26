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

        alarmSetBtn.onClick.AddListener(() =>
        {
            // ���常 ������ ������ �� �ֽ��ϴ�.
            if (!PhotonNetwork.IsMasterClient) { return; }

            if (selectedGameType != MiniGameManager.GameType.None)
                MiniGameManager.instance.SetGameType(selectedGameType);
            else
            {
                Debug.LogError("Game Type�� �������� �ʾҽ��ϴ�.");
                ToastExample.instance.ShowToast("���� ������ �������� �ʾҾ��!");
            }
        });
    }
}
