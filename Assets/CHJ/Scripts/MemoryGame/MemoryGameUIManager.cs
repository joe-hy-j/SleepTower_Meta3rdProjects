using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameUIManager : MemoryGameManager
{
    GameObject[,] blocks;
    public GameObject blockFactory;

    public GameObject buttonFactory;
    public GameObject textFactory;
    
    //user�� �Է��� �� �� �ִ� ��Ȳ���� Ȯ��
    bool canUserInput = false;

    Button startButton;
    Text winText;

    public void SetUIInterface()
    {
        winText = Instantiate(textFactory, GameObject.Find("GameCanvas").transform).GetComponent<Text>();
        winText.gameObject.SetActive(false);
        winText.text = "�̰���ϴ�!";
        startButton = Instantiate(buttonFactory, GameObject.Find("GameCanvas").transform).GetComponent<Button>();

        startButton.onClick.AddListener(() =>
        {
            StartGame();
            startButton.interactable = false;
        });
    }
    //���� �����ϴ� �Լ�
    public void StartGame()
    {
        InitializeGame();
        CreateQuestion();
        SetQuestionCount(3);
    }


    private void Update()
    {
        if (canUserInput)
        {
            if (CheckAnswer())
            {
                canUserInput = false;
                StartCoroutine(CheckProcess(0.1f));

            }
        }
    }
    IEnumerator CheckProcess(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (score < questionCount)
        {
            CreateQuestion();
        }
        else
        {
            EndGame();
        }
    }
    //������ ó�� ������ �� ȣ���ϴ� �Լ�
    void InitializeGame()
    {
        //�θ𿡼� question, answer �迭�� �ʱ�ȭ
        base.InitializeGame();
        //���� block �迭�� �̹� �����ϸ�
        if (blocks != null)
        {
            //block �迭�� gameObject�� �ı�
            foreach (var block in blocks)
            {
                Destroy(block);
            }
        }

        //block�� ����
        blocks = new GameObject[3, 3];
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for(int j = 0; j < blocks.GetLength(1); j++)
            {
                blocks[i,j] = Instantiate(blockFactory, new Vector3(ColomnToXPos(i), 0, RowToZPos(j)), Quaternion.identity);
                blocks[i,j].GetComponent<BlockClickedAction>().SetColumnAndRow(i, j);
            }
        }


    }

    void CreateQuestion()
    {
        base.CreateQuestion();
        print("CreateQuestion");
        StartCoroutine(QuestioinShowProcess(3.0f));
    }

    public void GetUserInput(int column, int row)
    {
        base.GetUserInput(column, row);
        if (answer[column, row] == true)
            blocks[column, row].GetComponent<Renderer>().material.color = Color.red;
        else
            blocks[column, row].GetComponent<Renderer>().material.color = Color.white;
        canUserInput = true;

    }

    void EndGame()
    {
        base.EndGame();
        MiniGameManager.instance.MiniGameEnd();
        Destroy(startButton.gameObject);
        RemoveAllBlocks();
        StartCoroutine(ShowTextProcess(1));
    }

    IEnumerator ShowTextProcess(float seconds)
    {
        winText.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        Destroy(winText.gameObject);
    }
    void RemoveAllBlocks()
    {
        foreach(GameObject block in blocks)
        {
            Destroy(block);
        }
    }
    float ColomnToXPos(int column)
    {
        return (column - 1) * 1.3f;
    }

    float RowToZPos(int row)
    {
        return (1 - row) * 1.3f;
    }

    IEnumerator QuestioinShowProcess(float showingTime)
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                if (question[i, j] == true)
                    blocks[i, j].GetComponent<Renderer>().material.color = Color.red;
                else
                    blocks[i, j].GetComponent<Renderer>().material.color = Color.white;
                blocks[i, j].GetComponent<BlockClickedAction>().isActive = false;
            }
        }

        yield return new WaitForSeconds(showingTime);

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                if (question[i, j] == true)
                {
                    blocks[i, j].GetComponent<Renderer>().material.color = Color.white;
                }
                blocks[i, j].GetComponent<BlockClickedAction>().isActive = true;
            }
        }
    }
}
