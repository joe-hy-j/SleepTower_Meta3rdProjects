using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    //���� ����
    protected bool[,] question;
    //������ ���
    protected bool[,] answer;

    //���� ��� ����
    protected int questionBlockCount = 3;

    //���� ���� ����
    protected int score = 0;

    protected void StartGame()
    {
        CreateQuestion();
        question = new bool[3, 3];
        answer = new bool[3, 3];
    }

    protected void CreateQuestion()
    {
        //question �迭�� ��� false�� �ʱ�ȭ��Ų��.
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
