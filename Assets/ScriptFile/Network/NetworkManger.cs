using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;
using UnityEngine.UI;
public class NetworkManger : MonoBehaviourPunCallbacks
{
    [Header("Debug option")]
    [Tooltip("Debug : On/Off")]
    public bool debugmes = true;
    public GameObject buttonLobby;
    public GameObject steamVrRemove;
    string gameVersion = "1.0";
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        //Screen.SetResolution(1980, 1020, false);
        //PhotonNetwork.ConnectUsingSettings();
    }
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JoinRoom();
        }
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
            //buttonLobby.SetActive(true);
        //JoinRoom();
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("MyRoom", new RoomOptions { MaxPlayers = 4 }, null);
    }
    public override void OnJoinedRoom()
    {
        if (debugmes)
        {
            Debug.Log("Room in");
            PhotonNetwork.LoadLevel(7);
        }
    }
    public void BtnDunGunMove()
    {
        Destroy(steamVrRemove);
        SceneManager.LoadScene(7);
    }
    private void PositionReset(Transform obj)
    {
        obj.localPosition = Vector3.zero;
    }
}
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    player.transform.GetChild(player.transform.childCount-1).GetComponent<NavMeshAgent>().enabled = false;
        //    player.transform.position = new Vector3(139.97f, 9.93f, 104.59f);
        //    for (int i = 0; i < player.transform.childCount; i++)
        //    {
        //        PositionReset(player.transform.GetChild(i));
        //    }
        //}
