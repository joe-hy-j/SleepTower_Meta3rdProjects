using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetUITime : MonoBehaviour
{
    public TMP_Text timeText;

    void Update()
    {
        timeText.text = TimeManager.Time.Hour + ":" + TimeManager.Time.Minute;
    }
}
