using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
