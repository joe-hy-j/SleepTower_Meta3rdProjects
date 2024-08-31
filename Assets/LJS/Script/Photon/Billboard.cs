using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour 
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //닉네임 방향을 카메라앞방향으로 설정
        transform.forward = Camera.main.transform.forward;
    }
}
