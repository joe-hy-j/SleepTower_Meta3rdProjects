using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject btn_Board;
    public GameObject highLight;
    public GameObject boardUI;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            highLight.SetActive(true);
            btn_Board.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            highLight.SetActive(false);
            btn_Board.SetActive(false);
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
}
