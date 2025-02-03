using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    static string nextSceneString;
    public Image progressbar;
    private float loadingSpeed = 0.2f;

    public void Awake()
    {
        if (instance == null) { instance = this;}
        else { Destroy(this); } 
    }
    public void ChangeScene(string sceneName)
    {
        nextSceneString = sceneName;
        SceneManager.LoadSceneAsync("LoadingScene_R");
    }

    public void LoadSceneProgress()
    {
        StartCoroutine(LoadSceneProgressCor());
    }
    public IEnumerator LoadSceneProgressCor()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneString);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if(op.progress < 0.9f)
            {
                progressbar.fillAmount = op.progress;
            }
            else
            {
                timer += (Time.unscaledDeltaTime* loadingSpeed);
                progressbar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressbar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
