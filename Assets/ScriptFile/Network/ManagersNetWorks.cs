using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ManagersNetWorks : MonoBehaviourPunCallbacks
{
    public static ManagersNetWorks instance;
    public GameObject spawnpos;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity);
    }
}
