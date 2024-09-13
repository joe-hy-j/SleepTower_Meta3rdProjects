using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice_Player : MonoBehaviour
{
    // 포톤 보이스 뷰
    public PhotonVoiceView voiceView;
    // 스피커 이미지 
    public GameObject speakerImg;
    // 말풍선 이미지 
    public GameObject speeckImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 말할때 말풍선 이미지 활성화 / 비활성화
       speeckImg.SetActive(voiceView.IsRecording);

        // 들을때 스피커 이미지 활성화 / 비활성화
       speakerImg.SetActive(voiceView.IsSpeaking);

    }
}
