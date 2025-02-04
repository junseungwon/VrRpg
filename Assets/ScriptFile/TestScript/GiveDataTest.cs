using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GiveDataTest : MonoBehaviour
{
    public string[] namespaceis = new string[3];
    public Text obj = null;
    public GameObject object1;
    private string data = "null";
    // Start is called before the first frame update
    void Start()
    {
        namespaceis[0] = "전승원";
        namespaceis[1] = "이다햐";
        namespaceis[2] = "아리셔";
        //PhotonNetwork.Instantiate("ChatPlayer", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    
}
