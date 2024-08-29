using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    //기억력 질문
    protected bool[,] question;
    //유저의 대답
    protected bool[,] answer;

    //질문 블록 갯수
    protected int questionBlockCount = 3;

    //질문 맞춘 갯수
    protected int score = 0;

    protected void StartGame()
    {
        CreateQuestion();
        question = new bool[3, 3];
        answer = new bool[3, 3];
    }

    protected void CreateQuestion()
    {
        //question 배열을 모두 false로 초기화시킨다.
        question = new bool[3,3];
        int createdBlock = 0;

        while(createdBlock < questionBlockCount)
        {
            int column = Random.Range(0, 3);
            int row = Random.Range(0, 3);

            if (question[column, row] != true)
            {
                question[column, row] = true;
                createdBlock++;
            }
        }
    }

    public void GetUserInput(int column, int row)
    {
        answer[column, row] = !answer[column, row];
        CheckAnswer();
    }
    public bool CheckAnswer()
    {
        for(int i = 0; i < question.GetLength(0); i++)
        {
            for(int j = 0; j< question.GetLength(1); j++)
            {
                if (answer[i, j] != question[i, j])
                    return false;
            }
        }
        score++;
        print("Win!");
        return true;
    }
}
