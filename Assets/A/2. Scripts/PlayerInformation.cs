using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public Shop shopScript;
    public List<Item> EquipItems;
    public GameObject useKnifeModel;
    public int useKnifeDameage;
    public string GetId()
    {
        string temp = "";
        temp = useDB.instance.getId();
        Debug.Log("id : "+ temp+" 가져옴");
        return temp;
    }

    public int GetMoney()
    {
        int temp = 0;
        temp = useDB.instance.getMoney();
        Debug.Log("돈 : " + temp + " 가져옴");
        return temp;
    }

    public string GetNickname()
    {
        string temp = "";
        temp = useDB.instance.getNickName();
        Debug.Log("Nickname : " + temp + " 가져옴");
        return temp;
    }

    public List<Item> GetEquipItem()
    {
        Debug.Log("EquipItems : " + EquipItems + " 가져옴");
        return EquipItems;
    }

    public void SetEquipItem()
    {
        EquipItems = shopScript.GetEquipItems();
    }
}
