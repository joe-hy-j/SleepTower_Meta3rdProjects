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
        //혹시몰라 빈도 수 높임
        // RPC 보내는 빈도 설정
        PhotonNetwork.SendRate = 60;
        // OnPhotonSerializeView 보내고 받고 하는 빈도 설정
        PhotonNetwork.SerializationRate = 60;
      
      // PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform spawnCenter;
    public Vector3[] spawnPos;
    void SetSpawnPos()
    {
        int maxPlayer = 10;
        //최대 인원만큼 spawnPos 의 공간을 할당
        spawnPos = new Vector3[maxPlayer];
        float angle = 360 / maxPlayer;

        // spawnPos 간의 간격
        // maxPlayer 만큼 반복
        for (int i = 0; i < maxPlayer; i++)
        {
            // spawnCenter 회전 (i*angle) 만큼 
            spawnCenter.eulerAngles = new Vector3(0, i * angle, 0);
            // spawnCenter 앞방향으로 2만큼 떨어진 위치 구하자
            spawnPos[i] = spawnCenter.position + spawnCenter.forward * 2;

            // Player 생성
            GameObject Player = PhotonNetwork.Instantiate("Player", spawnPos[i], Quaternion.identity);

            // 생성된 Player를 위에서 구한 위치에 놓자

        }


    }
}
