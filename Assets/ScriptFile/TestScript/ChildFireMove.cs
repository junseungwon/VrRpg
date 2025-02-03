using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFireMove : MonoBehaviour
{
    private Rigidbody rd;
    private Vector3 pos;
    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, FireSpawner.instance.originFire.transform.position, 0.01f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OriginFire"))
        {
            Debug.Log("넣어짐");
            FireSpawner.instance.InsertChildFire(gameObject);
        }
    }

}
