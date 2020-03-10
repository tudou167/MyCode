using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : Item
{
    public Equip() { }
    public Equip(ItemData itemData)
    {
        this.itemData = itemData;
    }
    public int itemID;
    public ItemData itemData;

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
    public string Prefab { get { return itemData.Prefab; } }
    public int ATK { get { return itemData.ATK; } }
    public int DEF { get { return itemData.DEF; } }
    public int ATKSpeed
    {
        get
        {
            return itemData.ATKSpeed;
        }
        set
        {
            if (itemData != null)
            {
                itemData.ATKSpeed = value;
            }
        }
    }
    public int Crit { get { return itemData.Crit; } }
    public int Repel { get { return itemData.Repel; } }

}
