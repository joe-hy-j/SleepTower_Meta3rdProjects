using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCanvas : MonoBehaviour
{
    public GameObject cheatCanvas;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            cheatCanvas.SetActive(!cheatCanvas.activeSelf);
        }
    }
}
