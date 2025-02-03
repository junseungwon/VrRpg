using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public SpriteRenderer SR;
    public GameObject spawner;
    private void Start()
    {
        spawner = GameObject.FindWithTag("PlayerPos");
        PV.RPC("SetParent", RpcTarget.AllBuffered);
    }
    void Update()
    {
        if (PV.IsMine)
        {
            float axis = Input.GetAxisRaw("Horizontal");
            transform.Translate(new Vector3(axis * Time.deltaTime * 7, 0, 0));

            if (axis != 0) PV.RPC("FlipXRPC", RpcTarget.AllBuffered, axis);
            //PV.RPC("SetParent", RpcTarget.AllBuffered, spawner);
        }
    }

    [PunRPC]
    void FlipXRPC(float axis)
    {
        SR.flipX = axis == -1;
    }
    [PunRPC]
    void SetParent()
    {
        //Debug.Log(mon);
        transform.SetParent(spawner.transform);
    }
}