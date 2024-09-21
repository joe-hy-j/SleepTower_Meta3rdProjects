using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CushionFight : MonoBehaviourPun
{
    public GameObject cushion;  // ���� ������
    public Transform throwPos;  // ���� ��ġ
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
            // ���� �������� ���� ��ġ���� �ڽ��� ȸ�� ���� �°� �����Ѵ�.
            PhotonNetwork.Instantiate("Cushion", throwPos.position, transform.rotation);
            SoundManager.instance.PillowSoundPlay();    
        }
    }
}
