using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviourPun
{
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public AudioSource alarmSound;
    public AudioSource[] emogeSound;
    public AudioSource pillowSound;
    public AudioSource monsterScreamSound;

    public AudioMixer audioMixer;
    [SerializeField]
    float alarmVolume = 0.0f;


    private void Start()
    {
        AlarmManager.instance.onAlarmUI += AlarmSoundPlay;
        AlarmManager.instance.offAlarmUI += AlarmSoundStop;
        AlarmManager.instance.offAlarmUI += MonsterScreamSoundPlay;
        AlarmManager.instance.offAlarmUI += ResetVolumeSize;
    }
    void AlarmSoundPlay(object sender, EventArgs e)
    {
        alarmSound.Play();
    }

    void AlarmSoundStop(object sender, EventArgs e)
    {
        alarmSound.Stop();
    }

    public void EmogeSoundPlay(int emogeIdx)
    {
        photonView.RPC(nameof(RpcEmogeSoundPlay), RpcTarget.All, emogeIdx);
    }

    [PunRPC]
    void RpcEmogeSoundPlay(int emogeIdx)
    {
        if(emogeIdx < emogeSound.Length)
        {
            emogeSound[emogeIdx].Play();
        }
    }

    public void PillowSoundPlay()
    {
        photonView.RPC(nameof(RpcPillowSoundPlay), RpcTarget.All);
    }

    [PunRPC]
    void RpcPillowSoundPlay()
    {
        pillowSound.Play();
    }

    public void MonsterScreamSoundPlay(object sender, EventArgs e)
    {
        monsterScreamSound.Play();
    }

    [PunRPC]
    public void VolumeUp(float volumeSize)
    {
        if (audioMixer != null)
        {
            alarmVolume += volumeSize;
            print(photonView.Owner.NickName + "´ÔÀÇ ¾Ë¶÷ º¼·ýÀÌ Ä¿Á³½À´Ï´Ù: ÇöÀç º¼·ý " + alarmVolume);
            audioMixer.SetFloat("AlarmVolume", alarmVolume);
        }
    }

    public void ResetVolumeSize(object sender, EventArgs e)
    {
        alarmVolume = 0.0f;
        audioMixer.SetFloat("AlarmVolume", alarmVolume);
    }
}
