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

    //오전 오후를 표시할 것인지 확인
    public bool showAMPMText = true;

    void Update()
    {
        //오전 오후 표시 방법 (12시간)
        if (showAMPMText)
        {
            if (TimeManager.Time.Hour > 12)
            {
                ampmText.text = "오후";
            }
            else
            {
                ampmText.text = "오전";
            }

            timeText.text = (TimeManager.Time.Hour % 12).TimeToPrettyString() + " : " + TimeManager.Time.Minute.TimeToPrettyString();
        }
        //24시간 표시, 오전 오후 표시 안함

        else
        {
            timeText.text = TimeManager.Time.Hour.TimeToPrettyString() + " : " + TimeManager.Time.Minute.TimeToPrettyString();
        }
    }


}
