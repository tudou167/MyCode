using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//英雄拥有的技能
public class HeroSkillLoader : MonoBehaviour
{
    void Awake()
    {
        if (!Directory.Exists(AllPath.Instance.userDirectoryPath)) return;
        int userID = UserManager.Instance.GetCurUser().ID;
        int roleID = HeroManager.Instance.GetCurHeroData().RoleID;
        string roleSkillPath = AllPath.Instance.accountDirectoryPath + userID + AllPath.Instance.accountSkillDirectoryPath + userID + "_" + roleID;
        SkillList data = Tool.Instance.JsonToObj<SkillList>(roleSkillPath);

        Dictionary<int, SkillData> dic = new Dictionary<int, SkillData>();
        for (int i = 0; i < data.skillList.Count; i++)
        {
            dic.Add(data.skillList[i].ID, data.skillList[i]);
            data.skillList[i].DelegateFunc = SkillInfoManager.Instance.GetSkill(data.skillList[i].ID).DelegateFunc;
        }

        HeroSkillManager.Instance.Bind(dic);
    }
}
