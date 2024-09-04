using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public GameObject ob;

    // Start is called before the first frame update
    void Start()
    {

      
       PhotonNetwork.Instantiate("Owner", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Create()
    {
         PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

    }
}
