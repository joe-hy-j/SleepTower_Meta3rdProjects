using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PillowGameUser : MonoBehaviour
{
    public PillowGameManager gm;
    void Update()
    {
        if (gm.isPlaying && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Pillow"))
                {

                    if (gm.RemovePillow(hit.transform.gameObject))
                    {
                        gm.OnClick();
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}
