using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClickedAction : MonoBehaviour
{
    public int blockColumn;
    public int blockRow;

    
    MemoryGameUIManager gm;

    private void Start()
    {
        gm = GameObject.Find("MemoryGameManager").GetComponent<MemoryGameUIManager>();
    }
    public void SetColumnAndRow(int column, int row)
    {
        blockColumn = column; blockRow = row;
    }
    public void OnClick()
    {
        print("Onclicked");
        gm.GetUserInput(blockColumn, blockRow);
    }
}
