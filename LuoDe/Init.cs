using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Init : MonoBehaviour
{
    //TODO 是否需要
    public static int userID;
    public static int roleID;
    void Awake()
    {
        //Debug.Log(Application.persistentDataPath);
        //顺序很重要
        Tool.Instance.InstantiateObj("Prefabs/Loader/ItemLoader", null);
        DontDestroyOnLoad(Tool.Instance.InstantiateObj("Prefabs/Loader/SkillEffectLoader", null));
        Tool.Instance.InstantiateObj("Prefabs/Loader/SkillInfoLoader", null);
        Tool.Instance.InstantiateObj("Prefabs/Loader/RoleLoader", null);
        Tool.Instance.InstantiateObj("Prefabs/Loader/NPCLoader", null);

        //TODO
        InitUserFolder();
        LoadUserList();

        //TODO
        //UserManager.Instance.AddUser(new UserData("admin", "admin123456"));
        //UserManager.Instance.AddUser(new UserData("admin2", "admin123456"));
        //userID = 1;
        //新建的时候记得打开
        //Debug.Log(Application.persistentDataPath);
        //UserManager.Instance.AddRole(userID);
        //UserManager.Instance.AddRole(userID);
        //if (UserManager.Instance.GetUserList().userList.Count != 0)
        //{
            
        //    if (HeroManager.Instance.GetAllHero().Count != 0)
        //    {
                
        //    }
        //}
        //Debug.Log(HeroManager.Instance.GetHero("1_1").Name);
        //Tool.Instance.InstantiateObj("Prefabs/Game/Hero", null

        DontDestroyOnLoad(GameObject.Find("UI"));
        DontDestroyOnLoad(GameObject.Find("voice"));
        Tool.Instance.InstantiateObj("Prefabs/UI/LBN/Login", GameObject.Find("UI/Canvas").transform);

    }
    public void LoadUserList()
    {
        if (!File.Exists(AllPath.Instance.userPath)) return;
        UserList data = new UserList(Tool.Instance.JsonToObj<List<UserData>>(AllPath.Instance.userPath));
        Dictionary <string, UserData> dic = new Dictionary<string, UserData>();
        for (int i = 0; i < data.userList.Count; i++)
        {
            dic.Add(data.userList[i].Username,data.userList[i]);
        }
        UserManager.Instance.Bind(dic);
    }
    //初始化用户列表文件 和 用户列表的文件夹
    public void InitUserFolder()
    {
        if (File.Exists(AllPath.Instance.userPath)) return;
        File.WriteAllText(AllPath.Instance.userPath, JsonMapper.ToJson(new UserList(new List<UserData>() { })));

        if (Directory.Exists(AllPath.Instance.userDirectoryPath)) return;
        Directory.CreateDirectory(AllPath.Instance.userDirectoryPath);
    }
}
