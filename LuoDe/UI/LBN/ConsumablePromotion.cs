using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ConsumablePromotion : MonoBehaviour
{
    Text propTime1, propTime2, propTime3, propTime4;
    Image image1, image2, image3, image4;
    List<Consumables> consumablesList;
    List<Item> itemList;
    List<Consumables> randomConsumablesList = null;
    PackageData curPackage;
    Text gold;
    GameObject curBuy;
    DayOfWeek week;
    void OnEnable()
    {
        curPackage = PackageManager.Instance.GetPackageData();
    }

    void Start()
    {
        week = DateTime.Now.DayOfWeek;
        curPackage = PackageManager.Instance.GetPackageData();
        propTime1 = transform.Find("frame/prop/Time").GetComponent<Text>();
        propTime2 = transform.Find("frame/prop (1)/Time").GetComponent<Text>();
        propTime3 = transform.Find("frame/prop (2)/Time").GetComponent<Text>();
        propTime4 = transform.Find("frame/prop (3)/Time").GetComponent<Text>();
        image1 = transform.Find("frame/prop/Image").GetComponent<Image>();
        image2 = transform.Find("frame/prop (1)/Image").GetComponent<Image>();
        image3 = transform.Find("frame/prop (2)/Image").GetComponent<Image>();
        image4 = transform.Find("frame/prop (3)/Image").GetComponent<Image>();
        gold = transform.parent.Find("gold/Text").GetComponent<Text>();
        gold.text = "<color=yellow>" + curPackage.Gold + "</color>";
        consumablesList = new List<Consumables>();
        itemList = new List<Item>();
        itemList = ItemManager.Instance.GetAllItem();
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] is Consumables && itemList[i].Type == ItemType.Consumables || itemList[i].Type == ItemType.ArmorMaterial || itemList[i].Type == ItemType.WeaponMaterial)
            {
                consumablesList.Add(itemList[i] as Consumables);
            }
        }
        randomConsumablesList = new List<Consumables>();
        for (int i = 0; i < consumablesList.Count; i++)
        {
            if (consumablesList[i].Quality == ItemQuality.N || consumablesList[i].Quality == ItemQuality.R || consumablesList[i].Quality == ItemQuality.SR)
            {
                randomConsumablesList.Add(consumablesList[i]);
            }
        }
        if (week == DayOfWeek.Monday)
        {
            Show(1, 4, 8, 9);
        }
        if (week == DayOfWeek.Tuesday)
        {
            Show(2, 3, 6, 10);
        }
        if (week == DayOfWeek.Wednesday)
        {
            Show(0, 5, 7, 9);
        }
        if (week == DayOfWeek.Thursday)
        {
            Show(1, 4, 7, 10);
        }
        if (week == DayOfWeek.Friday)
        {
            Show(2, 3, 6, 9);
        }
        if (week == DayOfWeek.Saturday)
        {
            Show(2, 5, 7, 10);
        }
        if (week == DayOfWeek.Sunday)
        {
            Show(2, 4, 8, 9);
        }
    }
    void Show(int i, int j, int k, int l)
    {
        transform.Find("frame/prop/name").GetComponent<Text>().text = randomConsumablesList[i].Name;
        transform.Find("frame/prop/price/Text").GetComponent<Text>().text = "" + (int)(randomConsumablesList[i].Price * 0.5f);
        transform.Find("frame/prop/price").GetComponent<Button>().onClick.AddListener(Buy);

        transform.Find("frame/prop (1)/name").GetComponent<Text>().text = randomConsumablesList[j].Name;
        transform.Find("frame/prop (1)/price/Text").GetComponent<Text>().text = "" + (int)(randomConsumablesList[j].Price * 0.5f);
        transform.Find("frame/prop (1)/price").GetComponent<Button>().onClick.AddListener(Buy);

        transform.Find("frame/prop (2)/name").GetComponent<Text>().text = randomConsumablesList[k].Name;
        transform.Find("frame/prop (2)/price/Text").GetComponent<Text>().text = "" + (int)(randomConsumablesList[k].Price * 0.5f);
        transform.Find("frame/prop (2)/price").GetComponent<Button>().onClick.AddListener(Buy);

        transform.Find("frame/prop (3)/name").GetComponent<Text>().text = randomConsumablesList[l].Name;
        transform.Find("frame/prop (3)/price/Text").GetComponent<Text>().text = "" + (int)(randomConsumablesList[l].Price * 0.5f);
        transform.Find("frame/prop (3)/price").GetComponent<Button>().onClick.AddListener(Buy);
    }
    void Buy()
    {
        curBuy = EventSystem.current.currentSelectedGameObject;
        int price;
        int.TryParse(curBuy.transform.Find("Text").GetComponent<Text>().text, out price);
        if (curPackage.Gold >= price)
        {
            Consumables curco = new Consumables();
            for (int i = 0; i < randomConsumablesList.Count; i++)
            {
                if (randomConsumablesList[i].Name == curBuy.transform.parent.Find("name").GetComponent<Text>().text)
                {
                    curco = randomConsumablesList[i];
                }
            }
            Consumables isInPack = null;
            for (int i = 0; i < curPackage.consumablesList.Count; i++)
            {
                if (curPackage.consumablesList[i].ID == curco.ID)
                {
                    isInPack = curPackage.consumablesList[i];
                    break;
                }
            }
            if (isInPack == null)
            {
                Consumables newCon = new Consumables(curco.itemData, 1);
                PackageManager.Instance.AddItem(newCon);
                PackageManager.Instance.Gold -= price;
                Debug.Log("购买成功none");
            }
            else
            {
                PackageManager.Instance.GetItem<Consumables>(isInPack.itemID).count += 1;
                PackageManager.Instance.Gold -= price;
                Debug.Log("购买成功exist");
            }
            
            PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, PackageManager.Instance.GetPackageData());
            curPackage = PackageManager.Instance.GetPackageData();
            gold.text = "<color=yellow>" + curPackage.Gold + "</color>";
        }
        else
        {
            Debug.Log("金钱不够");
        }
    }

    int hour, minute, second, day;
    string dq;
    int oo = 240000;
    int xs, fz, m;
    void Update()
    {
        //week = DateTime.Now.DayOfWeek;
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        day = DateTime.Now.Day;
        xs = hour;
        fz = minute;
        m = second;

        int XS = 24 - xs;
        int FZ = 60 - fz;
        int M = 60 - m;

        propTime1.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
        if (image1.gameObject.activeInHierarchy)
        {
            image1.gameObject.SetActive(false);
        }
        propTime2.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
        if (image2.gameObject.activeInHierarchy)
        {
            image2.gameObject.SetActive(false);
        }
        propTime3.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
        if (image3.gameObject.activeInHierarchy)
        {
            image3.gameObject.SetActive(false);
        }
        propTime4.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
        if (image4.gameObject.activeInHierarchy)
        {
            image4.gameObject.SetActive(false);
        }

        if (XS == 00 && FZ == 00 && M == 0)
        {
            Start();
        }
    }
}
