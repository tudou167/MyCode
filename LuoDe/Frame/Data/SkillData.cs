

using UnityEngine;
using UnityEngine.Events;

public class SkillData
{
    public int ID;
    public string Name;
    public string Icon;
    public int CD;
    public int BaseExp;
    public int NextSkillID;
    public string Detail;
    public SkillType Type;

    //当前的冷却时间
    //public float curCD;
    //是否激活（习得）
    public int Open;
    //技能效果委托
    public UnityAction<UnityEngine.Transform, string> DelegateFunc;
    //技能效果委托名称
    public string DelegateName;


    //技能效果执行参数
    public string DelegateParameter;
    //当前经验
    public int Exp;
    //当前等级
    public int LV;

    public SkillData() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="icon"></param>
    /// <param name="cd"></param>
    /// <param name="baseExp"></param>
    /// <param name="nextSkillID"></param>
    /// <param name="detail"></param>
    /// <param name="type"></param>
    /// <param name="open"></param>
    /// <param name="delegateFunc"></param>
    /// <param name="delegateName"></param>
    /// <param name="delegateParameter"></param>
    /// <param name="exp"></param>
    /// <param name="lv"></param>
    public SkillData(int id, string name, string icon, int cd, int baseExp, int nextSkillID, string detail, SkillType type, int open, UnityAction<UnityEngine.Transform, string> delegateFunc, string delegateName, string delegateParameter, int exp, int lv)
    {
        ID = id;
        Name = name;
        Icon = icon;
        CD = cd;
        BaseExp = baseExp;
        NextSkillID = nextSkillID;
        Detail = detail;
        Type = SkillType.Initiative;
        Open = open;
        DelegateFunc = delegateFunc;
        DelegateName = delegateName;
        DelegateParameter = delegateParameter;
        Exp = exp;
        LV = lv;
    }

}