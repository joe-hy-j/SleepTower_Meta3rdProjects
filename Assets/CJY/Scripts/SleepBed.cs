using Photon.Pun;
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

    bool isSleeping;
    private void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        // ħ�� Ʈ���ſ� ���� ����� Player�϶�
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                SetPlayer(other.gameObject);  // ���� ����� PlayerMove ������Ʈ�� �����´�
                btn_sleep.SetActive(true);  // ���� ��ư Ȱ��ȭ
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // ħ�� Ʈ���ſ��� ��� ����� Player�϶�
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                btn_sleep.SetActive(false);  // ���� ��ư ��Ȱ��ȭ
            }
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
        pm = player.GetComponent<PlayerMove>();
    }

    // ���� ��ư ���
    public void Sleeping()
    {
        grm.ChangeSleepCount(1);  // ���� �ڴ� �ο��� 1 ����
        pm.SetLying(); // �������̰� �����.

        player.transform.position = sleepPos.position;
        player.transform.rotation = sleepPos.rotation;

        btn_wakeUp.SetActive(true);  // ��� ��ư Ȱ��ȭ
        btn_sleep.SetActive(false);  // ���� ��ư ��Ȱ��ȭ

        BedManager.instance.SetSleepBedDicValue(gameObject.name, true);
        isSleeping = true;
    }

    // ��� ��ư ���
    public void WakeUp()
    {
        grm.ChangeSleepCount(-1);  // ���� �ڴ� �ο��� 1 ����

        player.transform.position = wakePos.position;
        player.transform.rotation = wakePos.rotation;

        btn_wakeUp.SetActive(false);  // ��� ��ư ��Ȱ��ȭ
        pm.SetWakeUp();

        BedManager.instance.SetSleepBedDicValue(gameObject.name, false);

        isSleeping = false;
    }
}
