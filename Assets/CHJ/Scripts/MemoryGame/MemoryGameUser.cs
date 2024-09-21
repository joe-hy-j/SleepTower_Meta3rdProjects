using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameUser : MonoBehaviourPun
{
    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("MemoryGameBlock"))
                {
                    hit.transform.gameObject.GetComponent<BlockClickedAction>().OnClick();
                }
            }
        }
    }
}
