using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3.0f;
    public Transform startBlock;

    float yDistance;
    Vector3 destination;

    private void Start()
    {
        yDistance = transform.position.y - startBlock.position.y;
    }
    void Update()
    {
        if(target!= null)
        {
            destination = new Vector3(transform.position.x, target.position.y + yDistance , transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, moveSpeed * Time.deltaTime);
        }
    }

    public void SetLookTarget(Transform target)
    {
        this.target = target;
    }
}
