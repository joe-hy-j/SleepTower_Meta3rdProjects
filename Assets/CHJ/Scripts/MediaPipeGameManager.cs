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
        // UI�� �����Ѵ�.
        // �ൿ ǥ���ϴ� text 1��
        infoText = Instantiate(textFactory, GameObject.Find("GameCanvas").transform).GetComponent<Text>();
        infoText.text = "";
        StartGame();
    }

    void StartGame()
    {
        isGameStart = true;
        startButton.interactable = false;
        // ���� ������ �����Ѵ�.
        SetQuiz();
        // UI�� ��� ǥ���Ѵ�.
        infoText.text = answerGesture.ToString() + "�� ���ϼ���!" ;
        // ���̽� �ڵ带 �����Ѵ�.
        try
        {
            Process psi = new Process();
            psi.StartInfo.FileName = "C:\\Users\\Admin\\AppData\\Local\\Programs\\Python\\Python312\\python.exe";
            // ������ ���ø����̼� �Ǵ� ����
            psi.StartInfo.Arguments = "C:\\Users\\Admin\\Downloads\\temp_1726030948758.623395642\\main.py";
            // ���� ���۽� ����� �μ�
            psi.StartInfo.CreateNoWindow = true;
            // ��â �ȶ����
            psi.StartInfo.UseShellExecute = false;
            // ���μ����� �����Ҷ� �ü�� ���� �������
            psi.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Unable to launch app: " + e.Message);
        }
    }

    bool CheckAnswer()
    {
        // ���� ���� ���� ����� ���ٸ�
        if (answerGesture.palm_sustained == true && gesture.palm_sustained == true)
            return true;
        else if (answerGesture.v_sustained == true && gesture.v_sustained == true)
            return true;
        else
            return false;
        // �ƴ϶��
    }

    void EndGame()
    {
        isGameStart = false;
        MiniGameManager.instance.MiniGameEnd();
        StartCoroutine(EndProcess());
    }

    IEnumerator EndProcess()
    {
        infoText.text = "���� ����...";  

        yield return new WaitForSeconds(2);

        // UI�� ���ش�
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
        // �����ϰ� �չٴ� ���̱�� v�� ���� �߿��� �� ���� ����.
        int randNum = UnityEngine.Random.Range(0, 2);
        answerGesture = new GestureData();
        switch (randNum)
        {
            // v�� ���̱�
            case 0:
                answerGesture.v_sustained = true;
                break;
            // �չٴ� ���̱�
            case 1:
                answerGesture.palm_sustained = true;
                break;
        }
    }

}
