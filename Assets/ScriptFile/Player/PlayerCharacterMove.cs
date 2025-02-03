using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class PlayerCharacterMove : MonoBehaviourPunCallbacks
{
    private Animator anim;
    private NavMeshAgent navMeshAgent;
    private Transform playerPosition;
    private PhotonView pv;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        playerPosition = GameManagers.instance.followPosition;
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetParent();
        SetManagerGamObject();
    }
    public void Moving()
    {
        StartCoroutine(CoruntineMove());
    }
    private void AnimationSetBool(string animName, bool result)
    {
        anim.SetBool(animName, result);
    }
    private IEnumerator CoruntineMove()
    {
        bool isbool = false;
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);
        while (!isbool)
        {
            float distance = Vector3.Distance(playerPosition.position, transform.position);
            if (distance > 1f)
            {
                navMeshAgent.SetDestination(playerPosition.position);
                AnimationSetBool("IsRun", true);
            }
            else
            {
                AnimationSetBool("IsRun", false);
                AnimationSetBool("IsWait", true);
                isbool = true;
            }
            yield return waitTime;
        }
    }
    private void SetManagerGamObject()
    {
        GameObject[] objectArray = SpawnPlayer.instance.playerArray;
        for (int i = 0; i < objectArray.Length; i++)
        {
            if (objectArray[i] == gameObject)
            {
                Debug.Log("같은거 넣어짐");
                break;
            }
            if (i == objectArray.Length)
            {
                Debug.Log("최대인원");
            }
            if (objectArray[i] == null)
            {
                if (pv.IsMine)
                {
                    SpawnPlayer.instance.playerNumber = i;
                }
                pv.RPC("PunPRCSetGameObject", RpcTarget.AllBuffered, i);
                break;
            }
        }
    }
    [PunRPC]
    private void PunPRCSetGameObject(int num)
    {
        SpawnPlayer.instance.playerArray[num] = gameObject;
    }
    private void SetParent()
    {
        pv.RPC("PunPPCSetParent", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void PunPPCSetParent()
    {
        transform.SetParent(SpawnPlayer.instance.spawnParent);
    }
}
