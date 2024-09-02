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

    public TMP_InputField inputField;
    public Button button;
    public TextMeshProUGUI winText;
    
    //user가 입력을 할 수 있는 상황인지 확인
    bool canUserInput = false;


    //게임 시작하는 함수
    public void StartGame()
    {
        InitializeGame();
        CreateQuestion();
        SetQuestionCount(Convert.ToInt32(inputField.text));
        inputField.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (canUserInput)
        {
            if (CheckAnswer())
            {
                canUserInput = false;
                if (score < questionCount)
                {
                    CreateQuestion();
                }
                else
                {
                    EndGame();
                }
            }
        }
    }

    //게임을 처음 시작할 때 호출하는 함수
    void InitializeGame()
    {
        //부모에서 question, answer 배열을 초기화
        base.InitializeGame();
        //만약 block 배열이 이미 존재하면
        if (blocks != null)
        {
            //block 배열의 gameObject를 파괴
            foreach (var block in blocks)
            {
                Destroy(block);
            }
        }

        //block을 형성
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

        inputField.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        winText.gameObject.SetActive(true);
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
