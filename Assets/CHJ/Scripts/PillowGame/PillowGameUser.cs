using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PillowGameUser : MonoBehaviourPun
{
    void Update()
    {
        if (!photonView.IsMine) return;

        if (MiniGameManager.instance.pillowGM.isPlaying && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Pillow"))
                {

                    if (MiniGameManager.instance.pillowGM.RemovePillow(hit.transform.gameObject))
                    {
                        MiniGameManager.instance.pillowGM.OnClick();
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}
