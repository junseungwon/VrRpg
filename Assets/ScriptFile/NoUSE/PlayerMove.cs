using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class PlayerMove : MonoBehaviour, IPunObservable, IOnEventCallback
{
    public Material[] materials;
    PhotonView pv;
    float hp = 100;
    Vector3 curPos;
    Quaternion curRot;
    public GameObject bullet;
    public Transform firePos;
    MeshRenderer[] renderers;
    public Image hpbar;
    public Canvas hudCanvas;
    public Text text;
    public const byte SendColorEvent = 1;
    int tankColor = 0;
    // Start is called before the first frame update

    void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            GetComponent<Renderer>().material = materials[0];
        }
        else
        {
            GetComponent<Renderer>().material = materials[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (pv.IsMine)
        {
            transform.Translate(Vector3.forward * v * Time.deltaTime * 10);
            transform.Rotate(Vector3.up * h * Time.deltaTime * 100);
            if (Input.GetMouseButtonDown(0))
            {
                pv.RPC("RPCFire", RpcTarget.All);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime * 10.0f);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(hp);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            hp = (float)stream.ReceiveNext();
        }
    }
    
    [PunRPC]
    private void RPCFire()
    {
        GameObject obj = Instantiate(bullet, firePos.position, transform.rotation);
        Destroy(obj, 5.0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="bullet")
        {
            Destroy(collision.gameObject);
            hp -= 20;
            text.text = hp.ToString();
            hpbar.GetComponent<Image>().fillAmount -= 0.2f;
            if (hp <=0)
            {
                SendEvent();
                StartCoroutine(ExplosionTank());
            }
        }
    }
    IEnumerator ExplosionTank()
    {
        TankVisble(false);
        yield return new WaitForSeconds(3.0f);
        TankVisble(true);
        hp = 100;
        text.text = hp.ToString();
        hpbar.GetComponent<Image>().fillAmount = 1.0f;
    }
    private void TankVisble(bool v)
    {
        foreach (var item in renderers)
        {
            item.enabled = v;
        }
        hudCanvas.enabled = v;
    }
    private void SendEvent()
    {
        object[] contents = new object[] { new Vector3(0, 0, 0) };
        PhotonNetwork.RaiseEvent(SendColorEvent, contents, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == SendColorEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            Vector3 temp = (Vector3)data[0];
            if (pv.IsMine)
            {
                GetComponent<Renderer>().material = materials[tankColor++ % 3];
            }
            else
            {
                print(temp);
                transform.position = temp;
            }
        }
    }
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
