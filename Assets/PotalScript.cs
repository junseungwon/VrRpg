using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PotalScript : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("3");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("출력1");
        if (other.CompareTag("Player"))
        {
        Debug.Log("출력2");
            StartCoroutine(CorutineMoveSence());
        }
    }
    private void MoveSence()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator CorutineMoveSence()
    {
        yield return null;
        MoveSence();
    }
}
