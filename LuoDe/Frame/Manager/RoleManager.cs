using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoleManager : Singleton<RoleManager>
{
    private Dictionary<int, RoleData> roleDic;

    public RoleData GetRole(int id)
    {
        if (roleDic != null && roleDic.ContainsKey(id))
        {
            return roleDic[id];
        }
        return null;
    }

    public void Bind(Dictionary<int, RoleData> roleDic)
    {
        this.roleDic = roleDic;
    }

    public List<RoleData> GetAllRole()
    { 
        List<RoleData> roleList = new List<RoleData>();
        foreach (RoleData item in roleDic.Values)
        {
            roleList.Add(item);
        }
        return roleList;
    }

    public RoleData InitData(int userID, string name, int id = 10001)
    {
        string rolePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountRoleDirectoryPath;
        string[] roleInfoArr = Directory.GetFiles(rolePath);
        RoleData tmp = GetRole(id);
        RoleData role = new RoleData(
            tmp.ID,
            tmp.Name,
            tmp.Prefab,
            tmp.HP,
            tmp.BaseExp,
            tmp.LV,
            tmp.Exp,
            tmp.ATK,
            tmp.DEF,
            tmp.Speed,
            tmp.ATKSpeed,
            tmp.Crit,
            tmp.Repel,
            tmp.WeaponId,
            tmp.ArmorId,
            null,
            null,
            tmp.StrSkillList,
            null
            );
        role.Name = name;

        List<RoleData> heroList = HeroManager.Instance.GetAllHero();
        int count = 0;
        for (int i = 0; i < heroList.Count; i++)
        {
            count = heroList[i].RoleID > count ? heroList[i].RoleID : count;
        }
        count += 1;
        role.RoleID = count;

        File.WriteAllText(rolePath + userID + "_" + role.RoleID, Tool.Instance.ObjToJson(role));

        return role;
    }

}
