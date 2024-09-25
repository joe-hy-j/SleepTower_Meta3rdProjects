using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonSetParent : MonoBehaviour
{
    public string parentName = "Slots";

    private void Awake()
    {
        gameObject.transform.SetParent(FindInactiveObject(parentName));
    }

    Transform  FindInactiveObject(string objectName)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();

        foreach (Transform obj in objs)
        {
            if (obj.name == objectName)
                return obj;

        }
        return null;
    }
}
