using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package
{

    // -物品接口列表
    public ItemInterface[] itemList;
    // -快捷键栏
    public ItemPossession[] itemPossession;
    // -快捷键栏ID列表
    public List<int> itemPossessionID;
    // -武器
    private Equipment weapon = null;
    public Equipment Weapon
    {
        get { return weapon; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.weapon)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是武器");
            };
        }
    }
    // -帽子
    private Equipment hat = null;
    public Equipment Hat
    {
        get { return hat; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.hat)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是帽子");
            };
        }
    }
    // -衣服
    private Equipment clothes = null;
    public Equipment Clothes
    {
        get { return clothes; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.clothes)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是衣服");
            };
        }
    }
    // -鞋子
    private Equipment shoes = null;
    public Equipment Shoes
    {
        get { return shoes; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.shoes)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是鞋子");
            };
        }
    }
    // -腰带
    private Equipment belt = null;
    public Equipment Belt
    {
        get { return belt; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.belt)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是腰带");
            };
        }
    }
    // -戒子1
    private Equipment ring1 = null;
    public Equipment Ring1
    {
        get { return ring1; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.ring)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是戒子");
            };
        }
    }
    // -戒子2
    private Equipment ring2 = null;
    public Equipment Ring2
    {
        get { return ring2; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.ring)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是戒子");
            };
        }
    }
    // -项链
    private Equipment necklace = null;
    public Equipment Necklace
    {
        get { return necklace; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.necklace)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是项链");
            };
        }
    }
    //手镯
    private Equipment bracelet = null;
    public Equipment Bracelet
    {
        get { return bracelet; }
        set
        {
            if (weapon.equipmentType == Enumclass.ItemType.bracelet)
            {
                weapon = value;
            }
            else
            {
                Debug.LogError("该物品不是手镯");
            };
        }
    }


}
