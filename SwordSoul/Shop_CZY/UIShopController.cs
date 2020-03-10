using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIShopController : MonoSingletion<UIShopController>
{
    UIShopView sv;
    public override void Awake()
    {
        base.Awake();
        sv = GetComponent<UIShopView>();
    }
    void Start()
    {
        Init();
    }

    public void Init()
    {

        AllTools.Instance.DeleteAllChild(sv.itemListTrf);
        AllTools.Instance.DeleteAllChild(sv.itemHotTrf);
        GroceryStore shopList= AllToObject.Instance.GetShopList();
        //List<Item> aa = AllToObject.Instance.GetShopList();
        List<EquipmentTemp.Row.Row2> itemList = AllToObject.Instance.GetItemList();
        for (int i = 0; i < shopList.items.Count; i++)
        {
            for (int j = 0; j < itemList.Count; j++)
            {

                if (shopList.items[i].id == itemList[j].id)
                {
                    //热门 continuedTime是借来的字段
                    sv.Goods(itemList[j], shopList.items[i].hp);
                }

            }
        }
    }

    public void Buy()
    {
        EquipmentTemp.Row.Row2 data = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<UIShopItemController>()._Data;
        //EquipmentTemp.Row.Row2 temp = (EquipmentTemp.Row.Row2)MemberwiseClone();
        EquipmentTemp.Row.Row2 temp = data.Clone();

        if (PlayerModel.Instance._playerDaraInfo.gold < temp.buy)
        {
           // Debug.Log("No Money No Talk");
            AllTools.Instance.Alert("所持金币不够!");
            return;
        }
        PlayerModel.Instance.AddGold(-temp.buy);
        AllTools.Instance.Alert("购买成功!");
        UIPackageModel.Instance.InItem(temp);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
