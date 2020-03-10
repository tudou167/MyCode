using System.Collections.Generic;

public class SkillList
{
    public List<SkillData> skillList;

    public SkillList() { }


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
    public SkillList(List<SkillData> skillList)
    {
        this.skillList = new List<SkillData>();
        for (int i = 0; i < skillList.Count; i++)
        {
            this.skillList.Add(new SkillData(
                skillList[i].ID,
                skillList[i].Name,
                skillList[i].Icon,
                skillList[i].CD,
                skillList[i].BaseExp,
                skillList[i].NextSkillID,
                skillList[i].Detail,
                skillList[i].Type,
                skillList[i].Open,
                null,
                skillList[i].DelegateName,
                skillList[i].DelegateParameter,
                skillList[i].Exp,
                skillList[i].LV
                ));
        }
    }
}
