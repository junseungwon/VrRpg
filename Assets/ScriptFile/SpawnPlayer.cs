using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayer : MonoBehaviour
{
    public static SpawnPlayer instance = null;
    public int playerNumber = 0;
    public GameObject playerModel = null;
    public GameObject pos;
    public GameObject[] playerArray = new GameObject[15]; //플레이어들을 모으는 배열
    public PhotonView[] photonViewArray = new PhotonView[15];
    public Transform spawnParent = null; //플레이어들 부모위치
    public Transform spawn3dParent;
    public GameObject falseobj;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    void Start()
    {
        if (spawn3dParent.transform.childCount == 1)
        {
            playerModel = PhotonNetwork.Instantiate("Girl", transform.position, Quaternion.identity);
            GameManagers.instance.followPosition = GameManagers.instance.player.transform.GetChild(0).GetChild(3);
            playerModel.transform.SetParent(spawnParent);
        }
        else
        {
            playerModel = PhotonNetwork.Instantiate("SubPlayerModel", transform.position, Quaternion.identity);
        }
        //playerModel = PhotonNetwork.Instantiate("Girl", transform.position, Quaternion.identity);
        //GameManagers.instance.followPosition = GameManagers.instance.player.transform.GetChild(0).GetChild(3);
        //playerModel.transform.SetParent(spawnParent);
        //else
        //{
       // //playerModel = PhotonNetwork.Instantiate("SubPlayerModel", transform.position, Quaternion.identity);
        //GameManagers.instance.followPosition = GameManagers.instance.player.transform.GetChild(0).GetChild(3);
        //playerModel.transform.SetParent(spawn3dParent);
        //}
    }
    public void SetFalseCamer()
    {
        for (int i = 0; i < playerArray.Length; i++)
        {
            if (playerArray[i] == null)
            {
                Debug.Log("비어있음");
                break;
            }
            if (!playerArray[i].GetComponent<PhotonView>().IsMine&& playerArray[i].GetComponentInChildren<Camera>().gameObject.activeSelf)
            {
                playerArray[i].GetComponentInChildren<Camera>().gameObject.SetActive(false);
            }
        }
    }
}
