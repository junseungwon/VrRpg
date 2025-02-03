using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;
using UnityEngine.UI;
public class NetworkTest : MonoBehaviourPunCallbacks
{
    public InputField inputField;
    public GameObject obj;
    [Header("Debug option")]
    [Tooltip("Debug : On/Off")]
    public bool debugmes = true;
    public GameObject buttonLobby;
    string gameVersion = "1.0";
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        //Screen.SetResolution(1980, 1020, false);
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        if (debugmes)
        {
            Debug.Log("Connect");
        }
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        if (debugmes)
        {
            Debug.Log("Lobby");
        }
        buttonLobby.SetActive(true);
    }
    public void JoinRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = inputField.text;
        PhotonNetwork.JoinOrCreateRoom("MyRoom", new RoomOptions { MaxPlayers = 4 }, null);
    }
    public override void OnJoinedRoom()
    {
        if (debugmes)
        {
            Debug.Log("Room in");
        }
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("내가 메인이다");
            PhotonNetwork.LoadLevel(1);
        }
    }
}
