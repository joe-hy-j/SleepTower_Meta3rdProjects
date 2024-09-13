using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSelect : MonoBehaviour
{
    public GameObject photoSelecter;
    bool isOn;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnSelecter()
    {
        isOn = true;
        photoSelecter.SetActive(true);
    }

    public void CloseSelecter()
    {
        if (isOn)
        {
            photoSelecter.SetActive(false);
            isOn = false;
        }
    }
}
