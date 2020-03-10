using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeijingTime : MonoBehaviour
{
    public enum MyEnum
    {
        equip,
        prop
    }
    // Text ShowTime;
    /// <summary>
    /// 道具是否正在出售
    /// </summary>
    public bool OnSale1 = true;
    public bool OnSale2 = true;
    public bool OnSale3 = true;
    public bool OnSale4 = true;
    //时间

    Text propTime1, propTime2, propTime3, propTime4;
    Image image1, image2, image3, image4;
    Consumables mu1, mu2, mu3, mu4;
    Equip me1, me2, me3, me4;
    public MyEnum myEnum = MyEnum.equip;
    void Start()
    {
        //ShowTime = transform.GetComponent<Text>();
        propTime1 = transform.Find("frame/prop/Time").GetComponent<Text>();
        propTime2 = transform.Find("frame/prop (1)/Time").GetComponent<Text>();
        propTime3 = transform.Find("frame/prop (2)/Time").GetComponent<Text>();
        propTime4 = transform.Find("frame/prop (3)/Time").GetComponent<Text>();
        image1 = transform.Find("frame/prop/Image").GetComponent<Image>();
        image2 = transform.Find("frame/prop (1)/Image").GetComponent<Image>();
        image3 = transform.Find("frame/prop (2)/Image").GetComponent<Image>();
        image4 = transform.Find("frame/prop (3)/Image").GetComponent<Image>();

        //创建了装备类型
        List<Equip> equipList = new List<Equip>();
        //创建了道具
        List<Consumables> consumablesList = new List<Consumables>();
        //获取了所有
        List<Item> itemList = new List<Item>();
        itemList = ItemManager.Instance.GetAllItem();
        for (int i = 0; i < itemList.Count; i++)
        {//拿到了所有道具
            //如果这个是装备就把他转换成装备
            if (itemList[i] is Equip)
            {
                equipList.Add(itemList[i] as Equip);
            }
            //如果这个是道具就把他转换成道具
            if (itemList[i] is Consumables)
            {
                consumablesList.Add(itemList[i] as Consumables);
            }
        }
        //拿到所有底级的道具

        if (myEnum == MyEnum.prop)
        {
            for (int i = 0; i < consumablesList.Count; i++)
            {
                if (consumablesList[i].Quality == ItemQuality.N || consumablesList[i].Quality == ItemQuality.R || consumablesList[i].Quality == ItemQuality.SR)
                {
                    int n1 = UnityEngine.Random.Range(0, consumablesList.Count - 1);
                    mu1 = consumablesList[n1];
                    int n2 = UnityEngine.Random.Range(0, consumablesList.Count - 1);
                    mu2 = consumablesList[n2];
                    int n3 = UnityEngine.Random.Range(0, consumablesList.Count - 1);
                    mu3 = consumablesList[n3];
                    int n4 = UnityEngine.Random.Range(0, consumablesList.Count - 1);
                    mu4 = consumablesList[n4];
                }
            }
        }
        else
        {
            for (int i = 0; i < equipList.Count; i++)
            {
                if (equipList[i].Quality == ItemQuality.N || equipList[i].Quality == ItemQuality.R || equipList[i].Quality == ItemQuality.SR)
                {
                    int n1 = UnityEngine.Random.Range(0, equipList.Count - 1);
                    me1 = equipList[n1];
                    int n2 = UnityEngine.Random.Range(0, equipList.Count - 1);
                    me2 = equipList[n2];
                    int n3 = UnityEngine.Random.Range(0, equipList.Count - 1);
                    me3 = equipList[n3];
                    int n4 = UnityEngine.Random.Range(0, equipList.Count - 1);
                    me4 = equipList[n4];
                }
            }
        }
        if (myEnum == MyEnum.prop)
        {
            transform.Find("frame/prop/name").GetComponent<Text>().text = mu1.Name;
            transform.Find("frame/prop (1)/name").GetComponent<Text>().text = mu2.Name;
            transform.Find("frame/prop (2)/name").GetComponent<Text>().text = mu3.Name;
            transform.Find("frame/prop (3)/name").GetComponent<Text>().text = mu4.Name;
        }
        else
        {
            transform.Find("frame/prop/name").GetComponent<Text>().text = me1.Name;
            transform.Find("frame/prop (1)/name").GetComponent<Text>().text = me2.Name;
            transform.Find("frame/prop (2)/name").GetComponent<Text>().text = me3.Name;
            transform.Find("frame/prop (3)/name").GetComponent<Text>().text = me4.Name;

        }

        //for (int i = 0; i < consumablesList.Count; i++)
        //{
        //    if(consumablesList[i].Name == name)
        //    {

        //    }
        //}
        // Consumables a = null;
        // PackageData p = PackageManager.Instance.GetPackageData();
        // for (int i = 0; i < p.consumablesList.Count; i++)
        // {
        //     if (p.consumablesList[i].Name == name)
        //     {
        //         a = p.consumablesList[i];
        //     }
        // }
        //// PackageManager.Instance.GetItem<Consumables>(id);
        // PackageManager.Instance.AddItem(a);
        // PackageManager.Instance.DeleteItem(a.itemID);
        // p = PackageManager.Instance.GetPackageData();
        // p.Gold -= 100;
        // PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID,HeroManager.Instance.GetCurHeroData().RoleID, p);

    }
    // ItemData d;
    public void OnRq()
    {

    }

    int hour, minute, second, day;
    string dq;
    int oo = 240000;
    int xs, fz, m;
    void Update()
    {
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        day = DateTime.Now.Day;
        //dq =( hour+":"+ minute + ":" + second).ToString();
        // dq = (hour + minute + second).ToString();
        // Debug.Log(dq);
        xs = hour;
        fz = minute;
        m = second;

        int XS = 24 - xs;
        int FZ = 60 - fz;
        int M = 60 - m;
        // Debug.Log(XS + ":" + FZ + ":" + M);

        if (OnSale1)
        {
            propTime1.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
            if (image1.gameObject.activeInHierarchy)
            {
                image1.gameObject.SetActive(false);
            }
        }
        else
        {
            propTime1.text = "<color=red>已经卖光了</color>";
            if (!image1.gameObject.activeInHierarchy)
            {
                image1.gameObject.SetActive(true);
            }
        }
        if (OnSale2)
        {
            propTime2.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
            if (image2.gameObject.activeInHierarchy)
            {
                image2.gameObject.SetActive(false);
            }
        }
        else
        {
            propTime2.text = "<color=red>已经卖光了</color>";
            if (!image2.gameObject.activeInHierarchy)
            {
                image2.gameObject.SetActive(true);
            }
        }
        if (OnSale3)
        {
            propTime3.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
            if (image3.gameObject.activeInHierarchy)
            {
                image3.gameObject.SetActive(false);
            }
        }
        else
        {
            propTime3.text = "<color=red>已经卖光了</color>";
            if (!image3.gameObject.activeInHierarchy)
            {
                image3.gameObject.SetActive(true);
            }
        }
        if (OnSale4)
        {
            propTime4.text = "限时特惠" + ("<color=#FF0000>" + XS + "</color>" + ":<color=#00FF7F>" + FZ + "</color>:<color=#00FF7F>" + M + "</color>").ToString();
            if (image4.gameObject.activeInHierarchy)
            {
                image4.gameObject.SetActive(false);
            }
        }
        else
        {
            propTime4.text = "<color=red>已经卖光了</color>";
            if (!image4.gameObject.activeInHierarchy)
            {
                image4.gameObject.SetActive(true);
            }
        }

        // ShowTime.text = "限时特惠"+("<color=#FF0000>"+XS+"</color>" +":<color=#CFB53B>"+FZ+"</color>:<color=#00FF7F>"+M+"</color>").ToString();
        if (XS == 00 && FZ == 00 && M == 0)
        {
            Debug.Log("数据刷新");
        }
    }
}
