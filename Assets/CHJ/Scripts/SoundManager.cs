using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource alarmSound;

    private void Start()
    {
        AlarmManager.instance.onAlarmUI += AlarmSoundPlay;
        AlarmManager.instance.offAlarmUI += AlarmSoundStop;
    }
    void AlarmSoundPlay(object sender, EventArgs e)
    {
        alarmSound.Play();
    }

    void AlarmSoundStop(object sender, EventArgs e)
    {
        alarmSound.Stop();
    }
}
