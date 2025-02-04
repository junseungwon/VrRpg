using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SubPlayerCharacter : MonoBehaviour
{
    public PhotonView pv;
    Vector3 curPos;
    Quaternion curRot;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (true)
        {

        }
        pv = GetComponent<PhotonView>();
        SetManagerGamObject();
        SetParent();
        SpawnPlayer.instance.SetFalseCamer();
    }

    void Update()
    {
        float r = Input.GetAxis("Horizontal");
        float t = Input.GetAxis("Vertical");
        if (pv.IsMine)
        {
            transform.Translate(Vector3.forward * t * Time.deltaTime * 30.0f);
            transform.Rotate(Vector3.up * r * Time.deltaTime * 80.0f);
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 30.0f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, curRot, 1);
            //transform.localScale = new Vector3(3f, 3f, 3f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(PhotonNetwork.LocalPlayer.NickName);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
        }
    }
    private void SetManagerGamObject()
    {
        GameObject[] objectArray = SpawnPlayer.instance.playerArray;
        for (int i = 0; i < objectArray.Length; i++)
        {
            if (objectArray[i] == gameObject)
            {
                Debug.Log("같은거 넣어짐");
                break;
            }
            if (i == objectArray.Length)
            {
                Debug.Log("최대인원");
            }
            if (objectArray[i] == null)
            {
                if (pv.IsMine)
                {
                    SpawnPlayer.instance.playerNumber = i;
                    Debug.Log(SpawnPlayer.instance.playerNumber);
                }
                pv.RPC("PunPRCSetGameObject", RpcTarget.AllBuffered, i);
                break;
            }
        }
    }
    [PunRPC]
    private void PunPRCSetGameObject(int num)
    {
        SpawnPlayer.instance.playerArray[num] = gameObject;
    }
    private void SetParent()
    {
        pv.RPC("PunPPCSetParent", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void PunPPCSetParent()
    {
        
        transform.SetParent(SpawnPlayer.instance.spawn3dParent);
        transform.localPosition = Vector3.zero;
        this.gameObject.name = pv.ViewID.ToString();
    }
}
