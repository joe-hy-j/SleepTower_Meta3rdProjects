using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WakeUpUI : MonoBehaviour
{
    // �� �ڽĵ�
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
            print("player ��ü�� null �ε���...?");
            return; 
        }

        // UI�� ǥ���Ѵ�.
        infoTextObject.gameObject.SetActive(true);
        wakeUpButton.gameObject.SetActive(true);

        // �ؽ�Ʈ ���� �ٲ۴�.
        infoText.text = player.NickName + "���� �ῡ ������ϴ�.";

        // ��ư�� OnClick�� �ٲ۴�.
        wakeUpButton.onClick.RemoveAllListeners();
        wakeUpButton.onClick.AddListener(() => SoundManager.instance.VolumeUp(player));
        wakeUpButton.onClick.AddListener(() => WakeUpManager.instance.RemoveSleepingPlayerList(player.NickName));
    }

    public void DisableUI()
    {
        // UI�� ������.
        infoTextObject.gameObject.SetActive(false);
        wakeUpButton.gameObject.SetActive(false);
    }

}
