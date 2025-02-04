using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseData : MonoBehaviour
{
    PlayerInformation useData;
    public List<Item> asset;
    // Start is called before the first frame update
    void Start()
    {
        useData =FindObjectOfType<PlayerInformation>();
        Debug.Log(useDB.instance.getMoney());
        useDB.instance.setMoney(100);
        Debug.Log(useDB.instance.getMoney());
        //useData.SetEquipItem();
        //asset = useData.EquipItems;
        //Debug.Log(asset.Count);
        //Debug.Log(useData.EquipItems);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
