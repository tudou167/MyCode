using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ItemLoader : MonoBehaviour
{
    void Awake()
    {
        List<ItemData> data = JsonMapper.ToObject<List<ItemData>>(Resources.Load<TextAsset>(AllPath.Instance.jsonPath + "Item").text);
        Dictionary<int, Item> dic = new Dictionary<int, Item>();
        for (int i = 0; i < data.Count; i++)
        {
            data[i].DelegateFunc = SkillEffectManager.Instance.GetEffect(data[i].DelegateName);
            if (data[i].Type == ItemType.Consumables || data[i].Type == ItemType.ArmorMaterial || data[i].Type == ItemType.WeaponMaterial || data[i].Type == ItemType.Task || data[i].Type == ItemType.Other)
            {
                dic.Add(data[i].ID, new Consumables(data[i],1));
            }
            else
            {
                dic.Add(data[i].ID, new Equip(data[i]));
            }
        }
        ItemManager.Instance.Bind(dic);

    }
}
