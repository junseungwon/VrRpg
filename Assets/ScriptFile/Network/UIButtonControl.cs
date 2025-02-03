using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIButtonControl : MonoBehaviour
{
    public GameObject setFalseCamera;
    public void StartButton()
    {
        Destroy(setFalseCamera);
        SceneManager.LoadScene("Lobby");
    }
}
