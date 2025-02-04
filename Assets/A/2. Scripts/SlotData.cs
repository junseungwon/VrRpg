using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public Item(string _Type, string _Name, bool _isHave, int _Price, Sprite _Image, GameObject _Obj3d,int _Damage)
    { Type = _Type; Name = _Name; isHave = _isHave; Image = _Image; Price = _Price; Obj3d = _Obj3d; Damage = _Damage; }

    public GameObject Obj3d;
    public string Type, Name;
    public bool isHave;
    public Sprite Image;
    public int Price, Damage;
}

public class SlotData : MonoBehaviour
{
    private List<Item> AllItems, CurItems;

    [Header("Sprites Item")]
    public Sprite[] armorSpirtes;
    public Sprite[] weaponSpirtes;
    public Sprite[] etcSpirtes;

    [Header("Prefabs")]
    public GameObject[] armor3dPrefabs;
    public GameObject[] Weapon3dPrefabs;
    public GameObject[] etc3dPrefabs;

    public static SlotData instance;

    public List<Item> GetItems(bool have)
    {
        AllItemsInit();
        List<Item> tempList = new List<Item>();
        tempList = AllItems.FindAll(x => x.isHave == have);
        return tempList;
    }
    public void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(instance); }
        else { Destroy(this); }

        
    }
    public void Start()
    {
        AllItemsInit();
    }
    public void AllItemsInit()
    {
        AllItems = new List<Item>();

        int[] weaponPrice = new int[11]{ 10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000, 110000 };
        //bool[] haveWeapon = new bool[11] {true, false, false, false, false, false, false, false, false, false, false};
        bool[] haveWeapon = useDB.instance.getWp();
        int[] weaponDamage = new int[11] { 10,20,30,40,50,60,70,80,90,100,110};


        int[] armorPrice = new int[5] { 10000, 20000, 30000, 40000, 50000 };
        //bool[] haveArmor = new bool[5] { true, false, false, false, false };
        bool[] haveArmor = useDB.instance.getChar();
        int[] armorDamage = new int[11] { 0,0,0,0,0,0,0,0,0,0,0 };



        for (int i = 0; i < haveWeapon.Length; i++)
        {
            Item item = new Item("WEAPON", string.Format(("WP{0}"), i + 1), haveWeapon[i], weaponPrice[i], weaponSpirtes[i], Weapon3dPrefabs[i], weaponDamage[i]);
            AllItems.Add(item);
        }
        for (int i = 0; i < haveArmor.Length; i++)
        {
            Item item = new Item("ARMOR", string.Format(("CHAR{0}"), i + 1), haveArmor[i], armorPrice[i], armorSpirtes[i], armor3dPrefabs[i],armorDamage[i]);
            AllItems.Add(item);
        }
        for (int i = 0; i < etcSpirtes.Length; i++)
        {
            Item item = new Item("ETC", string.Format(("ETC{0}"), i+1), true, 10000, etcSpirtes[i], etc3dPrefabs[0], 0);
            AllItems.Add(item);
        }
        //무기 11개, 옷 5개, 장신구 0개, 외형 0개, 기타 0개
    }
}
