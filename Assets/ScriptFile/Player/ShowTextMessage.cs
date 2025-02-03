using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class ShowTextMessage : MonoBehaviourPunCallbacks, IPunObservable
{
    public Text myChatInput = null;
    public Button sendButton = null;
    public PhotonView pv = null;
    public Text txtMessage = null;
    public string sendMessage = null;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        myChatInput = ChatManager.instance.chatInput;
        sendButton = ChatManager.instance.sendButton;
        sendButton.onClick.AddListener(SendMessageBtnClick);
    }
    public void SendMessageBtnClick()
    {
        if (pv.IsMine)
        {
            pv.RPC("ShowMessage", RpcTarget.All, myChatInput.text);
        }
    }
    [PunRPC]
    private void ShowMessage(string send)
    {
        txtMessage.text = send;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(myChatInput.text);
        }
        else
        {
            sendMessage = (string)stream.ReceiveNext();
        }
    }
}
