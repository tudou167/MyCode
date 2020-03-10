using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//英雄单个背包
public class PackageLoader : MonoBehaviour
{
    void Awake()
    {
        if (!Directory.Exists(AllPath.Instance.userDirectoryPath)) return;
        //角色配表里面 主角填的是背包里面的ItemID 怪物填的是静态的物品ID 默认1，2是装备和武器
        //TODO 用户ID 和 角色ID要动态获取
        int userID = UserManager.Instance.GetCurUser().ID;
        int roleID = HeroManager.Instance.GetCurHeroData().RoleID;
        string packagePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountPackageDirectoryPath + userID + "_" + roleID;
        PackageData data = Tool.Instance.JsonToObj<PackageData>(packagePath);

        Dictionary<int, Item> dic = new Dictionary<int, Item>();

        List<Item> itemList = data.GetItemData();
        for (int i = 0; i < itemList.Count; i++)
        {
            int itemID = dic.Count + 1;
            if (itemList[i] is Equip)
            {
                Equip equip = itemList[i] as Equip;
                equip.itemID = itemID;
                dic.Add(itemID, equip);
            }
            else
            {
                Consumables consumables = itemList[i] as Consumables;
                consumables.itemID = itemID;
                dic.Add(itemID, consumables);
            }
        }
        PackageManager.Instance.Bind(data.Gold,dic);
    }

}
