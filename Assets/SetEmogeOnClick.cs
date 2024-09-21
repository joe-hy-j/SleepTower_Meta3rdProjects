using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEmogeOnClick : MonoBehaviour
{
    public Button emoge1;
    public Button emoge2;
    public Button emoge3;
    public Button emoge4;
    public Button emoge5;
    public Button emoge6;
    public GameRoomManager roomManager;

    void Start()
    {
        emoge1.onClick.AddListener(roomManager.myEmoge.SmileEmo);
        emoge2.onClick.AddListener(roomManager.myEmoge.SadEmo);
        emoge3.onClick.AddListener(roomManager.myEmoge.SurpEmo);
        emoge4.onClick.AddListener(roomManager.myEmoge.HeartEmo);
        emoge5.onClick.AddListener(roomManager.myEmoge.AngryEmo);
        emoge6.onClick.AddListener(roomManager.myEmoge.QuestEmo);
    }

}
