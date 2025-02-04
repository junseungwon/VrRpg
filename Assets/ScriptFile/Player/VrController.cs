using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class VrController : MonoBehaviour
{
    public Material[] m_a = new Material[16];
    public GameObject bladeOriginObject;
    public GameObject blade;
    public Transform bladePos;
    public Transform bladeVector;
    public SteamVR_Action_Boolean grip;
    public SteamVR_Action_Boolean bladeGrip;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHand;
    private SphereCollider rightCol;
    private SphereCollider leftCol;
    public bool playBlade = true;
    // Start is called before the first frame update
    private void Awake()
    {
        //blade = Instantiate(bladeOriginObject);
        int materialColorR = Random.Range(0,15);
        blade.GetComponent<MeshRenderer>().material = m_a[materialColorR];
        blade.transform.position = Vector3.zero;
    }
    void Start()
    {
        leftCol = leftHand.GetComponent<SphereCollider>();
        rightCol = rightHand.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Grip(SteamVR_Input_Sources.LeftHand, leftCol, leftHand);
        Grip(SteamVR_Input_Sources.RightHand, rightCol, rightHand);
        if (playBlade == true)
        {
            Blade(SteamVR_Input_Sources.RightHand);
        }
    }
    private void Grip(SteamVR_Input_Sources hand, SphereCollider col, GameObject obj)
    {
        if (grip.GetStateDown(hand))
        {
            if (obj.transform.GetChild(0).GetChild(2).childCount > 0)
            {
                obj.transform.GetChild(0).GetChild(2).GetChild(0).SetParent(null);
                col.enabled = false;
                return;
            }
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }
    private void Blade(SteamVR_Input_Sources hand)
    {
        if (bladeGrip.GetStateDown(hand))
        {
            GameObject obj = Instantiate(blade);
            obj.transform.SetParent(bladePos);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = new Quaternion(0,0,0,1);
            obj.transform.SetParent(null);
            obj.GetComponent<Rigidbody>().AddForce((bladeVector.position-bladePos.position) * 500f);
        }
    }
}
