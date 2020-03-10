using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WarehouseManager : Singleton<WarehouseManager>
{

    private Dictionary<int, Item> warehouseItemDic;

    public void Bind(Dictionary<int, Item> warehouseItemDic)
    {
        this.warehouseItemDic = warehouseItemDic;
    }
    //所有放进仓库都要经过这个接口
    public int AddItem(Item item)
    {
        if (warehouseItemDic == null)
        {
            return 0;
        }

        //int itemID = warehouseItemDic.Count == 0 ? 1 : warehouseItemDic.Keys.Last<int>() + 1;
        int itemID = 0;
        if (warehouseItemDic.Count != 0)
        {
            foreach (int itemid in warehouseItemDic.Keys)
            {
                if (itemID < itemid) itemID = itemid;
            }
        }
        itemID += 1;
        if (item is Equip)
        {
            Equip equip = item as Equip;
            equip.itemID = itemID;
            warehouseItemDic.Add(itemID, equip);
        }
        else
        {
            Consumables consumables = item as Consumables;
            consumables.itemID = itemID;
            warehouseItemDic.Add(itemID, consumables);
        }
        return itemID;
    }
    public T GetItem<T>(int ItemID) where T : class, new()
    {
        if (warehouseItemDic != null && warehouseItemDic.ContainsKey(ItemID))
        {
            if (warehouseItemDic[ItemID] is T)
            {
                return warehouseItemDic[ItemID] as T;
            }
        }

        return null;
    }
    public void DeleteItem(int itemID)
    {
        if (warehouseItemDic.ContainsKey(itemID)) warehouseItemDic.Remove(itemID);
    }

    public WarehouseData GetPackageData()
    {
        WarehouseData data = new WarehouseData();
        data.equipList = new List<Equip>();
        data.consumablesList = new List<Consumables>();
        foreach (Item item in warehouseItemDic.Values)
        {
            if (item is Equip)
            {
                data.equipList.Add(item as Equip);
            }
            else
            {
                data.consumablesList.Add(item as Consumables);
            }
        }

        return data;
    }
    public void InitData(int userID, int roleID)
    {
        string warehousePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountWarehouseDirectoryPath;
        WarehouseData itemList = new WarehouseData(new List<Item>() {
            ItemManager.Instance.GetItem(10001)
            });
        File.WriteAllText(warehousePath + userID + "_" + roleID, Tool.Instance.ObjToJson(itemList));
    }
    public void SaveData(int userID, int roleID,WarehouseData data)
    {
        string warehousePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountWarehouseDirectoryPath;
        File.WriteAllText(warehousePath + userID + "_" + roleID, Tool.Instance.ObjToJson(data));
    }
}
