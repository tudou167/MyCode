using System.Collections.Generic;
public class RoleData
{
    //默认配表的ID
    public int ID;
    //每个用户的不同角色ID
    public int RoleID;
    public string Name;
    public string Prefab;
    public int HP;
    public int BaseExp;
    public int LV;
    public int Exp;
    public int ATK;
    public int DEF;
    public int Speed;
    public int ATKSpeed;
    //硬值
    public int Repel;
    //获取用
    public int WeaponId;
    //获取用
    public int ArmorId;
    public Equip Weapon;
    public Equip Armor;
    public int Crit;
    //配表用 1003#2002 #分割
    public string StrSkillList;
    //拥有的技能列表
    public List<SkillData> SkillList;

    public RoleData() { }
    public RoleData(int id,string name,string prefab,int hp,int baseExp,int lv,int exp,int atk,int def,int speed,int atkSpeed,int crit,int repel,int weaponId,int armorId, Equip weapon, Equip armor,string strSkillList,List<SkillData> skillList)
    {
        ID = id;
        Name = name;
        Prefab = prefab;
        HP = hp;
        BaseExp = baseExp;
        LV = lv;
        Exp = exp;
        ATK = atk;
        DEF = def;
        Speed = speed;
        ATKSpeed = atkSpeed;
        Crit = crit;
        Repel = repel;
        WeaponId = weaponId;
        ArmorId = armorId;
        Weapon = weapon;
        Armor = armor;
        StrSkillList = strSkillList;
        SkillList = skillList;
    }
}
