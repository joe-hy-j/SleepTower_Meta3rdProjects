using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JHJ;

public class SetUITime : MonoBehaviour
{
    public Text timeText;
    public Text ampmText;

    void Update()
    {
        if(TimeManager.Time.Hour > 12)
        {
            ampmText.text = "오후";
        }
        else
        {
            ampmText.text = "오전";
        }

        timeText.text = (TimeManager.Time.Hour%12).TimeToPrettyString() + " : " + TimeManager.Time.Minute.TimeToPrettyString();
    }


}
