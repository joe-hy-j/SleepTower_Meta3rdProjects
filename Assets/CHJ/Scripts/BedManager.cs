using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviourPunCallbacks
{
    // ���� ������� Bed�� �����ϴ� ��ü
    Dictionary<string, bool> SleepBedDic = new Dictionary<string, bool>();
    // key ������ Bed GameObject�� �ް�, value�� �ڰ� �ִ��� �ƴ����� �����Ѵ�.

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
        // Dictionary��  Bed1, Bed2, Bed3, Bed4 gameObject�� �ְ� ���� �ڰ� ������ false�� �ߴ�.
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
