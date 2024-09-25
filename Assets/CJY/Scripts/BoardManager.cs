using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviourPun
{
    public GameObject btn_Board;
    public GameObject highLight;
    public GameObject boardUI;
    public RectTransform slots;
    int postCount;
    int photoCount;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                highLight.SetActive(true);
                btn_Board.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                highLight.SetActive(false);
                btn_Board.SetActive(false);
            }
        }
    }

    public void OnBoard()
    {
        boardUI.SetActive(true);
    }

    public void CloseBoard()
    {
        
        boardUI.SetActive(false);
    }

    public void AddPost()
    {
        postCount++;
        if (postCount > 4) return;
        PhotonNetwork.Instantiate("PostIt", Vector3.zero, Quaternion.identity);
    }

    public void AddPhoto()
    {
        photoCount++;
        if (photoCount > 4) return;
        PhotonNetwork.Instantiate("Photo", Vector3.zero, Quaternion.identity);
    }

}
