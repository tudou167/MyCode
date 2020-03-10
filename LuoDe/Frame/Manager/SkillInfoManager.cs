using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfoManager : Singleton<SkillInfoManager>
{
    //用于获取技能整体
    private Dictionary<int, SkillData> skillDic;

    public SkillData GetSkill(int id)
    {
        if (skillDic != null && skillDic.ContainsKey(id))
        {
            return skillDic[id];
        }
        return null;
    }

    public SkillList GetSkillList()
    {
        if (skillDic == null) return null;

        SkillList skillList = new SkillList(new List<SkillData>() { });
        foreach (SkillData item in skillDic.Values)
        {
            skillList.skillList.Add(item);
        }
        return skillList;
    }


    public List<SkillData> GetSkillList(string strSkillList)
    {

        if (skillDic == null)
        {
            return null;
        }
        string[] strArr = strSkillList.Split('#');
        List<SkillData> skillList = new List<SkillData>();
        for (int i = 0; i < strArr.Length; i++)
        {
            if (strArr[i] == "")
            {
                break;
            }
            if (skillDic[int.Parse(strArr[i])] == null)
            {
                continue;
            }
            skillList.Add(skillDic[int.Parse(strArr[i])]);
        }
        return skillList;
    }

    public void Bind(Dictionary<int, SkillData> skillDic)
    {
        this.skillDic = skillDic;
    }
}
