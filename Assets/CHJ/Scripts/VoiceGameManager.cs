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
        if(myResult.result == "정답")
        {
            isGameStart = false;
            EndGame();
        }
    }
    public void SetUIInterface()
    {
        infoText = Instantiate(textFactory, GameObject.Find("GameCanvas").transform).GetComponent<Text>();
        infoText.text = "검정색 화면에 뜬 지시를 따라주세요...";
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
