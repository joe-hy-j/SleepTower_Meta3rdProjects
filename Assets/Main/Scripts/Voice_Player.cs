using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice_Player : MonoBehaviour
{
    // ���� ���̽� ��
    public PhotonVoiceView voiceView;
    // ����Ŀ �̹��� 
    public GameObject speakerImg;
    // ��ǳ�� �̹��� 
    public GameObject speeckImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���Ҷ� ��ǳ�� �̹��� Ȱ��ȭ / ��Ȱ��ȭ
       speeckImg.SetActive(voiceView.IsRecording);

        // ������ ����Ŀ �̹��� Ȱ��ȭ / ��Ȱ��ȭ
       speakerImg.SetActive(voiceView.IsSpeaking);

    }
}
