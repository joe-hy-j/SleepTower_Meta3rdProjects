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

    //Pillow를 관리하기 위한 리스트
    List<Pillow> pillows;

    //pillow를 만들기 위한 prefab
    public GameObject pillowFactory;

    [SerializeField]
    //터뜨려야 되는 배개 색깔
    PillowType removePillowColor;

    //만들 pillow의 갯수를 저장하는 변수
    [SerializeField]
    Dictionary<PillowType, int> pillowCount;

    public Text timeText;
    public Text removeColorText;
    public Button startButton;


    // 플레이어가 화면을 눌렀는지 체크
    bool userInputTrigger = false;

    //현재 user가 게임 중인지 체크
    public bool isPlaying = false;

    //몇 번째 게임인지
    int gameRound = 0;

    public Material[] mat = new Material[4];

    private void Update()
    {
        if (userInputTrigger)
        {
            userInputTrigger = false;
            if (CheckPillowClear())
            {
                //게임을 이김
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

        //색깔별로 만들 pillow의 갯수를 random으로 정한다. 
        pillowCount = new Dictionary<PillowType, int>();
        for (int i = 0; i < (int)PillowType.maxCount; i++)
        {
            pillowCount.Add((PillowType)i, Random.Range(3, 6));
            //print(i + ", " + pillowCount[(PillowType)i]);
        }
        //pillowCount 는 딕셔너리 i가 나타내는 것 -> 0, 1, 2, 3
        for (int i = 0; i < pillowCount.Count; i++)
        {
            //j가 나타내는 것: 각 색깔별로 만들 배개 갯수
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

    //배게를 생성한다.
    void CreatePillow(PillowType color)
    {
        Pillow newPillow = new Pillow(color, Instantiate(pillowFactory, new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-6.0f,6.0f),0),Quaternion.identity));
        newPillow.pillow.GetComponent<Renderer>().material = mat[(int)color];
        pillows.Add(newPillow);
    }

    //없애야 하는 배개를 다 없앴는지 확인하는 함수 없으면 true를 반환한다.
    public bool CheckPillowClear()
    {
        for(int i = 0; i< pillows.Count; i++)
        {
            if (pillows[i].color == removePillowColor)
                return false;
        }
        return true;
    }

    //눌려진 pillow를 없앤다.
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
        timeText.text = "다음 라운드!";
        yield return new WaitForSeconds(1);
        timeText.text = "";
        StartGame();
    }

    IEnumerator EndGameProcess()
    {
        timeText.text = "게임 종료...";
        yield return new WaitForSeconds(1);
        timeText.text = "";
        startButton.interactable = true;
    }

    public void OnClick()
    {
        userInputTrigger = true;
    }
}
