using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetWorkPlayerSpawnManager : MonoBehaviourPunCallbacks
{
    public static NetWorkPlayerSpawnManager instance = null;
    public GameObject player = null; //현재 플레이어
    public int playerNumber = 0;
    public Transform[] pos = new Transform[2];
    public GameObject[] playerArray = new GameObject[15]; //플레이어들을 모으는 배열
    public PhotonView[] photonViewArray = new PhotonView[15];
    public Transform spawnParent = null; //플레이어들 부모위치
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    void Start()
    {
        player = PhotonNetwork.Instantiate("ChatPlayer", Vector3.zero, Quaternion.identity);
    }
}
