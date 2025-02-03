using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("작동중");
            PlayerInformManager.instance.DownPlayerHp(30);
            Debug.Log(PlayerInformManager.instance.playerHp);
        }
    }
}
