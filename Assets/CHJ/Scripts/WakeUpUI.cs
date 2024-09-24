using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WakeUpUI : MonoBehaviour
{
    TMP_Text infoText;
    Button button;

    public void SetUI(string nickName)
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        infoText.text = nickName + "님이 잠에 들었습니다.";
        gameObject.SetActive(true);
    }
}
