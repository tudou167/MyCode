using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillEffectManager : Singleton<SkillEffectManager>
{
    //用于添加技能的委托
    private Dictionary<string, UnityAction<Transform,string>> skillDic;

    public SkillEffectManager()
    {
        skillDic = new Dictionary<string, UnityAction<Transform, string>>();
    }

    public UnityAction<Transform, string> GetEffect(string skillName)
    {
        if (skillDic != null && skillDic.ContainsKey(skillName))
        {
            return skillDic[skillName];
        }
        return null;
    }

    public void AddSkillEffect(string skillEffectName, UnityAction<Transform,string> action)
    {
        if (skillDic.ContainsKey(skillEffectName))
        {
            return;
        }
        skillDic.Add(skillEffectName,action);
    }
}
