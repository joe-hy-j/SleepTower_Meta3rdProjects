using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable10Sec : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableProcess(10.0f));   
    }

    IEnumerator DisableProcess(float seconds)
    {
        target.SetActive(true);
        yield return new WaitForSeconds(seconds);
        target.SetActive(false);
    }
}
