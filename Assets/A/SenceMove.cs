using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SenceMove : MonoBehaviour
{
    public void Move1() {
        SceneManager.LoadScene(1);
    }
    public void MoveSHop()
    {
        SceneManager.LoadScene("StartScene_R");
    }
}
