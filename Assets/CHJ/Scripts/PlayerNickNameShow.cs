using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNickNameShow : MonoBehaviourPun
{
    public TMP_Text nicknameText;

    private void Start()
    {
        nicknameText.text = photonView.Owner.NickName;

        if(photonView.IsMine)
        {
            nicknameText.color = Color.green;
        }    
    }
}
