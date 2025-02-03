using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CorutineShoutWave());
    }
    IEnumerator CorutineShoutWave()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
