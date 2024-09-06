using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClickedAction : MonoBehaviour
{
    public int blockColumn;
    public int blockRow;
    public bool isActive = false;
    
    MemoryGameUIManager gm;

    private void Start()
    {
        gm = MiniGameManager.instance.memoryGM;
    }
    public void SetColumnAndRow(int column, int row)
    {
        blockColumn = column; blockRow = row;
    }
    public void OnClick()
    {
        if (isActive)
        {
            gm.GetUserInput(blockColumn, blockRow);
        }
    }
}
