using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WarehouseLoader : MonoBehaviour
{

    void Awake()
    {
        if (!Directory.Exists(AllPath.Instance.userDirectoryPath)) return;
        //TODO 用户ID 和 角色ID要动态获取
        int userID = UserManager.Instance.GetCurUser().ID;
        int roleID = HeroManager.Instance.GetCurHeroData().RoleID;
        string warehousePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountWarehouseDirectoryPath + userID + "_" + roleID;
        WarehouseData data = Tool.Instance.JsonToObj<WarehouseData>(warehousePath);

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
        WarehouseManager.Instance.Bind(dic);
    }

}
