using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopView : MonoBehaviour {

    public Transform itemListTrf;
    public Transform itemHotTrf;
    void Awake()
    {
        itemListTrf = transform.Find("GameObject/Scroll View/Viewport/Content");
        itemHotTrf = transform.Find("GameObject/Hot/GameObject");
    }
    public void Goods(EquipmentTemp.Row.Row2 data,int isHot)
    {

        GameObject prefab = null;
        if (isHot == 1)
        {
            prefab = AllTools.Instance.LoadCell(AllPaths.Instance.shopPrefabs + "ShopItem", itemHotTrf);
            prefab.GetComponent<UIShopItemController>()._Data = data;
        }
        else
        {
            prefab = AllTools.Instance.LoadCell(AllPaths.Instance.shopPrefabs + "ShopItem", itemListTrf);
            prefab.GetComponent<UIShopItemController>()._Data = data;

        }
        prefab.GetComponent<UIShopItemController>()._Data = data;
        prefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(AllPaths.Instance.allIconPath + data.id);
        prefab.transform.Find("Name").GetComponent<Text>().text = data.name;
        prefab.transform.Find("Gold").GetComponent<Text>().text = "金币：" + data.buy.ToString();
        prefab.transform.Find("Buy").GetComponent<Button>().onClick.AddListener(()=> { UIShopController.Instance.Buy(); });
    }
}
