using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class Log_in : MonoSingletion<Log_in> {

    public InputField Username;
    public InputField Password;
    public string _userName;
    public void Logon () {
        AllTools.Instance.Load (AllPaths.Instance.Reg_logPrefabs + "Log_on");

    }

    public Transform LoginClick () {
        if (!File.Exists (AllPaths.Instance.AccountJsonPath)) {
            AllTools.Instance.Alert ("请先注册");
            return null;
        }
        UserInfoList Userlist = AllToObject.Instance.GetUserInfo ();
        List<UserInfo> list = Userlist.User;
        for (int i = 0; i < list.Count; i++) {
            //和即将注册的用户名进行比较,如果相同,说明已经注册过

            if (list[i].playerDataInfo.userName == Username.text) {

                if (list[i].playerDataInfo.password != Password.text) {
                    AllTools.Instance.Alert ("密码错误");
                    return null;
                } else {
                    // AllTools.Instance.Alert("登录成功");
                    Transform temp = AllTools.Instance.Load ("Prefabs/Reg_log/Panel_UI").transform;
                    _userName = list[i].playerDataInfo.userName;
                    PlayerModel.Instance.userName = _userName; //登录的账号等于 M层的账号 
                    PlayerModel.Instance._characterInfo = list[i].characterInfo.characterInfos[0]; //M层获得这个账号下的玩家属性
                    PlayerModel.Instance._bossInfo = list[i].characterInfo.characterInfos[1];
                    PlayerModel.Instance._playerDaraInfo = list[i].playerDataInfo; //M层获得这个账号下玩家的基础信息
                    //获取唯一ID
                    UIPackageModel.Instance.userID = i;
                    transform.DOScale (0, 0.3f).OnComplete (() => { transform.parent.Find ("register").gameObject.SetActive (false); });
                    //初始化数据
                    list[i].characterInfo.characterInfos[0].hp=list[i].characterInfo.characterInfos[0].maxHp;
                    list[i].characterInfo.characterInfos[1].hp=list[i].characterInfo.characterInfos[1].maxHp;
                    PlayerModel.Instance.SaveCharacterInfo (list[i].characterInfo.characterInfos[0]);
                    PlayerModel.Instance.SaveBossInfo (list[i].characterInfo.characterInfos[1]);
                    return temp;
                }

            }
        }
        AllTools.Instance.Alert ("当前用户不存在");
        return null;

    }
    public void GOback () {

        Destroy (gameObject);
    }
}