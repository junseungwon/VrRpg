using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerGrip : MonoBehaviour
{
    [SerializeField]
    private Transform gripPos;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WEAPON"))
        {
            Debug.Log("장착");
            other.transform.parent = gripPos;
            //other.transform.localPosition = Vector3.zero;
        }
    }
}
