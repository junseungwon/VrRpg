using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNormalBlade : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CorutineNormalBlade());
    }
    IEnumerator CorutineNormalBlade()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
