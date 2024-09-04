using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmogeSystem : MonoBehaviour
{
    public List<GameObject> emoges;
    public GameObject emoGroup;

    float currentTime = 0;
    bool onEmo = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(onEmo)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 3)
            {
                emoGroup.SetActive(false);
                onEmo = false;
                currentTime = 0;
            }
        }
    }

    public void SmileEmo()
    {
        EmoActive();
        ActiveOnlyOneEmoge(0);
    }

    public void SadEmo()
    {
        EmoActive();
        ActiveOnlyOneEmoge(1);
    }

    public void SurpEmo()
    {
        EmoActive();
        ActiveOnlyOneEmoge(2);
    }

    public void HeartEmo()
    {
        ActiveOnlyOneEmoge(3);
    }

    public void AngryEmo()
    {
        EmoActive();
        ActiveOnlyOneEmoge(4);
    }

    public void QuestEmo()
    {
        EmoActive();
        ActiveOnlyOneEmoge(5);
    }

    void ActiveOnlyOneEmoge(int WantToActive)
    {
        for (int i = 0; i < emoges.Count; i++)
        {
            if(i == WantToActive)
            {
                emoges[i].SetActive(true);
            }
            else
            {
                emoges[i].SetActive(false);
            }
        }
    }

    void EmoActive()
    {
        emoGroup.SetActive(true);
        onEmo = true;
        currentTime = 0;
    }
}
