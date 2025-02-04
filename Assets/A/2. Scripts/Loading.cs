using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image _progressbar;
    void Start()
    {
        GameManager.instance.progressbar = _progressbar;
        GameManager.instance.LoadSceneProgress();
    }
}
