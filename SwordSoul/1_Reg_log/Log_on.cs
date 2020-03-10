using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class UserInfo
{
    public CharacterInfoList characterInfo; //玩家属性
    public PlayerDataInfo playerDataInfo;   //玩家信息
}
public class UserInfoList
{
    public List<UserInfo> User;    
}
public class Log_on : MonoSingletion<Log_on>
{

    public InputField Username;
    public InputField Password;
    public InputField RPassword;


    public void RegisterClick()
    {

        if (Username.text == "" || Password.text == "" || RPassword.text == "")
        {
            AllTools.Instance.Alert("用户信息不完整");
            return;
        }

        if (Password.text != RPassword.text)
        {
            AllTools.Instance.Alert("两次输入密码不一致");
            return;
        }

        
        //可能是1(之前没有注册过)
        //存储JSON的文件应该是不存在的

        if (!File.Exists(AllPaths.Instance.AccountJsonPath))   //通过文件不存在,判定出之前没有注册过
        {

            //JsonData 生成列表
            WriteJson();
        }
        //可能性2(之前注册过)
        //存储的JSON的文件应该是存在的
        else
        {
            //JsonData 生成列表
            UserInfoList list = AllToObject.Instance.GetUserInfo();
            WriteJson(list);

            //把列表的JSON数据,写入文件中
            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(list), System.Text.Encoding.UTF8);
        }


       
    }
    public void WriteJson(UserInfoList list=null)
    {
        if (list==null)
        {
            list = new UserInfoList();
            list.User = new List<UserInfo>();
        }
        for (int i = 0; i < list.User.Count; i++)
        {
            if (list.User[i].playerDataInfo.userName==Username.text)
            {
                AllTools.Instance.Alert("该账号已被注册!");
                return;
            }
        }
        
        UserInfo userInfo = new UserInfo();
        userInfo.characterInfo = AllToObject.Instance.GetCharacterInfo();
        userInfo.characterInfo.characterInfos[0].name = Username.text;
        userInfo.playerDataInfo = AllToObject.Instance.GetPlayDataInfo();
        userInfo.playerDataInfo.userName = Username.text;
        userInfo.playerDataInfo.password = Password.text;

        //Debug.Log(userInfo.characterInfo.characterInfos[1].name);

        //生成一行的数据
        list.User.Add(userInfo);

        //把列表的JSON数据,写入文件中
        File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(list), System.Text.Encoding.UTF8);
        //保存用户数据
        PlayerPrefs.SetString("Name", Username.text);
        PlayerPrefs.SetString("Password", Password.text);


        //跨页面传递参数 把刚刚注册的账号密码传递到登录页面
        Log_in login = transform.parent.Find("Log_in").GetComponent<Log_in>();
        login.Username.text = Username.text;
        login.Password.text = Password.text;


        //添加用户背包
        UIPackageModel.Instance.NewPackage();

        //将关闭页面的回调方法，给Alert传递过去
        AllTools.Instance.Alert("注册成功", GObackClick); 

    }
    public void GObackClick()
    {
        Destroy(gameObject);
      
    }
}
