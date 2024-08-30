using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : MonoBehaviour
{
    public GameObject btn_sleep;  // ���� ��ư
    public GameObject btn_wakeUp;  // ��� ��ư
    public GameRoomManager grm;
    PlayerMove pm;


    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // ħ�� Ʈ���ſ� Player�� ���� ���
        if(other.gameObject.tag == "Player")
        {
            pm = other.GetComponent<PlayerMove>();  // ���� ����� PlayerMove ������Ʈ�� �����´�
            btn_sleep.SetActive(true);  // ���� ��ư Ȱ��ȭ
        }
        else
        {
            btn_sleep.SetActive(false);  // ���� ��ư ��Ȱ��ȭ
        }
    }

    // ���� ��ư ���
    public void Sleeping()
    {
        pm.moveSpeed = 0;  // �÷��̾��� �������� �����
        grm.sleepCount += 1;  // ���� �ڴ� �ο��� 1 ����
        btn_wakeUp.SetActive(true);  // ��� ��ư Ȱ��ȭ
        btn_sleep.SetActive(false);  // ���� ��ư ��Ȱ��ȭ
    }

    // ��� ��ư ���
    public void WakeUp()
    {
        grm.sleepCount -= 1;  // ���� �ڴ� �ο��� 1 ����
        btn_wakeUp.SetActive(false);  // ��� ��ư ��Ȱ��ȭ
        pm.moveSpeed = 5;  // �÷��̾� ������ ����ȭ
    }
}
