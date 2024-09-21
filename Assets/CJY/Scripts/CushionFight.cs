using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CushionFight : MonoBehaviourPun
{
    public GameObject cushion;  // 베게 프리팹
    public Transform throwPos;  // 던질 위치
    public Animator anim;

    
    Transform btn_throw;

    private void Start()
    {
        if (photonView.IsMine)
        {
            btn_throw = GameObject.Find("btn_Throw").transform;
            btn_throw.GetComponent<Button>().onClick.AddListener(CushionThrow);
        }
    }
    public void CushionThrow()
    {
        if (photonView.IsMine)
        {
            anim.SetTrigger("Throw");
            // 베게 프리팹을 던질 위치에서 자신의 회전 값에 맞게 생성한다.
            PhotonNetwork.Instantiate("Cushion", throwPos.position, transform.rotation);
            SoundManager.instance.PillowSoundPlay();    
        }
    }
}
