using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmogeSystem : MonoBehaviourPun
{
    public List<GameObject> emoges;  // 이모티콘들을 넣을 리스트 생성
    public GameObject emoGroup;  // 이모티콘들을 자식으로 넣은 부모 객체 지정

    float currentTime = 0;  // 시간초 현재시간
    bool onEmo = false;  // 이모티콘 활성화 여부

    void Start()
    {
        
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // 이모티콘이 활성화되어 있을경우
            if (onEmo)
            {
                currentTime += Time.deltaTime;  // 시간초 시작
                                                // 시간초가 3초 경과할 경우
                if (currentTime >= 3)
                {
                    emoGroup.SetActive(false);  // 이모티콘 부모 객체 비활성화 (모든 이모티콘 비활성화)
                    onEmo = false;  // 이모티콘 비활성화 상태
                    currentTime = 0;  // 시간초 초기화
                }
            }
        }
    }

    public void SmileEmo()  // 웃는 이모티콘 버튼
    {
        if (photonView.IsMine)
        {
            EmoActive();  // 이모티콘 활성화 상태
            ActiveOnlyOneEmoge(0);  // 해당 이모티콘만 활성화, 나머지 비활성화
        }
    }

    public void SadEmo()  // 우는 이모티콘 버튼
    {
        if (photonView.IsMine)
        {
            EmoActive();  // 이모티콘 활성화 상태
            ActiveOnlyOneEmoge(1);  // 해당 이모티콘만 활성화, 나머지 비활성화
        }
    }

    public void SurpEmo()  // 놀란 이모티콘 버튼
    {
        if (photonView.IsMine)
        {
            EmoActive();  // 이모티콘 활성화 상태
            ActiveOnlyOneEmoge(2);  // 해당 이모티콘만 활성화, 나머지 비활성화
        }
    }

    public void HeartEmo()  // 하트 이모티콘 버튼
    {
        if (photonView.IsMine)
        {
            EmoActive();  // 이모티콘 활성화 상태
            ActiveOnlyOneEmoge(3);  // 해당 이모티콘만 활성화, 나머지 비활성화
        }
    }

    public void AngryEmo()  // 화난 이모티콘 버튼
    {
        if (photonView.IsMine)
        {
            EmoActive();  // 이모티콘 활성화 상태
            ActiveOnlyOneEmoge(4);  // 해당 이모티콘만 활성화, 나머지 비활성화
        }
    }

    public void QuestEmo()  // 물음표 이모티콘 버튼
    {
        if (photonView.IsMine)
        {
            EmoActive();  // 이모티콘 활성화 상태
            ActiveOnlyOneEmoge(5);  // 해당 이모티콘만 활성화, 나머지 비활성화
        }
    }

    // 원하는 이모티콘만 활성화 하고 나머지는 비활성화 시키는 함수
    void ActiveOnlyOneEmoge(int WantToActive)  // WantToActive = 활성화 하고자 하는 이모티콘의 리스트 인덱스 숫자
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < emoges.Count; i++)  // 저장된 모든 이모티콘의 수만큼 계산
            {
                if (i == WantToActive)  // 원하는 이모티콘의 인덱스 숫자가 i 라면
                {
                    emoges[i].SetActive(true);  // i 에 해당하는 인덱스의 이모티콘만 활성화
                }
                else
                {
                    emoges[i].SetActive(false);  // i 에 남은 나머지 인덱스의 이모티콘들은 비활성화
                }
            }
        }
    }

    void EmoActive()  // 이모티콘이 활성화 상태일때 호출할 함수
    {
        if (photonView.IsMine)
        {
            emoGroup.SetActive(true);  // 이모티콘들의 부모 객체 활성화
            onEmo = true;  // 이모티콘 활성화 상태
            currentTime = 0;  // 시간초 초기화
        }
    }
}
