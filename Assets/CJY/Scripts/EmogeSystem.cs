using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmogeSystem : MonoBehaviour
{
    public List<GameObject> emoges;  // �̸�Ƽ�ܵ��� ���� ����Ʈ ����
    public GameObject emoGroup;  // �̸�Ƽ�ܵ��� �ڽ����� ���� �θ� ��ü ����

    float currentTime = 0;  // �ð��� ����ð�
    bool onEmo = false;  // �̸�Ƽ�� Ȱ��ȭ ����

    void Start()
    {
        
    }

    void Update()
    {
        // �̸�Ƽ���� Ȱ��ȭ�Ǿ� �������
        if(onEmo)
        {
            currentTime += Time.deltaTime;  // �ð��� ����
            // �ð��ʰ� 3�� ����� ���
            if (currentTime >= 3)
            {
                emoGroup.SetActive(false);  // �̸�Ƽ�� �θ� ��ü ��Ȱ��ȭ (��� �̸�Ƽ�� ��Ȱ��ȭ)
                onEmo = false;  // �̸�Ƽ�� ��Ȱ��ȭ ����
                currentTime = 0;  // �ð��� �ʱ�ȭ
            }
        }
    }

    public void SmileEmo()  // ���� �̸�Ƽ�� ��ư
    {
        EmoActive();  // �̸�Ƽ�� Ȱ��ȭ ����
        ActiveOnlyOneEmoge(0);  // �ش� �̸�Ƽ�ܸ� Ȱ��ȭ, ������ ��Ȱ��ȭ
    }

    public void SadEmo()  // ��� �̸�Ƽ�� ��ư
    {
        EmoActive();  // �̸�Ƽ�� Ȱ��ȭ ����
        ActiveOnlyOneEmoge(1);  // �ش� �̸�Ƽ�ܸ� Ȱ��ȭ, ������ ��Ȱ��ȭ
    }

    public void SurpEmo()  // ��� �̸�Ƽ�� ��ư
    {
        EmoActive();  // �̸�Ƽ�� Ȱ��ȭ ����
        ActiveOnlyOneEmoge(2);  // �ش� �̸�Ƽ�ܸ� Ȱ��ȭ, ������ ��Ȱ��ȭ
    }

    public void HeartEmo()  // ��Ʈ �̸�Ƽ�� ��ư
    {
        EmoActive();  // �̸�Ƽ�� Ȱ��ȭ ����
        ActiveOnlyOneEmoge(3);  // �ش� �̸�Ƽ�ܸ� Ȱ��ȭ, ������ ��Ȱ��ȭ
    }

    public void AngryEmo()  // ȭ�� �̸�Ƽ�� ��ư
    {
        EmoActive();  // �̸�Ƽ�� Ȱ��ȭ ����
        ActiveOnlyOneEmoge(4);  // �ش� �̸�Ƽ�ܸ� Ȱ��ȭ, ������ ��Ȱ��ȭ
    }

    public void QuestEmo()  // ����ǥ �̸�Ƽ�� ��ư
    {
        EmoActive();  // �̸�Ƽ�� Ȱ��ȭ ����
        ActiveOnlyOneEmoge(5);  // �ش� �̸�Ƽ�ܸ� Ȱ��ȭ, ������ ��Ȱ��ȭ
    }

    // ���ϴ� �̸�Ƽ�ܸ� Ȱ��ȭ �ϰ� �������� ��Ȱ��ȭ ��Ű�� �Լ�
    void ActiveOnlyOneEmoge(int WantToActive)  // WantToActive = Ȱ��ȭ �ϰ��� �ϴ� �̸�Ƽ���� ����Ʈ �ε��� ����
    {
        for (int i = 0; i < emoges.Count; i++)  // ����� ��� �̸�Ƽ���� ����ŭ ���
        {
            if(i == WantToActive)  // ���ϴ� �̸�Ƽ���� �ε��� ���ڰ� i ���
            {
                emoges[i].SetActive(true);  // i �� �ش��ϴ� �ε����� �̸�Ƽ�ܸ� Ȱ��ȭ
            }
            else
            {
                emoges[i].SetActive(false);  // i �� ���� ������ �ε����� �̸�Ƽ�ܵ��� ��Ȱ��ȭ
            }
        }
    }

    void EmoActive()  // �̸�Ƽ���� Ȱ��ȭ �����϶� ȣ���� �Լ�
    {
        emoGroup.SetActive(true);  // �̸�Ƽ�ܵ��� �θ� ��ü Ȱ��ȭ
        onEmo = true;  // �̸�Ƽ�� Ȱ��ȭ ����
        currentTime = 0;  // �ð��� �ʱ�ȭ
    }
}
