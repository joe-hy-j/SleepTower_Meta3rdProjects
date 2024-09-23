using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : MonoBehaviour
{
    public GameObject btn_sleep;  // ���� ��ư
    public GameObject btn_wakeUp;  // ��� ��ư
    public GameRoomManager grm;  // GameRoomManager ��ũ��Ʈ ����
    PlayerMove pm;  // PlayerMove ��ũ��Ʈ ��������;
    public GameObject player;
    public Transform sleepPos;
    public Transform wakePos;


    private void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        // ħ�� Ʈ���ſ� ���� ����� Player�϶�
        if (other.gameObject.tag == "Player")
        {
            pm = other.GetComponent<PlayerMove>();  // ���� ����� PlayerMove ������Ʈ�� �����´�
            btn_sleep.SetActive(true);  // ���� ��ư Ȱ��ȭ
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // ħ�� Ʈ���ſ��� ��� ����� Player�϶�
        if (other.gameObject.tag == "Player")
        {
            btn_sleep.SetActive(false);  // ���� ��ư ��Ȱ��ȭ
        }
    }

    // ���� ��ư ���
    public void Sleeping()
    {
        grm.sleepCount += 1;  // ���� �ڴ� �ο��� 1 ����
        pm.moveSpeed = 0;  // �÷��̾��� �������� �����

        player.transform.position = sleepPos.position;
        player.transform.rotation = sleepPos.rotation;

        btn_wakeUp.SetActive(true);  // ��� ��ư Ȱ��ȭ
        btn_sleep.SetActive(false);  // ���� ��ư ��Ȱ��ȭ
    }

    // ��� ��ư ���
    public void WakeUp()
    {
        grm.sleepCount -= 1;  // ���� �ڴ� �ο��� 1 ����

        player.transform.position = wakePos.position;
        player.transform.rotation = wakePos.rotation;

        btn_wakeUp.SetActive(false);  // ��� ��ư ��Ȱ��ȭ
        pm.moveSpeed = 5;  // �÷��̾� ������ ����ȭ
    }
}
