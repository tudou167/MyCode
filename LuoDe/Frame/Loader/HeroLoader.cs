using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//加载玩家所有英雄
public class HeroLoader : MonoBehaviour
{
    void Awake()
    {
        if (!Directory.Exists(AllPath.Instance.userDirectoryPath)) return;
        //TODO 用户ID 要动态获取
        string rolePath = AllPath.Instance.accountDirectoryPath + UserManager.Instance.GetCurUser().ID + AllPath.Instance.accountRoleDirectoryPath;
        string[] roleInfoArr = Directory.GetFiles(rolePath);

        Dictionary<string, RoleData> dic = new Dictionary<string, RoleData>();
        for (int i = 0; i < roleInfoArr.Length; i++)
        {
            RoleData roleData = Tool.Instance.JsonToObj<RoleData>(roleInfoArr[i]);
            string[] strArr = roleInfoArr[i].Split('/');
            string heroID = strArr[strArr.Length - 1];
            //角色配表里面 主角填的是背包里面的ItemID 怪物填的是静态的物品ID
            roleData.Weapon = PackageManager.Instance.GetItem<Equip>(roleData.WeaponId);
            roleData.Armor = PackageManager.Instance.GetItem<Equip>(roleData.ArmorId);
            dic.Add(heroID, roleData);
        }

        HeroManager.Instance.Bind(dic);
        
    }
}
