using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterInfo
{
    public int id;// -id
    public string name;// -名字
    public int atk;// -攻击力
    public int def;// -防御力
    public int crit;// -暴击率
    public int lv;//等级
    public int hp;// -血量
    public int maxHp;// -最大血量

}
[System.Serializable]
public class CharacterInfoList
{
    public List<CharacterInfo> characterInfos;
}