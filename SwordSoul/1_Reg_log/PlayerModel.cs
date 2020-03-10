using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class PlayerModel : CSharpSingletion<PlayerModel> {
    public UserInfoList info;
    public UserInfo _userInfo;
    public int infoUserListNum;
    public string userName;
    private string _userJson;
    public CharacterInfo _characterInfo;
    public CharacterInfo _bossInfo;
    public PlayerDataInfo _playerDaraInfo;
    /// <summary>
    /// 玩家属性枚举
    /// </summary>     
    //public enum CharacterInfo_Typ
    //{

    //    atk = 0,// -攻击力
    //    def = 1,// -防御力
    //    crit = 2,// -暴击率
    //    lv = 3,//等级
    //    hp = 4,// -血量
    //    maxHp = 5// -最大血量

    //}
    protected override void Init () {
        GetInfo ();
        //   GameDebug.Log("PlayerModel初始化成功!");
    }

    /// <summary>
    /// 玩家信息枚举
    /// </summary>
    //public enum PlayerDataInfo_Typ
    //{
    //    registerDay = 0,  //签到天数
    //    exp = 1,          //经验
    //    gold = 2,         //金币
    //    diamond = 3       //钻石

    //}

    //public void SaveData()
    //{
    //    JsonData jsonData = new JsonData();
    //    string str1 = JsonMapper.ToJson(Player.Instance.characterInfo);//CharacterInfo 把玩家的属性数据存入玩家JSON表里
    //    string str2 = JsonMapper.ToJson(Player.Instance.playerData);   //PlayerDataInfo 玩玩家的个人数据存入玩家JSON表里
    //    JsonData json1 = JsonMapper.ToObject(str1);
    //    JsonData json2 = JsonMapper.ToObject(str2);
    //    jsonData["CharacterInfo"] = json1;
    //    jsonData["PlayerDataInfo"] = json2;

    //    string strJosn = jsonData.ToJson();
    //    // Debug.Log(strJosn);
    //    File.WriteAllText(AllPaths.Instance.AccountJsonPath, strJosn);
    //}

    public T LoadData<T> (string Path) {
        string str = File.ReadAllText (Path);
        T jsonData = JsonMapper.ToObject<T> (str);
        return jsonData;
    }
    public UserInfo GetInfo () {
        info = AllToObject.Instance.GetUserInfo ();
        for (int i = 0; i < info.User.Count; i++) {
            if (info.User[i].playerDataInfo.userName == userName) {
                infoUserListNum = i;
                _userInfo = info.User[i];
                return _userInfo;
            }
        }
        return null;
    }

    public UserInfoList DisplayUserInfoList () {
        string temp = AllJson.Instance.UserInfoJson ();
        if (_userJson == temp) {
            return info;
        } else {
            _userJson = temp;
            info = AllToObject.Instance.GetUserInfo ();
            _userInfo = GetInfo ();
            return info;
        }
    }
    public void SaveCharacterInfo (CharacterInfo data) {
        info.User[infoUserListNum].characterInfo.characterInfos[0] = data;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));
        DisplayUserInfoList ();
        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();
        PlayerDataInfoView.Instance.Display_BossDataInfo ();
    }
    public void SaveBossInfo (CharacterInfo data) {
        info.User[infoUserListNum].characterInfo.characterInfos[1] = data;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));
        DisplayUserInfoList ();
        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();
        PlayerDataInfoView.Instance.Display_BossDataInfo ();
    }

    #region 玩家属性方法 攻击力 防御力 暴击 等级 血量 最大血量
    public int GetAtk () {
        return _characterInfo.atk;
    }
    public int AddAtk (int atk) {
        DisplayUserInfoList ();
        Mathf.Clamp (_characterInfo.atk, 0, 99999);
        _characterInfo.atk += atk;
        info.User[infoUserListNum].characterInfo.characterInfos[0].atk = _characterInfo.atk;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //TODO 发送事件
        //UIMainMenRoleCountView.Instance.Display ();
        return _characterInfo.atk;
    }

    public int GetDef () {
        return _characterInfo.def;
    }
    public int AddDef (int def) {
        DisplayUserInfoList ();
        Mathf.Clamp (_characterInfo.def, 0, 99999);
        _characterInfo.def += def;
        info.User[infoUserListNum].characterInfo.characterInfos[0].def = _characterInfo.def;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //UIMainMenRoleCountView.Instance.Display ();

        return _characterInfo.def;
    }

    public int GetCrit () {
        return _characterInfo.crit;
    }
    public int AddCrit (int crit) {
        DisplayUserInfoList ();
        Mathf.Clamp (_characterInfo.crit, 0, 99999);
        _characterInfo.crit += crit;
        info.User[infoUserListNum].characterInfo.characterInfos[0].crit = _characterInfo.crit;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //UIMainMenRoleCountView.Instance.Display ();

        return _characterInfo.crit;
    }

    public int GetLv () {
        return _characterInfo.lv;
    }
    public int AddLv (int lv) {
        DisplayUserInfoList ();
        Mathf.Clamp (_characterInfo.lv, 0, 50);
        _characterInfo.lv += lv;
        info.User[infoUserListNum].characterInfo.characterInfos[0].lv = _characterInfo.lv;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //UIMainMenRoleCountView.Instance.Display ();

        return _characterInfo.lv;
    }

    public int GetHp () {
        return _characterInfo.hp;
    }
    public int AddHp (int hp) {

        _characterInfo.hp += hp;
        if (_characterInfo.hp <= 0) _characterInfo.hp = 0;
        info.User[infoUserListNum].characterInfo.characterInfos[0].hp = _characterInfo.hp;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //UIMainMenRoleCountView.Instance.Display ();
        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();
        DisplayUserInfoList ();
        return _characterInfo.hp;
    }
    public int GetBossHp () {
        return _bossInfo.hp;
    }
    public int AddBossHp (int hp) {
        _bossInfo.hp += hp;
        if (_bossInfo.hp <= 0) _bossInfo.hp = 0;
        info.User[infoUserListNum].characterInfo.characterInfos[1].hp = _bossInfo.hp;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //UIMainMenRoleCountView.Instance.Display ();
        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();
        PlayerDataInfoView.Instance.Display_BossDataInfo ();
        DisplayUserInfoList ();
        return _bossInfo.hp;
    }

    public int GetMaxHp () {
        return _characterInfo.maxHp;
    }
    public int AddMaxHp (int MaxHp) {
        DisplayUserInfoList ();
        Mathf.Clamp (_characterInfo.maxHp, 0, 99999);
        _characterInfo.maxHp += MaxHp;
        info.User[infoUserListNum].characterInfo.characterInfos[0].maxHp = _characterInfo.maxHp;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //UIMainMenRoleCountView.Instance.Display ();
        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();

        return _characterInfo.maxHp;
    }

    #endregion

    #region 玩家基本信息方法 登录时间  签到天数 经验 金币 钻石
    public string LastLoadTime (string lastLoadTime) //上次登录时间
    {
        DisplayUserInfoList ();
        _playerDaraInfo.lastLoadTime = lastLoadTime;

        // info.User[i].playerDataInfo.lastLoadTime = lastLoadTime;
        info.User[infoUserListNum].playerDataInfo.lastLoadTime = _playerDaraInfo.lastLoadTime;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();
        return _playerDaraInfo.lastLoadTime;
    }

    public int GetRegisterDay () {
        return _playerDaraInfo.registerDay;
    }
    public int AddRegisterDay (int registerDay) //签到天数
    {
        DisplayUserInfoList ();
        _playerDaraInfo.registerDay += registerDay;

        info.User[infoUserListNum].playerDataInfo.registerDay = _playerDaraInfo.registerDay;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();

        return _playerDaraInfo.registerDay;

    }

    public int AddRegisterDay_zero (int registerDay) //签到天数归零
    {
        DisplayUserInfoList ();
        _playerDaraInfo.registerDay = registerDay;

        info.User[infoUserListNum].playerDataInfo.registerDay = _playerDaraInfo.registerDay;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();

        return _playerDaraInfo.registerDay;

    }

    public int GetExp () {
        return _playerDaraInfo.exp;
    }
    public int AddExp (int exp) //经验
    {
        DisplayUserInfoList ();
        _playerDaraInfo.exp += exp;
        info.User[infoUserListNum].playerDataInfo.exp = _playerDaraInfo.exp;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        //UIMainMenRoleCountView.Instance.Display ();
        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();

        return _playerDaraInfo.exp;
    }
    public int GetGold () {
        return _playerDaraInfo.gold;
    }
    public int AddGold (int gold) //金币
    {
        DisplayUserInfoList ();
        _playerDaraInfo.gold += gold;
        info.User[infoUserListNum].playerDataInfo.gold = _playerDaraInfo.gold;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();

        return _playerDaraInfo.gold;
    }
    public int GetDiamond () {
        return _playerDaraInfo.diamond;
    }
    public int AddDiamond (int diamond) //钻石
    {
        DisplayUserInfoList ();
        _playerDaraInfo.diamond += diamond;
        info.User[infoUserListNum].playerDataInfo.diamond = _playerDaraInfo.diamond;
        File.WriteAllText (AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson (info));

        PlayerDataInfoView.Instance.Display_PlayerDataInfo ();
        return _playerDaraInfo.diamond;
    }

    #endregion

    /// <summary>
    /// 玩家属性变动传值方法
    /// </summary>
    /// <param name="characrter"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /*public CharacterInfo CharacterInfo(CharacterInfo_Typ characrter, int data)
    {
        switch (characrter)
        {
            case CharacterInfo_Typ.atk:
                _userInfo.characterInfo.characterInfos[0].atk += data;
                break;
            case CharacterInfo_Typ.def:
                _userInfo.characterInfo.characterInfos[0].def += data;
                break;
            case CharacterInfo_Typ.crit:

                _userInfo.characterInfo.characterInfos[0].crit += data;
                break;
            case CharacterInfo_Typ.lv:
                _userInfo.characterInfo.characterInfos[0].lv += data;
                break;
            case CharacterInfo_Typ.hp:
                _userInfo.characterInfo.characterInfos[0].hp += data;
                if (_userInfo.characterInfo.characterInfos[0].hp >= _userInfo.characterInfo.characterInfos[0].maxHp)
                {
                    _userInfo.characterInfo.characterInfos[0].hp = _userInfo.characterInfo.characterInfos[0].maxHp;
                }
                break;
            case CharacterInfo_Typ.maxHp:
                _userInfo.characterInfo.characterInfos[0].maxHp += data;
                break;
            default:
                break;
        }

        //序列化后再写回文件（存档）
        File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
        return _userInfo.characterInfo.characterInfos[0];
    }
    */

    /// <summary>
    /// 玩家基础信息变动传值方法
    /// </summary>
    /// <param name="player"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /*public PlayerDataInfo PlayerDataInfo(PlayerDataInfo_Typ player, int data)
    //{

    //    userName = Log_in.Instance._userName;
    //    info = AllToObject.Instance.GetUserInfo();
    //    for (int i = 0; i < info.User.Count; i++)
    //    {
    //        if (info.User[i].playerDataInfo.userName == userName)
    //        {
    //            switch (player)
    //            {
    //                case PlayerDataInfo_Typ.registerDay:
    //                    info.User[i].playerDataInfo.registerDay += data;
    //                    break;
    //                case PlayerDataInfo_Typ.exp:
    //                    info.User[i].playerDataInfo.exp += data;
    //                    break;
    //                case PlayerDataInfo_Typ.gold:
    //                    info.User[i].playerDataInfo.gold += data;
    //                    break;
    //                case PlayerDataInfo_Typ.diamond:
    //                    info.User[i].playerDataInfo.diamond += data;
    //                    break;
    //                default:
    //                    break;
    //            }



    //            //序列化后再写回文件（存档）
    //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
    //            return info.User[i].playerDataInfo;
    //        }
    //    }

        return null;


    }
    */

    /// <summary>
    /// 上次登录时间
    /// </summary>
    /// <param name="lastLoadTime"></param>
    /// <returns></returns>
    //public PlayerDataInfo LastLoadTime(string lastLoadTime)
    //{

    //    userName = Log_in.Instance._userName;
    //    info = AllToObject.Instance.GetUserInfo();
    //    for (int i = 0; i < info.User.Count; i++)
    //    {
    //        if (info.User[i].playerDataInfo.userName == userName)
    //        {
    //            info.User[i].playerDataInfo.lastLoadTime = lastLoadTime;
    //            //序列化后再写回文件（存档）
    //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
    //            return info.User[i].playerDataInfo;
    //        }
    //    }

    //    return null;

    //}

    /*  

        //public CharacterInfo Atk(int atk)
        //{
        //    userName = Log_in.Instance._userName;
        //    info = AllToObject.Instance.GetUserInfo();
        //    for (int i = 0; i < info.User.Count; i++)
        //    {
        //        if (info.User[i].playerDataInfo.userName == userName)
        //        {
        //            info.User[i].characterInfo.characterInfos[0].atk += atk;
        //            //序列化后再写回文件（存档）
        //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
        //            return info.User[i].characterInfo.characterInfos[0];
        //        }

        //        CharacterInfo(CharacterInfo_Typ.def, 345);
        //    }

        //    return null;

        //}

        //public CharacterInfo Def(int def)
        //{

        //    userName = Log_in.Instance._userName;
        //    info = AllToObject.Instance.GetUserInfo();
        //    for (int i = 0; i < info.User.Count; i++)
        //    {
        //        if (info.User[i].playerDataInfo.userName == userName)
        //        {
        //            info.User[i].characterInfo.characterInfos[0].def += def;
        //            //序列化后再写回文件（存档）
        //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
        //            return info.User[i].characterInfo.characterInfos[0];
        //        }
        //    }

        //    return null;

        //}
        //public CharacterInfo Crit(int crit)
        //{


        //    userName = Log_in.Instance._userName;
        //    info = AllToObject.Instance.GetUserInfo();
        //    for (int i = 0; i < info.User.Count; i++)
        //    {
        //        if (info.User[i].playerDataInfo.userName == userName)
        //        {
        //            info.User[i].characterInfo.characterInfos[0].crit += crit;
        //            //序列化后再写回文件（存档）
        //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
        //            return info.User[i].characterInfo.characterInfos[0];
        //        }
        //    }

        //    return null;
        //}
        //public CharacterInfo Lv(int lv)
        //{


        //    userName = Log_in.Instance._userName;
        //    info = AllToObject.Instance.GetUserInfo();
        //    for (int i = 0; i < info.User.Count; i++)
        //    {
        //        if (info.User[i].playerDataInfo.userName == userName)
        //        {
        //            info.User[i].characterInfo.characterInfos[0].lv += lv;
        //            //序列化后再写回文件（存档）
        //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
        //            return info.User[i].characterInfo.characterInfos[0];
        //        }
        //    }

        //    return null;
        //}
        //public CharacterInfo Hp(int hp)
        //{

        //    userName = Log_in.Instance._userName;
        //    info = AllToObject.Instance.GetUserInfo();
        //    for (int i = 0; i < info.User.Count; i++)
        //    {
        //        if (info.User[i].playerDataInfo.userName == userName)
        //        {
        //            info.User[i].characterInfo.characterInfos[0].hp += hp;
        //            //序列化后再写回文件（存档）
        //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
        //            return info.User[i].characterInfo.characterInfos[0];
        //        }
        //    }

        //    return null;
        //}
        //public CharacterInfo MaxHp(int maxHp)
        //{

        //    userName = Log_in.Instance._userName;
        //    info = AllToObject.Instance.GetUserInfo();
        //    for (int i = 0; i < info.User.Count; i++)
        //    {
        //        if (info.User[i].playerDataInfo.userName == userName)
        //        {
        //            info.User[i].characterInfo.characterInfos[0].maxHp += maxHp;
        //            //序列化后再写回文件（存档）
        //            File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
        //            return info.User[i].characterInfo.characterInfos[0];
        //        }
        //    }

        //    return null;
        //}
        public PlayerDataInfo RegisterDay(int registerDay)
        {

            userName = Log_in.Instance._userName;
            info = AllToObject.Instance.GetUserInfo();
            for (int i = 0; i < info.User.Count; i++)
            {
                if (info.User[i].playerDataInfo.userName == userName)
                {
                    info.User[i].playerDataInfo.registerDay += registerDay;
                    //序列化后再写回文件（存档）
                    File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
                    return info.User[i].playerDataInfo;
                }
            }

            return null;

        }

        public PlayerDataInfo Exp(int exp)
        {
            info = AllToObject.Instance.GetUserInfo();
            for (int i = 0; i < info.User.Count; i++)
            {
                if (info.User[i].playerDataInfo.userName == userName)
                {
                    info.User[i].playerDataInfo.exp += exp;
                    //序列化后再写回文件（存档）
                    File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
                    Debug.Log(info.User[i].playerDataInfo);
                    return info.User[i].playerDataInfo;
                }
            }

            return null;

        }

        public PlayerDataInfo Gold(int gold)
        {

            info = AllToObject.Instance.GetUserInfo();
            for (int i = 0; i < info.User.Count; i++)
            {
                if (info.User[i].playerDataInfo.userName == userName)
                {
                    info.User[i].playerDataInfo.gold += gold;
                    //序列化后再写回文件（存档）
                    File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
                    Debug.Log(info.User[i].playerDataInfo);
                    return info.User[i].playerDataInfo;
                }
            }

            return null;
        }
        public PlayerDataInfo Diamond(int diamond)
        {

            info = AllToObject.Instance.GetUserInfo();
            for (int i = 0; i < info.User.Count; i++)
            {
                if (info.User[i].playerDataInfo.userName == userName)
                {
                    info.User[i].playerDataInfo.diamond += diamond;
                    //序列化后再写回文件（存档）
                    File.WriteAllText(AllPaths.Instance.AccountJsonPath, JsonMapper.ToJson(info));
                    Debug.Log(info.User[i].playerDataInfo);
                    return info.User[i].playerDataInfo;
                }
            }

            return null;

        }*/
}