using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DebugCheckButton : MonoBehaviour
{
    public Sprite[] armorSpirtes = new Sprite[4];
    public Sprite[] weaponSpirtes = new Sprite[11];
    private int[] damageScore = new int[16];
    public GameObject[] armor3dPrefabs = new GameObject[4];
    public GameObject[] Weapon3dPrefabs = new GameObject[11];
    public Image useKnifeObject;
    public PlayerInformation inform;
    private void Start()
    {
        for (int i = 0; i < damageScore.Length; i++)
        {
            damageScore[i] = i * 10;
        }
    }
    public void BtnDunGunMove()
    {
        getKnife();
        SceneManager.LoadScene(3);
    }
    public void btclick()
    {
        Debug.Log("발생중");
    }
    private void getKnife()
    {
        inform = FindObjectOfType<PlayerInformation>();
        int getNumber = 0;
        for (int i = 0; i < armorSpirtes.Length; i++)
        {
            if (useKnifeObject.sprite.name == armorSpirtes[i].texture.name)
            {
                getNumber = i;
                inform.useKnifeModel = armor3dPrefabs[getNumber].gameObject;
                inform.useKnifeDameage = damageScore[getNumber+1];
                Debug.Log(inform.useKnifeDameage);
                break;
            }
        }
        for (int i = 0; i < weaponSpirtes.Length; i++)
        {
            if (useKnifeObject.sprite.name == weaponSpirtes[i].texture.name)
            {
                getNumber = i;
                inform.useKnifeModel = Weapon3dPrefabs[getNumber].gameObject;
                inform.useKnifeDameage = damageScore[getNumber+1];
                break;
            }
        }
        Debug.Log(getNumber);
    }
}
