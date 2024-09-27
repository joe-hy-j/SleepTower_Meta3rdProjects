using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Disable10Sec : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableProcess(10.0f));   
    }

    private void Update()
    {
        if (!target.activeSelf) return;
        if (Input.GetMouseButtonDown(0))
        {
            target.SetActive(false);
        }

    }
    IEnumerator DisableProcess(float seconds)
    {
        target.SetActive(true);
        yield return new WaitForSeconds(seconds);
        target.SetActive(false);
    }


}
