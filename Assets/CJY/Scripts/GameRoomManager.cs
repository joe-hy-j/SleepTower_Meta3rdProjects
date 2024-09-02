using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoomManager : MonoBehaviour
{
    public int sleepCount = 0;  // ���� �ڴ� �ο� ��
    public int needSleepCount = 1;  // �ʿ��� �� �ο�
    public GameObject img_TimerSetting;  // �˶� �ð����� ȭ��
    public GameObject img_TimerUI;  // �˶� ȭ��
    public bool isSleepAll = false;  // �ʿ��� �ο����� ���� �Ǿ�����


    void Start()
    {
        isSleepAll = false;  // ���� �ʱ�ȭ
    }

    void Update()
    {
        // ���� �ڴ� �ο� ���� �ʿ��� �� �ο� ���� ������ ���
        if (sleepCount >= needSleepCount)
        {
            isSleepAll = true;  // �ʿ��� �ο��� ����

            // ���� �ڴ� �ο� ī��Ʈ�� �ʿ� �ο� ���� �ʰ��� ��� �Ѱ�ġ ����
            if (sleepCount > needSleepCount)
            {
                sleepCount = needSleepCount;
            }
        }
        // ���� �ڴ� �ο� ���� 0 ���� �۾��� ��� 0 ���� ����
        else
        {
            if (sleepCount < 0)
            {
                sleepCount = 0;
            }
        }

        // �ڴ� �ο��� �ʿ��� �ο����� ������ ���
        if(isSleepAll)
        {
            sleepCount = 0;
            img_TimerUI.SetActive(true);  // �˶� UI Ȱ��ȭ
        }
    }

    // �˶� �ð����� â ���� ��ư
    public void TimerSetting()
    {
        img_TimerSetting.SetActive(true);
    }

    // �˶� �ð����� â �ݱ� ��ư
    public void TimerSettingClose()
    {
        img_TimerSetting.SetActive(false);
    }

    // �˶� �ð� ���� �Ϸ�
    public void TimerConfirm()
    {
        
    }

    // �˶� UI
    public void StopTimer()
    {
        isSleepAll = false;  // 
        img_TimerUI.SetActive(false);  // �˶� UI ��Ȱ��ȭ
    }

    // �� ������ ��ư
    public void ExitRoom()
    {
        //SceneManager.LoadScene();
    }
}
