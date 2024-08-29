using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameUIManager : MemoryGameManager
{
    GameObject[,] blocks;
    public GameObject blockFactory;
    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        base.StartGame();
        blocks = new GameObject[3, 3];
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for(int j = 0; j < blocks.GetLength(1); j++)
            {
                blocks[i,j] = Instantiate(blockFactory, new Vector3(ColomnToXPos(i), 0, RowToZPos(j)), Quaternion.identity);
                blocks[i,j].GetComponent<BlockClickedAction>().SetColumnAndRow(i, j);
            }
        }

        CreateQuestion();
    }

    void CreateQuestion()
    {
        base.CreateQuestion();

        StartCoroutine(QuestioinShowProcess(3.0f));
    }
    public void GetUserInput(int column, int row)
    {
        base.GetUserInput(column, row);
        if (answer[column, row] == true)
            blocks[column, row].GetComponent<Renderer>().material.color = Color.red;
        else
            blocks[column, row].GetComponent<Renderer>().material.color = Color.white;

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
        for(int i =0; i< blocks.GetLength(0); i++)
        {
            for(int j = 0;j < blocks.GetLength(1); j++)
            {
                if (question[i,j] == true)
                    blocks[i, j].GetComponent<Renderer>().material.color = Color.red;
            }
        }

        yield return new WaitForSeconds(showingTime);

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                if (question[i, j] == true)
                    blocks[i, j].GetComponent<Renderer>().material.color = Color.white;
            }
        }

    }

}
