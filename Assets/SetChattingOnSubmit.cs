using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetChattingOnSubmit : MonoBehaviour
{
    public TMP_InputField chatInput;
    public GameRoomManager roomManager;

    // Start is called before the first frame update
    void Start()
    {
        chatInput.onSubmit.AddListener(roomManager.myPlayer.GetComponentInChildren<ChatBubble>().OnSubmit);
    }
}
