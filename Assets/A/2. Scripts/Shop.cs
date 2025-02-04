using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot
{
    public EquipSlot(string type, Image itemSlotImage)
    {
        Type = type;
        ItemSlotImage = itemSlotImage;
    }
    public string Type;
    public Image ItemSlotImage;
}
public class Tab {
    public Tab(string tabName,Image tabSlotImage, Image tabSlotImageTextImage, Sprite[] tabOnOffSprite, Sprite[] tabOnOffTextSprite)
    {
        Name = tabName; 
        TabImage = tabSlotImage; 
        TabTextImage = tabSlotImageTextImage;
        OnSprite = tabOnOffSprite[0]; 
        OffSprite = tabOnOffSprite[1];
        OnTextSprite = tabOnOffTextSprite[0];
        OffTextSprite = tabOnOffTextSprite[1];

        TabImage.sprite = OffSprite;
    }
    public string Name;
    public Sprite OnSprite, OffSprite, OnTextSprite, OffTextSprite;
    public Image TabImage,TabTextImage;
}

public class Shop : MonoBehaviour
{
    [Header("Slot Image")]
    public Image[] SlotImages;
    public Image[] SlotItemImage;
    
    [Header("Tab Sprite")]
    public Sprite[] WeaponTabSprite;
    public Sprite[] ArmorTabSprite;
    public Sprite[] AccessoriesTabSprite;
    public Sprite[] ApearanceTabSprite;
    public Sprite[] EtcTabSprite;

    [Header("Tab Text Sprite")]
    public Sprite[] WeaponTabTextSprite;
    public Sprite[] ArmorTabTextSprite;
    public Sprite[] AccessoriesTabTextSprite;
    public Sprite[] ApearanceTabTextSprite;
    public Sprite[] EtcTabTextSprite;

    [Header("Tab Image")]
    public Image[] TabImages;
    public Image[] TabImagesTextImages;

    [Header("EquipSlots Image")]
    public Image[] EquipSlotItemImage;

    [Header("ShowSlots Image")]
    public Image[] ShowSlotItemImage;

    [Header("Slot On Off Sprite")]
    public Sprite[] SlotOnOffImages;

    [Header("Alarm Text Sprite")] 
    public Sprite AlarmTextBuyTheItemSprite;
    public Sprite AlarmTextSellTheItemSprite;

    [Header("Buy or Sell Button")]
    public GameObject buyButton;
    public GameObject sellButton;
    public Sprite buyTextSprite;
    public Sprite sellTextSprite;

    [Header("UiAnim")]
    public UiAnimation uiAnimation;

    [Header("Money")]
    public Text moneyText;

    [Header("Instantiate Transform")]
    public Transform[] itemSpawnTransform;

    private List<Item> ShopItems, InvenItems, EquipItems;
    private string curTab = "WEAPON";
    private List<Tab> Tabs;
    private List<EquipSlot> EquipSlots, ShowSlots;

    private Item SelectedItem;
    private GameObject[] showItem = new GameObject[3];
    private GameObject[] storeShowItem = new GameObject[3];
    private GameObject CharacterHandItem;
      
    private bool itemSelected;

    public bool isStore = true;

    public void Start()
    {
        MakeTab();
        MakeEquipSlot();
        EquipItemInit();
        SlotInit();
    }
    public List<Item> GetEquipItems()
    {
        return EquipItems;
    }
    public void SetIsStore(bool _isStore) { isStore = _isStore; SlotInit(); }
    public void SlotInit()
    {
        moneyText.text = useDB.instance.getMoney().ToString();
        BuySellButtonInit(); //상점이면 구매, 인벤토리면 판매로 버튼 교체
        TabClick(curTab); // 현재 탭으로 아이템 변경
        Debug.Log("id : "+useDB.instance.getId());
    }

    public void ShowSlot()
    {
        string[] type = new string[] { "WEAPON", "ARMOR", "ACCESSORIES", "APEARANCE", "ETC" };

        for (int i = 0; i < type.Length; i++)
        {
            Item item = EquipItems.Find(x => x.Type == type[i]);
            if (item != null)
            {
                ShowSlots.Find(x => x.Type == type[i]).ItemSlotImage.sprite = item.Image;
            }
            else
            {
                EquipSlots.Find(x => x.Type == type[i]).ItemSlotImage.sprite = null;
            }
        }
    }

    public void EquipItemToSlot()
    {
        if (isStore) 
        {
            for (int i = 0; i < EquipSlots.Count; i++)
            {
                EquipSlots[i].ItemSlotImage.sprite = null;
            }
        }
        else
        {
            string[] type = new string[] { "WEAPON", "ARMOR", "ACCESSORIES", "APEARANCE", "ETC" };

            for (int i = 0; i < type.Length; i++)
            {
                Item item = EquipItems.Find(x => x.Type == type[i]);
                if (item != null)
                {
                    EquipSlots.Find(x => x.Type == type[i]).ItemSlotImage.sprite = item.Image;
                    AddEquipItem(item);
                }
                else
                {
                    EquipSlots.Find(x => x.Type == type[i]).ItemSlotImage.sprite = null;
                }
            }
        }
        
    }

    public void EquipItemInit()
    {
        EquipItems = new List<Item>();
    }
    public void BuySellButtonInit()
    {
        if (isStore) 
        {
            buyButton.SetActive(true);
            sellButton.SetActive(false);
        }
        else
        {
            buyButton.SetActive(false);
            sellButton.SetActive(true);
        }
    }
    private void MakeEquipSlot()
    {
        ShowSlots = new List<EquipSlot>();
        ShowSlots.Add(new EquipSlot("WEAPON", ShowSlotItemImage[0]));
        ShowSlots.Add(new EquipSlot("ARMOR", ShowSlotItemImage[1]));
        ShowSlots.Add(new EquipSlot("ACCESSORIES", ShowSlotItemImage[2]));
        ShowSlots.Add(new EquipSlot("APEARANCE", ShowSlotItemImage[3]));
        ShowSlots.Add(new EquipSlot("ETC", ShowSlotItemImage[4]));

        EquipSlots = new List<EquipSlot>();
        EquipSlots.Add(new EquipSlot("WEAPON", EquipSlotItemImage[0]));
        EquipSlots.Add(new EquipSlot("ARMOR", EquipSlotItemImage[1]));
        EquipSlots.Add(new EquipSlot("ACCESSORIES", EquipSlotItemImage[2]));
        EquipSlots.Add(new EquipSlot("APEARANCE", EquipSlotItemImage[3]));
        EquipSlots.Add(new EquipSlot("ETC", EquipSlotItemImage[4]));

        foreach (var item in EquipSlots)
        {
            item.ItemSlotImage.sprite = null;
        }
        foreach (var item in ShowSlots)
        {
            item.ItemSlotImage.sprite = null;
        }
    }

    private void MakeTab()
    {
        Tabs = new List<Tab>();
        Tabs.Add(new Tab("WEAPON", TabImages[0], TabImagesTextImages[0], WeaponTabSprite, WeaponTabTextSprite));
        Tabs.Add(new Tab("ARMOR", TabImages[1], TabImagesTextImages[1], ArmorTabSprite, ArmorTabTextSprite));
        Tabs.Add(new Tab("ACCESSORIES", TabImages[2], TabImagesTextImages[2], AccessoriesTabSprite, AccessoriesTabTextSprite));
        Tabs.Add(new Tab("APEARANCE", TabImages[3], TabImagesTextImages[3], ApearanceTabSprite, ApearanceTabTextSprite));
        Tabs.Add(new Tab("ETC", TabImages[4], TabImagesTextImages[4], EtcTabSprite, EtcTabTextSprite));
    } 
    public void BuyTheItemClick()
    {
        //클릭한게 없으면
        if (SelectedItem == null){ Debug.Log("클릭한거 없음"); return;}
        //가진 돈 보다 비싸면
        if (useDB.instance.getMoney() < SelectedItem.Price){ Debug.Log("돈 부족"); return;}

        string s = string.Format("현재 보유중인 돈 : {0}, 아이템 가격 : {1}", useDB.instance.getMoney(), SelectedItem.Price);
        Debug.Log(s);

        //알람 캔버스 띄우기
        uiAnimation.PlayOnAnimation("Buy");
    }

    public void SellTheItemClick()
    {
        if (SelectedItem == null) { Debug.Log("클릭한거 없음"); return; }

        uiAnimation.PlayOnAnimation("Sell");
    }
    public void ChoiceSell()
    {
        if (SelectedItem == null) { return; }
        useDB.instance.setMoney(SelectedItem.Price);
        moneyText.text = useDB.instance.getMoney().ToString();
        switch (SelectedItem.Type)
        {
            case "WEAPON":
                useDB.instance.updateWp(SelectedItem.Name,false);
                break;
            case "ARMOR":
                useDB.instance.updateChar(SelectedItem.Name, false);
                break;
        }
        SlotData.instance.AllItemsInit();
        ChangeEquipSlot(null, SelectedItem.Type);
        RemoveEquipItem(SelectedItem);
        SlotClickRefresh();
        ImageRefresh();
    }

    public void ChoiceBuy(bool buy)
    {
        if (SelectedItem == null) { return; }
        if (buy)
        {
            //돈 차감하고
            useDB.instance.setMoney(-SelectedItem.Price);
            moneyText.text = useDB.instance.getMoney().ToString();

            //아이템 정보 true로 바꾸고
            switch (SelectedItem.Type)
            {
                case "WEAPON":
                    useDB.instance.updateWp(SelectedItem.Name,true);
                    break;
                case "ARMOR":
                    useDB.instance.updateChar(SelectedItem.Name, true);
                    break;
            }
            SlotData.instance.AllItemsInit();
        }
        //화면 다시 보여주기
        Remove3DItem(SelectedItem);
        ChangeEquipSlot(null, SelectedItem.Type);
        SlotClickRefresh();
        ImageRefresh();
    }

    public void SlotClickRefresh()
    {
        foreach (var slot in SlotImages)
        {
            slot.sprite = SlotOnOffImages[1];
        }

        itemSelected = false;
        SelectedItem = null;
    }
    
    public void ShopSlotClick()
    {
        if(SelectedItem != null) ChangeEquipSlot(null, SelectedItem.Type);
        //누른 슬롯의 이미지
        GameObject Slot = EventSystem.current.currentSelectedGameObject;
        Image[] Images = Slot.GetComponentsInChildren<Image>();
        Image itemImage = Images[0];
        Image itemSlotItemImage = Images[1];

        Text[] Texts = Slot.GetComponentsInChildren<Text>(); 
        
        bool otherItem = true;

        //이미 누른건지 체크 
        if (itemImage.sprite == SlotOnOffImages[0])
        {
            if (!isStore)
            {
                RemoveEquipItem(SelectedItem);
            }
            else
            {
                Remove3DItem(SelectedItem);
            }
            otherItem = false;
            itemSelected = false;
            SelectedItem = null;
        }
        //반복문으로 모두 체크 안한 표시 하고
        foreach (var slot in SlotImages) {slot.sprite = SlotOnOffImages[1];}

        //누른게 아닐때만 체크
        if (otherItem){ itemImage.sprite = SlotOnOffImages[0]; itemSelected = true; }

        if (itemSelected) 
        {
            SelectedItem = isStore? ShopItems.Find(x => x.Image == itemSlotItemImage.sprite): InvenItems.Find(x => x.Image == itemSlotItemImage.sprite);
            ChangeEquipSlot(SelectedItem.Image, SelectedItem.Type);
            if (!isStore) { AddEquipItem(SelectedItem); }
            else { Make3DItem(SelectedItem); }
        }
    }

    public void AutoEquipClick()
    {
        List<Item> TempList = new List<Item>();
        TempList = isStore ? ShopItems : InvenItems;
        if (TempList.Count == 0) { return; }
        //랜덤한 숫자
        int ranNum = Random.Range(0, TempList.Count);
        //선택된 슬롯바꾸기
        SlotClickRefresh();
        Item tempItem = TempList[ranNum];
        SelectedItem = tempItem;
        SlotImages[ranNum].sprite = SlotOnOffImages[0];

        //인벤토리면 장착정보 저장 
        if (!isStore) { AddEquipItem(tempItem); }
        else{Make3DItem(SelectedItem);}

        //장착 슬롯에 끼우기
        ChangeEquipSlot(tempItem.Image,tempItem.Type);
    }
    public void Make3DItem(Item item)
    {
        Debug.Log("Make3DItem 호출됨");
        Remove3DItem(item);
        int index = 10;

        if (item.Type == "WEAPON")
        { 
            index = 0;
        }
        else if (item.Type == "ARMOR")
        {
            index = 1;
        }
        else if (item.Type == "ETC")
        {
            index = 2;
        }

        //무기나 아이템 생성
        storeShowItem[index] = Instantiate(item.Obj3d, itemSpawnTransform[index]);

        //무기를 찾아서 가져옴
        GameObject weapon = storeShowItem[0];

        //생성된 캐릭터에서 스폰위치 가져오기
        if(storeShowItem[1] != null)
        {
            Transform[] temp = storeShowItem[1].GetComponentsInChildren<Transform>();
            Transform weaponSpawnPos = null;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].tag == "WeaponSpawnPos")
                {

                    weaponSpawnPos = temp[i];
                    break;
                }
            }
            if (weapon != null && weaponSpawnPos != null)
            {
                Debug.Log("손에 아이템 생성");
                Destroy(CharacterHandItem);
                CharacterHandItem = Instantiate(weapon, weaponSpawnPos.transform);
                Debug.Log(weapon);
                Debug.Log(weaponSpawnPos.transform.position);
            }
            else
            {
                Debug.Log("손이나 무기 둘중 한개 못찾음");
            }
        }

    }
    //GameObject temp = GameObject.FindGameObjectWithTag("WeaponSpawnPos");
    public void Remove3DItem(Item item)
    {
        int index = 10;
        if (item.Type == "WEAPON") { index = 0; }
        else if (item.Type == "ARMOR") { index = 1; }

        if (storeShowItem[index] != null)
        {
            Destroy(storeShowItem[index]);
        }

        if(index == 0)
        {
            Destroy(CharacterHandItem);
        }
    }

    public void RemoveAllShowItem() 
    {
        if (!isStore)
        {
            for (int i = 0; i < showItem.Length; i++)
            {
                Destroy(showItem[i]);
            }
        }
        else 
        {
            for (int i = 0; i < storeShowItem.Length; i++)
            {
                Destroy(storeShowItem[i]);
            }
        }
    }
    public void RemoveEquipItem(Item item)
    {
        for (int i = 0; i < EquipItems.Count; i++)
        {
            if (EquipItems[i].Type == item.Type)
            {
                EquipItems.RemoveAt(i);
                int index = 10;
                if (item.Type == "WEAPON") { index = 0; }
                else if (item.Type == "ARMOR") { index = 1; }
                else if (item.Type == "ETC") { index = 2; }
                Destroy(showItem[index]);
            }
        }
    }
    public void AddEquipItem(Item item)
    {
        RemoveEquipItem(item);
        EquipItems.Add(item);
        int index = 10;
        if (item.Type == "WEAPON"){index = 0;}
        else if(item.Type == "ARMOR"){ index = 1;}
        else if(item.Type == "ETC"){ index = 2; }
        showItem[index] = Instantiate(item.Obj3d, itemSpawnTransform[index]);
        //무기를 찾아서 가져옴
        GameObject weapon = showItem[0];

        //생성된 캐릭터에서 스폰위치 가져오기
        if (showItem[1] != null)
        {
            Transform[] temp = showItem[1].GetComponentsInChildren<Transform>();
            Transform weaponSpawnPos = null;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].tag == "WeaponSpawnPos")
                {

                    weaponSpawnPos = temp[i];
                    break;
                }
            }
            if (weapon != null && weaponSpawnPos != null)
            {
                Debug.Log("손에 아이템 생성");
                Destroy(CharacterHandItem);
                CharacterHandItem = Instantiate(weapon, weaponSpawnPos.transform);
                Debug.Log(weapon);
                Debug.Log(weaponSpawnPos.transform.position);
            }
            else
            {
                Debug.Log("손이나 무기 둘중 한개 못찾음");
            }
        }

    }
    public void ChangeEquipSlot(Sprite sprite, string type)
    {
        //가져온 타입으로 장착슬롯을 찾아서 이미지 교체
        EquipSlot equipSlot = EquipSlots.Find(x => x.Type == type);
        equipSlot.ItemSlotImage.sprite = equipSlot==null? sprite: null;
    }
    public void TabClick(string tab)
    {
        SlotClickRefresh();
        Tab tempTab = Tabs.Find(x => x.Name == curTab);
        tempTab.TabImage.sprite = tempTab.OffSprite;
        tempTab.TabTextImage.sprite = tempTab.OffTextSprite;

        curTab = tab;
        //현재 탭은 onSprite로 아니면 offSprite로
        tempTab = Tabs.Find(x=> x.Name == curTab);
        tempTab.TabImage.sprite = tempTab.OnSprite;
        tempTab.TabTextImage.sprite = tempTab.OnTextSprite;
        ImageRefresh();
    }

    //탭에 따라 아이템 이미지의 스프라이트 교체
    public void ImageRefresh()
    {
        List<Item> TempItems = new List<Item>();
        ShopItems = new List<Item>();
        InvenItems = new List<Item>();

        TempItems = SlotData.instance.GetItems(!isStore);
        TempItems = TempItems.FindAll(x=> x.Type == curTab);

        foreach (var slot in SlotImages) { slot.gameObject.SetActive(false); }
        for (int i = 0; i < TempItems.Count; i++)
        {
            SlotImages[i].gameObject.SetActive(true);
            SlotItemImage[i].sprite = TempItems[i].Image;
            Text[] Texts = SlotImages[i].gameObject.GetComponentsInChildren<Text>();
            Texts[0].text = TempItems[i].Name;
            Texts[1].text = TempItems[i].Price.ToString();
            Texts[2].text = TempItems[i].Damage.ToString();
        }

        if (isStore) { ShopItems = TempItems; }
        else { InvenItems = TempItems; }
    }
}
