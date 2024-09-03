using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillowGameManager : MonoBehaviour
{
    enum PillowType
    {
        white,
        yellow,
        blue,
        green,
        maxCount
    }
    
    struct Pillow
    {
        public PillowType color;
        public GameObject pillow;

        public Pillow(PillowType color, GameObject pillow)
        {
            this.color = color;
            this.pillow = pillow;
        }
    }

    //Pillow�� �����ϱ� ���� ����Ʈ
    List<Pillow> pillows;

    //pillow�� ����� ���� prefab
    public GameObject pillowFactory;

    [SerializeField]
    //�Ͷ߷��� �Ǵ� �谳 ����
    PillowType removePillowColor;

    //���� pillow�� ������ �����ϴ� ����
    [SerializeField]
    Dictionary<PillowType, int> pillowCount;

    public Text timeText;
    public Text removeColorText;
    public Button startButton;


    // �÷��̾ ȭ���� �������� üũ
    bool userInputTrigger = false;

    //���� user�� ���� ������ üũ
    public bool isPlaying = false;

    //�� ��° ��������
    int gameRound = 0;

    public Material[] mat = new Material[4];

    private void Update()
    {
        if (userInputTrigger)
        {
            userInputTrigger = false;
            if (CheckPillowClear())
            {
                //������ �̱�
                gameRound++;
                isPlaying = false;
                RemovePillows();

                if(gameRound < 2)
                {
                    RestartGame();
                }
                else
                {
                    EndGame();
                }
            }
        }
    }
    public void StartGame()
    {
        startButton.interactable = false;
        StartCoroutine(StartGameProcess());
    }

    void InitializeGame()
    {
        pillows = new List<Pillow>();

        removePillowColor = (PillowType)Random.Range(0, 4);
        removeColorText.text = removePillowColor.ToString();

        //���򺰷� ���� pillow�� ������ random���� ���Ѵ�. 
        pillowCount = new Dictionary<PillowType, int>();
        for (int i = 0; i < (int)PillowType.maxCount; i++)
        {
            pillowCount.Add((PillowType)i, Random.Range(3, 6));
            //print(i + ", " + pillowCount[(PillowType)i]);
        }
        //pillowCount �� ��ųʸ� i�� ��Ÿ���� �� -> 0, 1, 2, 3
        for (int i = 0; i < pillowCount.Count; i++)
        {
            //j�� ��Ÿ���� ��: �� ���򺰷� ���� �谳 ����
            for(int j = 0; j < pillowCount[(PillowType)i]; j++)
            {
                CreatePillow((PillowType)i);
            }
        }
        isPlaying = true;
    }

    IEnumerator StartGameProcess()
    {
        timeText.text = "3";
        yield return new WaitForSeconds(1);
        timeText.text = "2";
        yield return new WaitForSeconds(1);
        timeText.text = "1";
        yield return new WaitForSeconds(1);
        timeText.text = "";
        InitializeGame();
    }

    //��Ը� �����Ѵ�.
    void CreatePillow(PillowType color)
    {
        Pillow newPillow = new Pillow(color, Instantiate(pillowFactory, new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-6.0f,6.0f),0),Quaternion.identity));
        newPillow.pillow.GetComponent<Renderer>().material = mat[(int)color];
        pillows.Add(newPillow);
    }

    //���־� �ϴ� �谳�� �� ���ݴ��� Ȯ���ϴ� �Լ� ������ true�� ��ȯ�Ѵ�.
    public bool CheckPillowClear()
    {
        for(int i = 0; i< pillows.Count; i++)
        {
            if (pillows[i].color == removePillowColor)
                return false;
        }
        return true;
    }

    //������ pillow�� ���ش�.
    public bool RemovePillow(GameObject pillow)
    {
        for (int i = 0; i < pillows.Count; i++)
        {
            if (Object.ReferenceEquals(pillows[i].pillow, pillow))
            {
                pillows.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    void RestartGame()
    {
        StartCoroutine(RestartProcess());
    }

    void EndGame()
    {
        StartCoroutine(EndGameProcess());
    }
    
    void RemovePillows()
    {
        for(int i =0; i< pillows.Count;i++)
        {
            Destroy(pillows[i].pillow);
        }
    }
    IEnumerator RestartProcess()
    {
        timeText.text = "���� ����!";
        yield return new WaitForSeconds(1);
        timeText.text = "";
        StartGame();
    }

    IEnumerator EndGameProcess()
    {
        timeText.text = "���� ����...";
        yield return new WaitForSeconds(1);
        timeText.text = "";
        startButton.interactable = true;
    }

    public void OnClick()
    {
        userInputTrigger = true;
    }
}
