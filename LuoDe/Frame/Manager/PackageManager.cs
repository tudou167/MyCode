using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PackageManager : Singleton<PackageManager>
{
    public int Gold;

    private Dictionary<int, Item> packageItemDic;

    public void Bind(int gold, Dictionary<int, Item> packageItemDic)
    {
        Gold = gold;
        this.packageItemDic = packageItemDic;
    }

    public PackageData GetPackageData()
    {
        PackageData packageData = new PackageData();
        packageData.Gold = Gold;
        packageData.equipList = new List<Equip>();
        packageData.consumablesList = new List<Consumables>();
        foreach (Item item in packageItemDic.Values)
        {
            if (item is Equip)
            {
                packageData.equipList.Add(item as Equip);
            }
            else
            {
                packageData.consumablesList.Add(item as Consumables);
            }
        }
        return packageData;
    }

    //所有放进背包都要经过这个接口
    public int AddItem(Item item)
    {
        if (packageItemDic == null)
        {
            return 0;
        }
        //int itemID = packageItemDic.Count == 0 ? 1 : packageItemDic.Keys.Last<int>() + 1;
        int itemID = 0;
        if (packageItemDic.Count != 0)
        {
            foreach (int itemid in packageItemDic.Keys)
            {
                if (itemID < itemid) itemID = itemid;
            }
        }
        itemID += 1;
        if (item is Equip)
        {
            Equip equip = item as Equip;
            equip.itemID = itemID;
            packageItemDic.Add(itemID, equip);
        }
        else
        {
            Consumables consumables = item as Consumables;
            consumables.itemID = itemID;
            packageItemDic.Add(itemID, consumables);
        }
        return itemID;
    }
    //物品在背包的ItemID
    public T GetItem<T>(int ItemID) where T : class, new()
    {
        if (packageItemDic != null && packageItemDic.ContainsKey(ItemID))
        {
            if (packageItemDic[ItemID] is T)
            {
                return packageItemDic[ItemID] as T;
            }
        }

        return null;
    }
    public void DeleteItem(int itemID)
    {
        if (packageItemDic.ContainsKey(itemID)) packageItemDic.Remove(itemID);
    }

    public void InitData(int userID, int roleID)
    {
        string packagePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountPackageDirectoryPath;
        PackageData package = new PackageData(100000, new List<Item>() {
            ItemManager.Instance.GetItem(20001),
            ItemManager.Instance.GetItem(50001),
            ItemManager.Instance.GetItem(30001),
            ItemManager.Instance.GetItem(40001),
            ItemManager.Instance.GetItem(10001)
        });
        int index = 0;
        for (int i = 0; i < package.equipList.Count; i++)
        {
            index += 1;
            package.equipList[i].itemID = index;
        }
        for (int i = 0; i < package.consumablesList.Count; i++)
        {
            index += 1;
            package.consumablesList[i].itemID = index;
        }
        File.WriteAllText(packagePath + userID + "_" + roleID, Tool.Instance.ObjToJson(package));
    }
    public void SaveData(int userID, int roleID, PackageData data)
    {
        string packagePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountPackageDirectoryPath;
        File.WriteAllText(packagePath + userID + "_" + roleID, Tool.Instance.ObjToJson(data));
    }
    public void SaveData(PackageData data)
    {
        string packagePath = AllPath.Instance.accountDirectoryPath + UserManager.Instance.GetCurUser().ID + AllPath.Instance.accountPackageDirectoryPath;
        File.WriteAllText(packagePath + HeroManager.Instance.GetUserHeroID(), Tool.Instance.ObjToJson(data));
    }

}
