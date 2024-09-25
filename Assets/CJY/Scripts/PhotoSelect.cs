using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSelect : MonoBehaviourPun
{
    public GameObject photoSelecter;
    public GameObject imageSlot;
    public Sprite[] images;
    Image image;


    void Start()
    {
        image = imageSlot.GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void ImageUpload1()
    {
        photonView.RPC(nameof(RpcImageUpload), RpcTarget.All, 0);
    }
    
    public void ImageUpload2()
    {
        photonView.RPC(nameof(RpcImageUpload), RpcTarget.All, 1);
    }

    public void ImageUpload3()
    {
        photonView.RPC(nameof(RpcImageUpload), RpcTarget.All, 2);
    }

    public void ImageUpload4()
    {
        photonView.RPC(nameof(RpcImageUpload), RpcTarget.All, 3);
    }

    [PunRPC]
    void RpcImageUpload(int index)
    {
        image.sprite = images[index];
    }

    public void OnSelecter()
    {
        photoSelecter.SetActive(true);
    }

    public void CloseSelecter()
    {
        photoSelecter.SetActive(false);
    }
}
