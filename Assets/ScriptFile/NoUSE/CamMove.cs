using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    int dist = 5;
    int height = 3;
    public Transform player;
    Vector3 offset;
    
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
            offset = player.transform.forward * dist + Vector3.down * height;
            transform.position = Vector3.Lerp(transform.position, player.transform.position-offset, 0.05f);
            transform.LookAt(player.transform);
        }    
    }
}
