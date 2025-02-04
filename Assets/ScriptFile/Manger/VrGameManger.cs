using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VrGameManger : MonoBehaviour
{
    public static VrGameManger instance;
    public GameObject player;
    public Text textTime;
    public Text textScore;
    private float GameTime;
    private int Min;
    private int Sec;
    private int intTimer;
    private int score = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    private void Start()
    {
        SetTimer();
    }
    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    public void UpScore(int num)
    {
        score += num;
        ScoreTextChange();
    }
    public void ResetScore()
    {
        score = 0;
    }
    private void ScoreTextChange()
    {
        textScore.text = score.ToString();
    }
    private void SetTimer()
    {
       intTimer= (5) * 60;
    }
    private void Timer()
    {
        GameTime += Time.deltaTime;
        Min = (int)(intTimer - GameTime) % 3600 / 60;
        Sec = (int)(intTimer - GameTime) % 3600 % 60;
        textTime.text = string.Format("{0:D2}:{1:D2}", Min, Sec);

    }
}
