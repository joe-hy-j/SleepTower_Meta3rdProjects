using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionFight : MonoBehaviour
{
    public GameObject cushion;
    public Transform throwPos;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CushionThrow()
    {
        Instantiate(cushion, throwPos.position, transform.rotation);
    }
}
