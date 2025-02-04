using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPunCallbacks //, IPunObservable
{
    public static ChatManager instance = null;
    public Text chatInput = null;
    public Button sendButton = null;
    public Text[] chatText = null;
    private PhotonView pv = null;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    public void MessageSendBtn()
    {
        pv.RPC("Chatting", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName + " : " + chatInput.text);
    }
    private void SetTxtMessage(int num)
    {
        pv.RPC("PunRPCSetMessage", RpcTarget.AllBuffered, num);
    }
    [PunRPC]
    private void Chatting(string message)
    {
        bool isInput = false;
        for (int i = 0; i < chatText.Length; i++)
        {
            if (chatText[i].text == "")
            {
                isInput = true;
                chatText[i].text = message;
                break;
            }
        }
        if (!isInput)
        {
            for (int i = 1; i < chatText.Length; i++)
            {
                chatText[i - 1].text = chatText[i].text;
                chatText[chatText.Length - 1].text = message;
            }
        }
    }
}
