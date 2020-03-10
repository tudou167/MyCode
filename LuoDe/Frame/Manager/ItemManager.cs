using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager:Singleton<ItemManager>
{
    private Dictionary<int, Item> itemDic;

    public Item GetItem(int id)
    {
        if (itemDic != null && itemDic.ContainsKey(id))
        {
            return itemDic[id];
        }
        return null;
    }

    public List<Item> GetAllItem()
    {
        List<Item> list = new List<Item>();
        foreach (Item item in itemDic.Values)
        {
            list.Add(item);
        }
        return list;
    }

    public void Bind(Dictionary<int, Item> itemDic)
    {
        this.itemDic = itemDic;
    }
}
