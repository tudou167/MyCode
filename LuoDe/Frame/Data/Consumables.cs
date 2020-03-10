using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : Item
{
    public Consumables() { }
    public Consumables(ItemData itemData,int count)
    {
        this.itemData = itemData;
        this.count = count;
    }
    public int itemID;
    public ItemData itemData;
    public int count { get; set; }

    public string Detail
    {
        get
        {
            return itemData.Detail;
        }
    }

    public string Icon
    {
        get
        {
            return itemData.Icon;
        }

    }

    public int ID
    {
        get
        {
            return itemData.ID;
        }
    }

    public string Name
    {
        get
        {
            return itemData.Name;
        }

    }

    public int Price
    {
        get
        {
            return itemData.Price;
        }

    }

    public ItemQuality Quality
    {
        get
        {
            return itemData.Quality;
        }

    }

    public ItemType Type
    {
        get
        {
            return itemData.Type;
        }

    }
}
