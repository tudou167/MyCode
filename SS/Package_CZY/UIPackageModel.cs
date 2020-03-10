using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System;
using UnityEngine.Events;

[Serializable]
public class EquipmentTemp
{
    [Serializable]
    public class Row
    {
        public class Row2
        {
            public int id;
            public int eid;
            public string name;
            public int InitialLevel;
            public int atk;
            public int def;
            public int crit;
            public Enumclass.Quality quality;
            public int hp;
            public int buy;
            public Enumclass.ItemType itemType;
            public string description;
            public int count;
            public int isWear;
            public Row2 Clone()
            {

                return (Row2)MemberwiseClone();

            }
        }
        public List<Row2> Data;


    }
    public List<Row> User;
}

public class UIPackageModel : CSharpSingletion<UIPackageModel>
{
    public int userID;

    public List<EquipmentTemp.Row.Row2> LoadUserPackageData()
    {
        if (File.Exists(AllPaths.Instance.userPackagePath))
        {
            EquipmentTemp UserPackageData = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>();

            return UserPackageData.User[userID].Data;
        }
        return null;
    }

    public delegate bool SortDelegate(EquipmentTemp.Row.Row2 a, EquipmentTemp.Row.Row2 b);
    private bool SortByID(EquipmentTemp.Row.Row2 a, EquipmentTemp.Row.Row2 b)
    {
        return a.id > b.id;
    }
    private bool SortByQuality(EquipmentTemp.Row.Row2 a, EquipmentTemp.Row.Row2 b)
    {
        return a.quality > b.quality;
    }
    private bool SortByItemType(EquipmentTemp.Row.Row2 a, EquipmentTemp.Row.Row2 b)
    {
        return a.itemType > b.itemType;
    }
    private bool SortBySellPrice(EquipmentTemp.Row.Row2 a, EquipmentTemp.Row.Row2 b)
    {
        return AllFormula.Instance.SellPrice(a.buy, a.InitialLevel) > AllFormula.Instance.SellPrice(b.buy, b.InitialLevel);
    }
    private List<EquipmentTemp.Row.Row2> SortFunc(List<EquipmentTemp.Row.Row2> data, SortDelegate sort)
    {
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = i; j < data.Count - 1; j++)
            {
                if (sort(data[i], data[j + 1]))
                {
                    EquipmentTemp.Row.Row2 temp = data[i];
                    data[i] = data[j + 1];
                    data[j + 1] = temp;
                }
            }
        }

        return data;
    }

    public List<EquipmentTemp.Row.Row2> Sort(List<EquipmentTemp.Row.Row2> data, int sortType)
    {
        switch (sortType)
        {
            case 0:
                data = SortFunc(data, SortByID);
                break;
            case 1:
                data = SortFunc(data, SortByQuality);
                break;
            case 2:
                data = SortFunc(data, SortByItemType);
                break;
            case 3:
                data = SortFunc(data, SortBySellPrice);
                Debug.Log("改");
                break;
        }

        EquipmentTemp lastData = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>();
        lastData.User[userID].Data = data;
        SaveAllUserData(lastData);
        return data;
    }

    public void SaveAllUserData(EquipmentTemp data)
    {
        File.WriteAllText(AllPaths.Instance.userPackagePath, JsonMapper.ToJson(data));
    }

    public List<EquipmentTemp.Row.Row2> Sell(EquipmentTemp.Row.Row2 data, int count = 1)
    {
        List<EquipmentTemp.Row.Row2> getInfo = null;
        EquipmentTemp UserPackageData = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>();
        for (int i = 0; i < UserPackageData.User[userID].Data.Count; i++)
        {
            if (UserPackageData.User[userID].Data[i].eid == data.eid)
            {
                PlayerModel.Instance.AddGold(AllFormula.Instance.SellPrice(UserPackageData.User[userID].Data[i].buy, UserPackageData.User[userID].Data[i].InitialLevel, count));
                
                if (UserPackageData.User[userID].Data[i].count == count)
                {
                    EquipmentTemp.Row.Row2 temp1 = UserPackageData.User[userID].Data[i];
                    UserPackageData.User[userID].Data.Remove(temp1);
                    if ((int)temp1.itemType < 8 && temp1.isWear == 1)
                    {
                        CharacterInfo userData = PlayerModel.Instance._characterInfo;
                        userData.atk -= temp1.atk;
                        userData.def -= temp1.def;
                        userData.crit -= temp1.crit;
                        userData.maxHp -= temp1.hp;
                        if (userData.hp >= userData.maxHp)
                        {
                            userData.hp = userData.maxHp;
                        }
                        else
                        {
                            userData.hp -= temp1.hp;
                        }
                        PlayerModel.Instance.SaveCharacterInfo(userData);
                    }
                }
                else
                {
                    UserPackageData.User[userID].Data[i].count -= count;
                }
                getInfo = UserPackageData.User[userID].Data;
            }
        }
        SaveAllUserData(UserPackageData);
        return getInfo;
    }

    public void InItem(EquipmentTemp.Row.Row2 data, int count = 1)
    {
        EquipmentTemp UserPackageData = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>();
        EquipmentTemp.Row packData = UserPackageData.User[userID];
        for (int i = 0; i < packData.Data.Count; i++)
        {
            if (packData.Data[i].eid == data.id && (int)data.itemType >= 8)
            {
                packData.Data[i].count += count;
                SaveAllUserData(UserPackageData);
                return;
            }
        }


        data.count = count;
        data.eid = data.id;
        if (UserPackageData.User[userID].Data.Count == 0)
        {
            data.id = 1;
        }
        else
        {
            data.id = UserPackageData.User[userID].Data[UserPackageData.User[userID].Data.Count - 1].id + 1;

        }
        data.InitialLevel = 1;

        UserPackageData.User[userID].Data.Add(data);
        SaveAllUserData(UserPackageData);
    }

    public EquipmentTemp.Row.Row2 WearEquip(EquipmentTemp.Row.Row2 data)
    {
        EquipmentTemp UserPackageData = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>();
        EquipmentTemp.Row.Row2 original = null;
        for (int i = 0; i < UserPackageData.User[userID].Data.Count; i++)
        {
            if (UserPackageData.User[userID].Data[i].itemType == data.itemType && UserPackageData.User[userID].Data[i].isWear == 1)
            {
                UserPackageData.User[userID].Data[i].isWear = 0;
                original = UserPackageData.User[userID].Data[i];
            }

            if (UserPackageData.User[userID].Data[i].id == data.id)
            {
                UserPackageData.User[userID].Data[i].isWear = 1;
            }
        }
        SaveAllUserData(UserPackageData);
        return original;
    }

    public void NewPackage()
    {
        if (!File.Exists(AllPaths.Instance.fileUserPackageStaticPath))
        {
            Debug.LogError("没有用户背包生成表");
            return;
        }

        string staticUserPackage = AllJson.Instance.userPackageStaticJson();
        EquipmentTemp UserPackageData = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>();
        List<EquipmentTemp.Row.Row2> newPackageInit = JsonMapper.ToObject<List<EquipmentTemp.Row.Row2>>(staticUserPackage);

        EquipmentTemp.Row row = new EquipmentTemp.Row();
        row.Data = new List<EquipmentTemp.Row.Row2>();
        for (int i = 0; i < newPackageInit.Count; i++)
        {
            row.Data.Add(newPackageInit[i]);
        }
        UserPackageData.User.Add(row);

        SaveAllUserData(UserPackageData);
    }

    public EquipmentTemp.Row.Row2 GainItem()
    {
        List<EquipmentTemp.Row.Row2> itemList = AllToObject.Instance.GetItemList();
        return randItem(itemList);
    }

    private EquipmentTemp.Row.Row2 randItem(List<EquipmentTemp.Row.Row2> itemList)
    {
        System.Random rand = new System.Random();
        int randNum = rand.Next(0, itemList.Count);
        if ((int)itemList[randNum].quality <= 2 && (int)itemList[randNum].itemType != 10)
        {
            return itemList[randNum];
        }
        else
        {
            itemList.Remove(itemList[randNum]);
            return randItem(itemList);
        }
    }

    public EquipmentTemp.Row.Row2 Intensify(EquipmentTemp.Row.Row2 data)
    {
        int need = data.InitialLevel;
        if (need >= 5)
        {
            need = 5;
        }
        EquipmentTemp UserPackageData = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>();
        List<EquipmentTemp.Row.Row2> userPackage = UserPackageData.User[userID].Data;

        for (int i = 0; i < userPackage.Count; i++)
        {
            if ((int)userPackage[i].itemType == 8 && userPackage[i].quality == data.quality && userPackage[i].count >= need)
            {
                userPackage[i].count -= need;
                if (userPackage[i].count == 0)
                {
                    userPackage.Remove(userPackage[i]);
                }
                for (int j = 0; j < userPackage.Count; j++)
                {
                    if (userPackage[j].id == data.id)
                    {
                        userPackage[j].InitialLevel += 1;
                        int tempLv = userPackage[j].InitialLevel;
                        int atk = AllFormula.Instance.GetEquipProperty(userPackage[j].atk, tempLv);
                        int def = AllFormula.Instance.GetEquipProperty(userPackage[j].def, tempLv);
                        int crit = AllFormula.Instance.GetEquipProperty(userPackage[j].crit, tempLv);
                        int hp = AllFormula.Instance.GetEquipProperty(userPackage[j].hp, tempLv);
                        userPackage[j].atk = atk;
                        userPackage[j].def = def;
                        userPackage[j].crit = crit;
                        userPackage[j].hp = hp;
                        data.InitialLevel += 1;
                        data.atk = atk;
                        data.def = def;
                        data.crit = crit;
                        data.hp = hp;
                    }
                }
            }
        }

        UserPackageData.User[userID].Data = userPackage;

        SaveAllUserData(UserPackageData);
        return data;
    }

    public EquipmentTemp.Row.Row2 GetSomeItemByName(string name, int count = 1)
    {
        List<EquipmentTemp.Row.Row2> itemList = AllToObject.Instance.GetItemList();

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].name == name)
            {
                itemList[i].count = count;
                return itemList[i];
            }
        }
        return null;
    }
}


