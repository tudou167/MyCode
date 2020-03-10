using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PackageData
{
    public int Gold;
    //public List<Item> ItemList;
    public List<Equip> equipList;
    public List<Consumables> consumablesList;
    public PackageData() { }
    //把传进来的list进行分类
    public PackageData(int gold, List<Item> itemList)
    {
        Gold = gold;
        //ItemList = itemList;
        equipList = new List<Equip>();
        consumablesList = new List<Consumables>();
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] is Equip)
            {
                equipList.Add(itemList[i] as Equip);
            }
            else
            {
                consumablesList.Add(itemList[i] as Consumables);
            }
        }
    }

    public List<Item> GetItemData()
    {
        List<Item> itemList = new List<Item>();
        for (int i = 0; i < equipList.Count; i++)
        {
            itemList.Add(equipList[i]);
        }
        for (int i = 0; i < consumablesList.Count; i++)
        {
            itemList.Add(consumablesList[i]);
        }
        return itemList;
    }


}
