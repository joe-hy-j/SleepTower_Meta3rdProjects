using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonPostIt : MonoBehaviourPun
{
    public TMP_InputField postitInput;

    private void Start()
    {
        postitInput.onEndEdit.AddListener(SetText);
    }

    void SetText(string s)
    {
        photonView.RPC(nameof(RpcSetText), RpcTarget.All, s);
    }

    [PunRPC]
    void RpcSetText(string s)
    {
        postitInput.text = s;
    }
}
