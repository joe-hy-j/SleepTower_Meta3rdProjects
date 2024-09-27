using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCanvas : MonoBehaviour
{
    public GameObject cheatCanvas;
    public MonsterMove monsterMove;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            cheatCanvas.SetActive(!cheatCanvas.activeSelf);
            if(cheatCanvas.activeSelf)
            {
                monsterMove.MonsterJump();
            }
            else
            {
                monsterMove.MonsterJumpDown();
            }

        }
    }
}
