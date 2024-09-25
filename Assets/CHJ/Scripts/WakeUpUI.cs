using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WakeUpUI : MonoBehaviour
{
    // 내 자식들
    public Button wakeUpButton;
    public TMP_Text infoText;
    public GameObject infoTextObject;

    private void Start()
    {
        wakeUpButton.gameObject.SetActive(false);
        infoTextObject.SetActive(false);
    }

    public void SetUI(Player player)
    {
        if (player == null) {
            print("player 객체가 null 인데요...?");
            return; 
        }

        // UI를 표시한다.
        infoTextObject.gameObject.SetActive(true);
        wakeUpButton.gameObject.SetActive(true);

        // 텍스트 값을 바꾼다.
        infoText.text = player.NickName + "님이 잠에 들었습니다.";

        // 버튼의 OnClick을 바꾼다.
        wakeUpButton.onClick.RemoveAllListeners();
        wakeUpButton.onClick.AddListener(() => SoundManager.instance.VolumeUp(player));
        wakeUpButton.onClick.AddListener(() => WakeUpManager.instance.RemoveSleepingPlayerList(player.NickName));
    }

    public void DisableUI()
    {
        // UI를 꺼주자.
        infoTextObject.gameObject.SetActive(false);
        wakeUpButton.gameObject.SetActive(false);
    }

}
