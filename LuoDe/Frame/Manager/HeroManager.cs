using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    private Dictionary<string, RoleData> heroDic;
    private Transform curHero;
    private RoleData curHeroData;
    private string userHeroID;
    public Transform GetCurHero()
    {
        return curHero == null ? null : curHero;
    }
    public RoleData GetCurHeroData()
    {
        return curHeroData == null ? null : curHeroData;
    }
    public void SetCurHero(Transform tf)
    {
        curHero = tf;
    }
    public string GetUserHeroID(string userHeroID = null)
    {
        if (userHeroID != null)
        {
            this.userHeroID = userHeroID;
        }
        return this.userHeroID;
    }

    public RoleData GetHero(string id)
    {
        if (heroDic != null && heroDic.ContainsKey(id))
        {
            curHeroData = heroDic[id];
            return heroDic[id];
        }
        return null;
    }

    public void Bind(Dictionary<string, RoleData> heroDic)
    {
        this.heroDic = heroDic;
    }

    public List<RoleData> GetAllHero()
    {
        if (heroDic == null) heroDic = new Dictionary<string, RoleData>();
        List<RoleData> roleList = new List<RoleData>() { };
        foreach (KeyValuePair<string, RoleData> item in heroDic)
        {
            roleList.Add(item.Value);
        }
        return roleList;
    }

    //添加一个新英雄
    public void AddHero(int userID, string name, int id = 10001)
    {
        if (!Directory.Exists(AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountRoleDirectoryPath)) return;

        RoleData role = RoleManager.Instance.InitData(userID, name);

        //初始装备
        PackageManager.Instance.InitData(userID, role.RoleID);

        //初始技能
        HeroSkillManager.Instance.InitData(userID, role.RoleID, role);

        //初始仓库
        WarehouseManager.Instance.InitData(userID, role.RoleID);
        heroDic.Add(userID + "_" + role.RoleID, role);
    }
    public void SaveData(int userID, int roleID, RoleData data)
    {
        string id = userID + "_" + roleID;
        string rolePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountRoleDirectoryPath + id;
        if (data == null)
        {
            File.Delete(rolePath);
            string skillPath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountSkillDirectoryPath + id;
            File.Delete(skillPath);
            string packagePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountPackageDirectoryPath + id;
            File.Delete(packagePath);
            string warehousePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountWarehouseDirectoryPath + id;
            File.Delete(warehousePath);
            heroDic.Remove(id);
            return;
        }
        File.WriteAllText(rolePath, Tool.Instance.ObjToJson(data));
    }
    public void SaveData(RoleData data)
    {
        int userID = UserManager.Instance.GetCurUser().ID;
        string id = userID + "_" + GetCurHeroData().ID;
        string rolePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountRoleDirectoryPath + id;
        if (data == null)
        {
            File.Delete(rolePath);
            string skillPath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountSkillDirectoryPath + id;
            File.Delete(skillPath);
            string packagePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountPackageDirectoryPath + id;
            File.Delete(packagePath);
            string warehousePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountWarehouseDirectoryPath + id;
            File.Delete(warehousePath);
            heroDic.Remove(id);
            return;
        }
        File.WriteAllText(rolePath, Tool.Instance.ObjToJson(data));
    }
}
