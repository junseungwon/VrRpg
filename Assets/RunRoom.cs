using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunRoom : MonoBehaviour
{
    public int moveStage = 0;
    // Start is called before the first frame update
   public void moveBossRoom()
    {
        PlayerInformManager.instance.BossRoom();
    }
    public void moveShopStage()
    {
        PlayerInformManager.instance.MoveShopPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (moveStage==1&&other.CompareTag("Player"))
        {
            moveBossRoom();
        }
        else if (moveStage == 2 && other.CompareTag("Player"))
        {
            Debug.Log("이동");
            moveShopStage();
        }
    }
}
