using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class playerTest : MonoBehaviourPunCallbacks
{
    public PhotonView pv = null;
    void Start()
    {
        SetParent();
        SetManagerGamObject();
        PlayerPositionSetting();
    }
    private void SetParent()
    {
        pv.RPC("PunPPCSetParent", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void PunPPCSetParent()
    {
        transform.SetParent(NetWorkPlayerSpawnManager.instance.spawnParent);
    }
    private void SetManagerGamObject()
    {
        GameObject[] objectArray = NetWorkPlayerSpawnManager.instance.playerArray;
        for (int i = 0; i < objectArray.Length; i++)
        {
            if (objectArray[i] == gameObject)
            {
                Debug.Log("같은거 넣어짐");
                break;
            }
            if(i == objectArray.Length)
            {
                Debug.Log("최대인원");
            }
            if (objectArray[i] == null)
            {
                if (pv.IsMine)
                {
                    NetWorkPlayerSpawnManager.instance.playerNumber = i;
                }
                pv.RPC("PunPRCSetGameObject", RpcTarget.AllBuffered, i);
                break;
            }
        }
    }
    [PunRPC]
    private void PunPRCSetGameObject(int num)
    {
        NetWorkPlayerSpawnManager.instance.playerArray[num] = gameObject;
    }
    private void PlayerPositionSetting()
    {
        if (NetWorkPlayerSpawnManager.instance.spawnParent.childCount == 1)
        {
            SetTransform(0);
        }
        else
        {
            SetTransform(1);
        }
    }
    private void SetTransform(int num)
    {
        transform.position = NetWorkPlayerSpawnManager.instance.pos[num].position;
    }
}
