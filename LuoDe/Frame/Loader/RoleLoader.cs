using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleLoader : MonoBehaviour
{
    void Awake()
    {
        List<RoleData> data = JsonMapper.ToObject<List<RoleData>>(Resources.Load<TextAsset>(AllPath.Instance.jsonPath + "Role").text);
        Dictionary<int, RoleData> dic = new Dictionary<int, RoleData>();
        for (int i = 0; i < data.Count; i++)
        {
            RoleData roledata = new RoleData(
                data[i].ID,
                data[i].Name,
                data[i].Prefab,
                data[i].HP,
                data[i].BaseExp,
                data[i].LV,
                data[i].Exp,
                data[i].ATK,
                data[i].DEF,
                data[i].Speed,
                data[i].ATKSpeed,
                data[i].Crit,
                data[i].Repel,
                data[i].WeaponId,
                data[i].ArmorId,
                ItemManager.Instance.GetItem(data[i].WeaponId == 1 ? 20001 : data[i].WeaponId) as Equip,//因为主角填的是动态ID
                ItemManager.Instance.GetItem(data[i].ArmorId == 2 ? 50001 : data[i].ArmorId) as Equip,
                data[i].StrSkillList,
                SkillInfoManager.Instance.GetSkillList(data[i].StrSkillList)
                );
           dic.Add(data[i].ID, roledata);
        }
        RoleManager.Instance.Bind(dic);
    }
}
