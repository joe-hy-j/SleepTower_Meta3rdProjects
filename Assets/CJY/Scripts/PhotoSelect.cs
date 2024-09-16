using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSelect : MonoBehaviour
{
    public GameObject photoSelecter;
    public GameObject imageSlot;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
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
        image.sprite = image1;
    }

    public void ImageUpload2()
    {
        image.sprite = image2;
    }

    public void ImageUpload3()
    {
        image.sprite = image3;
    }

    public void ImageUpload4()
    {
        image.sprite = image4;
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
