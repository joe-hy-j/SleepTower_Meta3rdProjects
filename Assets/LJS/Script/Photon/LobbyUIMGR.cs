using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LobbyUIMGR : MonoBehaviour
{
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;
    public List<string> myList = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

    public Button CreateRoomBtn;
    public GameObject RoomCreate;
    public Button exitBtn;

    public GameObject img1;
    public Button btn1;
    public GameObject img2;
    public Button btn2;
    public GameObject img3;
    public Button btn3;
    public GameObject img4;
    public Button btn4;

    public GameObject PopUpUiCanvas;


    bool createComplete = false;

    int currentPage = 1, maxPage, multiple;

    // Start is called before the first frame update
    void Start()
    {
  
        // 최대페이지
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        RoomCreate.SetActive(false);

    }

    // ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
    public void BtnClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else print(myList[multiple + num]);

        Start();
    }

    // 방만들기 탭 클릭 시 나오는 패널 조정
    public void CreateRoomBtnClick()
    {
        RoomCreate.SetActive(true);
        createComplete = true;

        if(createComplete)
        {
        exitBtn.GetComponent<Button>().interactable = true;
           
        }
        
    }

    // 방만들기 후 패널 비활성화
    public void exitBtnClick()
    {
        if (exitBtn.GetComponent<Button>().interactable)
        {
           
            RoomCreate.SetActive(false);
            createComplete = false;
        }
    }

    public void OnBtn1Click()
    {
        btn1.interactable = false;  // 버튼 비활성화
        img1.SetActive(false);      // 해당 이미지 비활성화
    }

    public void OnBtn2Click()
    {
        btn2.interactable = false;
        img2.SetActive(false);
    }

    public void OnBtn3Click()
    {
        btn3.interactable = false;
        img3.SetActive(false);
    }

    public void OnBtn4Click()
    {
        btn4.interactable = false;
        img4.SetActive(false);
        PopUpUiCanvas.SetActive(false);
    }


}
