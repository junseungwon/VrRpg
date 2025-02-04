using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerInformManager : MonoBehaviour
{
    public static PlayerInformManager instance = null;
    public GameObject playerKnife = null;
    public List<Item> asset = new List<Item>();
    public Transform gripPos;
    public int plusGetMoney = 0;
    public int playerHp = 100;
    public int KnifeDamage = 50;
    public int stage = 1;
    public GameObject winText;
    public GameObject loseText;
    public GameObject bossMonster;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    private void Start()
    {
        PlayerInformation inform = FindObjectOfType<PlayerInformation>();
        playerKnife = Instantiate(inform.useKnifeModel);
        KnifeDamage = inform.useKnifeDameage;
        if (stage ==1 )
        {
            playerKnife.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            playerKnife.transform.transform.position = new Vector3(-110.18f, 43.5f, -29.5f);
            playerKnife.transform.rotation = Quaternion.Euler(-90, 90, 0);
        }
        else if (stage ==2)
        {
            playerKnife.transform.SetParent(gripPos.transform);
            playerKnife.transform.localPosition = Vector3.zero;
            playerKnife.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            playerKnife.transform.localPosition = new Vector3(-0.097f, -0.045f, -0.157f);
            playerKnife.transform.localRotation = Quaternion.Euler(12.803f, -57.767f, -0.09F);
        }
    }
    public void DownPlayerHp(int num)
    {
        playerHp -= num;
        if (playerHp <= 0)
        {

            useDB.instance.setMoney(GetMoney());
            MoveShopPlayer();
        }
    }
    public void PlusMoney(int money)
    {
        plusGetMoney += money;
    }
    public int GetMoney()
    {
        return plusGetMoney;
    }
    public void MoveShopPlayer()
    {
        SceneManager.LoadScene("StartScene_R");
    }
    public void BossRoom()
    {
        SceneManager.LoadScene("BoseScene");
    }
    public void Win()
    {
        winText.SetActive(true);
        bossMonster.SetActive(false);
        StartCoroutine(CorutineAfterMove());
    }
    IEnumerator CorutineAfterMove()
    {
        yield return new WaitForSeconds(3.0f);
        PlusMoney(100000);
        useDB.instance.setMoney(GetMoney()); ;
        MoveShopPlayer();
    }
}
