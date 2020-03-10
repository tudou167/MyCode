using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataInfo  {
    public int id;
    // -账号
    public string userName;
    // -密码
    public string password;
    // -上次登录时间
    public string lastLoadTime;
    // -签到天数
    public int registerDay;
    // -经验
    public int exp;
    // -金币
    public int gold;
    // -钻石
    public int diamond;
}
