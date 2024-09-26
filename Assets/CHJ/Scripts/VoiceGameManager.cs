using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceGameResult
{
    public string result;
}
public class VoiceGameManager : MonoBehaviour
{

    public GameObject textFactory;

    Text infoText;
    bool isGameStart = false;

    private void Update()
    {
        if (!isGameStart) return;

        string result = InputOutputFileManager.GetInstance().ReadFile();
        VoiceGameResult myResult = JsonUtility.FromJson<VoiceGameResult>(result);
        if(myResult.result == "����")
        {
            isGameStart = false;
            EndGame();
        }
    }
    public void SetUIInterface()
    {
        infoText = Instantiate(textFactory, GameObject.Find("GameCanvas").transform).GetComponent<Text>();
        infoText.text = "������ ȭ�鿡 �� ���ø� �����ּ���...";
        StartGame();
    }

    void StartGame()
    {
        isGameStart = true;
    }

    void EndGame()
    {
        Destroy(infoText.gameObject);
        MiniGameManager.instance.MiniGameEnd();
    }
}
