using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MediaPipeGameManager : MonoBehaviour
{
    GestureData gesture;
    GestureData answerGesture;

    public GameObject buttonFactory;
    public GameObject textFactory;

    bool isGameStart =false;

    Button startButton;
    Text infoText;

    private void Update()
    {
        if (!isGameStart) return;

        gesture = SocketManager.instance.gesture;

        if (CheckAnswer())
        {
            EndGame();
        }
    }
    public void SetUIInterface()
    {
        // UI를 세팅한다.
        // 행동 표시하는 text 1개
        infoText = Instantiate(textFactory, GameObject.Find("GameCanvas").transform).GetComponent<Text>();
        infoText.text = "";
        StartGame();
    }

    void StartGame()
    {
        isGameStart = true;
        startButton.interactable = false;
        // 퀴즈 정답을 세팅한다.
        SetQuiz();
        // UI에 퀴즈를 표시한다.
        infoText.text = answerGesture.ToString() + "를 취하세요!" ;
        // 파이썬 코드를 실행한다.
        try
        {
            Process psi = new Process();
            psi.StartInfo.FileName = "C:\\Users\\Admin\\AppData\\Local\\Programs\\Python\\Python312\\python.exe";
            // 시작할 어플리케이션 또는 문서
            psi.StartInfo.Arguments = "C:\\Users\\Admin\\Downloads\\temp_1726030948758.623395642\\main.py";
            // 애플 시작시 사용할 인수
            psi.StartInfo.CreateNoWindow = true;
            // 새창 안띄울지
            psi.StartInfo.UseShellExecute = false;
            // 프로세스를 시작할때 운영체제 셸을 사용할지
            psi.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Unable to launch app: " + e.Message);
        }
    }

    bool CheckAnswer()
    {
        // 만약 들어온 답이 정답과 같다면
        if (answerGesture.palm_sustained == true && gesture.palm_sustained == true)
            return true;
        else if (answerGesture.v_sustained == true && gesture.v_sustained == true)
            return true;
        else
            return false;
        // 아니라면
    }

    void EndGame()
    {
        isGameStart = false;
        MiniGameManager.instance.MiniGameEnd();
        StartCoroutine(EndProcess());
    }

    IEnumerator EndProcess()
    {
        infoText.text = "게임 종료...";  

        yield return new WaitForSeconds(2);

        // UI를 없앤다
        Destroy(startButton.gameObject);
        Destroy(infoText.gameObject);

    }
    public void SetGestureData(GestureData gesture)
    {
        this.gesture = gesture;

        if (CheckAnswer())
        {
            EndGame();
        }
    }

    void SetQuiz()
    {
        // 랜덤하게 손바닥 보이기와 v자 포즈 중에서 한 개를 고른다.
        int randNum = UnityEngine.Random.Range(0, 2);
        answerGesture = new GestureData();
        switch (randNum)
        {
            // v자 보이기
            case 0:
                answerGesture.v_sustained = true;
                break;
            // 손바닥 보이기
            case 1:
                answerGesture.palm_sustained = true;
                break;
        }
    }

}
