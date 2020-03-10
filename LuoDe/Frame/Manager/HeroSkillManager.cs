using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class HeroSkillManager:Singleton<HeroSkillManager>
{
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

    public void Bind(Dictionary<int, SkillData> skillDic)
    {
        this.skillDic = skillDic;
    }
    public void InitData(int userID, int roleID , RoleData data)
    {
        string skillPath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountSkillDirectoryPath;
        SkillList skillList = new SkillList(SkillInfoManager.Instance.GetSkillList(data.StrSkillList));
        File.WriteAllText(skillPath + userID + "_" + roleID, Tool.Instance.ObjToJson(skillList));
    }
    public void SaveData(int userID, int roleID, SkillList data)
    {
        List<UnityAction<Transform, string>> tmpList = new List<UnityAction<Transform, string>>();
        for (int i = 0; i < data.skillList.Count; i++)
        {
            tmpList.Add(data.skillList[i].DelegateFunc);
            data.skillList[i].DelegateFunc = null;
        }

        string skillPath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountSkillDirectoryPath;
        File.WriteAllText(skillPath + userID + "_" + roleID, Tool.Instance.ObjToJson(data));
        
        for (int i = 0; i < data.skillList.Count; i++)
        {
            data.skillList[i].DelegateFunc = tmpList[i];
        }

        Dictionary<int, SkillData> dic = new Dictionary<int, SkillData>();
        for (int i = 0; i < data.skillList.Count; i++)
        {
            dic.Add(data.skillList[i].ID, data.skillList[i]);
            data.skillList[i].DelegateFunc = SkillInfoManager.Instance.GetSkill(data.skillList[i].ID).DelegateFunc;
        }
        Bind(dic);
    }
}
