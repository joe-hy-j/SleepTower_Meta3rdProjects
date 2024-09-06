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
    public Button alarmSetBtn;

    MiniGameManager.GameType selectedGameType;

    private void Start()
    {
        memorySelectBtn.onClick.AddListener( () =>{
            selectedGameType = MiniGameManager.GameType.MemoryGame;
        });
        pillowSelectBtn.onClick.AddListener(() =>
        {
            selectedGameType = MiniGameManager.GameType.PillowGame;
        });

        alarmSetBtn.onClick.AddListener(() =>
        {
            if (selectedGameType != MiniGameManager.GameType.None)
                MiniGameManager.instance.SetGameType(selectedGameType);
            else
                Debug.LogError("Game Type이 결정되지 않았습니다.");
        });
    }
}
