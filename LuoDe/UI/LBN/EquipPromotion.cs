using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipPromotion : MonoBehaviour
{
    Text propTime1, propTime2, propTime3, propTime4;
    Image image1, image2, image3, image4;
    List<Equip> equipList;
    List<Item> itemList;
    List<Equip> randomEquipList = null;
    DayOfWeek week;
    PackageData curPackage;
    Text gold;
    GameObject curBuy;
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
        gold.text = "" + curPackage.Gold;
        equipList = new List<Equip>();
        itemList = new List<Item>();
        itemList = ItemManager.Instance.GetAllItem();
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] is Equip && itemList[i].Type == ItemType.LongHandle || itemList[i].Type == ItemType.SwordShield || itemList[i].Type == ItemType.Wand || itemList[i].Type == ItemType.Armor)
            {
                equipList.Add(itemList[i] as Equip);
            }
        }
        randomEquipList = new List<Equip>();
        for (int i = 0; i < equipList.Count; i++)
        {
            if (equipList[i].Quality == ItemQuality.N || equipList[i].Quality == ItemQuality.R || equipList[i].Quality == ItemQuality.SR)
            {
                randomEquipList.Add(equipList[i]);
            }
        }
        /* 
         * 0:20001，1:20002，2:20003
         * 3:30001，4:30002，5:30003
         * 6:40001，7:40002，8:40003
         * 9:50001，10:50002，11:50003
         */
        if (week == DayOfWeek.Monday)
        {
            Show(0, 4, 8, 9);
        }
        if (week == DayOfWeek.Tuesday)
        {
            Show(1, 5, 6, 10);
        }
        if (week == DayOfWeek.Wednesday)
        {
            Show(2, 3, 7, 11);
        }
        if (week == DayOfWeek.Thursday)
        {
            Show(0, 5, 6, 11);
        }
        if (week == DayOfWeek.Friday)
        {
            Show(2, 3, 8, 9);
        }
        if (week == DayOfWeek.Saturday)
        {
            Show(1, 4, 7, 10);
        }
        if (week == DayOfWeek.Sunday)
        {
            Show(2, 5, 8, 11);
        }
    }
    void Show(int i, int j, int k, int l)
    {
        transform.Find("frame/prop/name").GetComponent<Text>().text = randomEquipList[i].Name;
        transform.Find("frame/prop/price/Text").GetComponent<Text>().text = "" + (int)(randomEquipList[i].Price * 0.50f);
        transform.Find("frame/prop/price").GetComponent<Button>().onClick.AddListener(Buy);

        transform.Find("frame/prop (1)/name").GetComponent<Text>().text = randomEquipList[j].Name;
        transform.Find("frame/prop (1)/price/Text").GetComponent<Text>().text = "" + (int)(randomEquipList[j].Price * 0.50f);
        transform.Find("frame/prop (1)/price").GetComponent<Button>().onClick.AddListener(Buy);

        transform.Find("frame/prop (2)/name").GetComponent<Text>().text = randomEquipList[k].Name;
        transform.Find("frame/prop (2)/price/Text").GetComponent<Text>().text = "" + (int)(randomEquipList[k].Price * 0.50f);
        transform.Find("frame/prop (2)/price").GetComponent<Button>().onClick.AddListener(Buy);

        transform.Find("frame/prop (3)/name").GetComponent<Text>().text = randomEquipList[l].Name;
        transform.Find("frame/prop (3)/price/Text").GetComponent<Text>().text = "" + (int)(randomEquipList[l].Price * 0.50f);
        transform.Find("frame/prop (3)/price").GetComponent<Button>().onClick.AddListener(Buy);
    }
    void Buy()
    {
        curBuy = EventSystem.current.currentSelectedGameObject;
        int price;
        int.TryParse(curBuy.transform.Find("Text").GetComponent<Text>().text, out price);
        if (curPackage.Gold >= price)
        {
            Equip cureq = new Equip();
            for (int i = 0; i < randomEquipList.Count; i++)
            {
                if (randomEquipList[i].Name == curBuy.transform.parent.Find("name").GetComponent<Text>().text)
                {
                    cureq = randomEquipList[i];
                }
            }

            Equip newCon = new Equip(cureq.itemData);
            PackageManager.Instance.AddItem(newCon);
            PackageManager.Instance.Gold -= price;
            Debug.Log("购买成功");

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
        week = DateTime.Now.DayOfWeek;
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        day = DateTime.Now.Day;
        xs = hour;
        fz = minute;
        m = second;

        int XS = 23 - xs;
        int FZ = 59 - fz;
        int M = 59 - m;
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
