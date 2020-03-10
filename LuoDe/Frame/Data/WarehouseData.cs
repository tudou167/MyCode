using System.Collections.Generic;

public class WarehouseData
{
    public List<Equip> equipList;
    public List<Consumables> consumablesList;

    public WarehouseData() { }
    //把传进来的list进行分类
    public WarehouseData(List<Item> itemList)
    {
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
