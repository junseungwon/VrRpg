using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 resetPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
    }
    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        transform.position = resetPos;
    }
    private void Update()
    {
            transform.Rotate(new Vector3(1, 1, 1) * 20.0f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("작동중");
            PlayerInformManager.instance.DownPlayerHp(50);
            Debug.Log(PlayerInformManager.instance.playerHp);
        }
    }
}
