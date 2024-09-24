using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviourPunCallbacks
{
    // 현재 사용중인 Bed를 관리하는 객체
    Dictionary<string, bool> SleepBedDic = new Dictionary<string, bool>();
    // key 값으로 Bed GameObject를 받고, value로 자고 있는지 아닌지를 관리한다.

    public static BedManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Dictionary에  Bed1, Bed2, Bed3, Bed4 gameObject를 넣고 현재 자고 있음을 false로 했다.
        for(int i = 1; i<=4; i++)
            SleepBedDic.Add(transform.Find("Bed" + i).gameObject.name, false);

    }

    public void SetSleepBedDicValue(string bedObject, bool isSleeping)
    {
        photonView.RPC(nameof(RpcSetSleepBedDicValue),RpcTarget.All, bedObject, isSleeping);
    }
    [PunRPC]
    void RpcSetSleepBedDicValue(string bedObject, bool isSleeping)
    {
        if (SleepBedDic.ContainsKey(bedObject))
        {
            SleepBedDic[bedObject] = isSleeping;
        }
        PrintDicValueKey();
    }
    public GameObject GetEmptyBed()
    {
        foreach (string bed in SleepBedDic.Keys)
        {
            if (!SleepBedDic[bed])
            {
                return transform.Find(bed).gameObject;
            }
        }
        return null;    
    }

    public void PrintDicValueKey()
    {
        foreach(string bed in SleepBedDic.Keys)
        {
            print(bed + ", " + SleepBedDic[bed]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (!photonView.IsMine) return;

        photonView.RPC(nameof(SetDictionary), newPlayer, SleepBedDic);

    }

    [PunRPC]
    void SetDictionary(Dictionary<string, bool> myDictionary)
    {
        SleepBedDic = myDictionary;
    }
}
