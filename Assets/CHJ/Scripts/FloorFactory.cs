using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFactory : MonoBehaviour
{
    public GameObject floorFactory;
    public CameraMove cam;

    public float createTime = 3.0f;
    float currentTime = 0;

    GameObject go;
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            GameObject go = Instantiate(floorFactory);
            
            cam.SetLookTarget(go.transform);
            go.transform.position = transform.position;
            transform.position += Vector3.up * go.transform.localScale.y;
            currentTime = 0;
        }
    }
}
