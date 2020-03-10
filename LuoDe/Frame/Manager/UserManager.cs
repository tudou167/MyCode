using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class UserManager : Singleton<UserManager>
{
    private Dictionary<string, UserData> userDic;
    private UserData curUser;

    public UserData GetUser(string username)
    {
        if (userDic != null && userDic.ContainsKey(username))
        {
            curUser = userDic[username];
            return userDic[username];
        }
        return null;
    }

    public UserData GetCurUser()
    {
        if (curUser != null) return curUser;
        return null;
    }

    public UserList GetUserList()
    {
        if (userDic == null) return null;

        UserList userList = new UserList(new List<UserData>() { });
        foreach (UserData item in userDic.Values)
        {
            userList.userList.Add(item);
        }
        return userList;
    }
    public int AddUser(UserData data)
    {
        //TODO动态添加用户ID
        if (userDic == null || !File.Exists(AllPath.Instance.userPath) || userDic.ContainsKey(data.Username)) return 0;
        data.ID = userDic.Count + 1;
        userDic.Add(data.Username, data);
        List<UserData> dataList = Tool.Instance.JsonToObj<List<UserData>>(AllPath.Instance.userPath);
        dataList.Add(data);
        File.WriteAllText(AllPath.Instance.userPath, Tool.Instance.ObjToJson(dataList));
        InitAccountTable(data.ID);

        return data.ID;
    }
    public void SaveUser(UserData data)
    {
        if (userDic == null || !File.Exists(AllPath.Instance.userPath) || !userDic.ContainsKey(data.Username)) return;
        userDic[data.Username] = data;
        List<UserData> dataList = Tool.Instance.JsonToObj<List<UserData>>(AllPath.Instance.userPath);
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].ID == data.ID)
            {
                dataList[i] = data;
            }
        }
        File.WriteAllText(AllPath.Instance.userPath, Tool.Instance.ObjToJson(dataList));
    }

    public void Bind(Dictionary<string, UserData> userDic)
    {
        this.userDic = userDic;
    }

    private void InitAccountTable(int userID)
    {
        if (!Directory.Exists(AllPath.Instance.userDirectoryPath) || Directory.Exists(AllPath.Instance.userDirectoryPath + userID)) return;
        Directory.CreateDirectory(AllPath.Instance.accountDirectoryPath + userID);

        string rolePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountRoleDirectoryPath;
        string packagePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountPackageDirectoryPath;
        string skillPath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountSkillDirectoryPath;
        string warehousePath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountWarehouseDirectoryPath;

        //账号背包文件夹
        Directory.CreateDirectory(packagePath);
        //账号仓库文件夹
        Directory.CreateDirectory(warehousePath);
        //账号技能文件夹
        Directory.CreateDirectory(skillPath);
        //账号角色文件夹
        Directory.CreateDirectory(rolePath);
    }

}
