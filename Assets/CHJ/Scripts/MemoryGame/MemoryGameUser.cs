using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameUser : MonoBehaviour
{
    public MemoryGameUIManager gm;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                print(hit.transform.name);
                if (hit.collider.gameObject.CompareTag("MemoryGameBlock"))
                {
                    print("Hello");
                    hit.transform.gameObject.GetComponent<BlockClickedAction>().OnClick();
                }
            }
        }
    }
}
