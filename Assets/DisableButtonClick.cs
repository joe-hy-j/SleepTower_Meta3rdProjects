using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButtonClick : MonoBehaviour
{
    public void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
