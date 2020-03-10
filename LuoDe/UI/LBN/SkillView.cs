using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    SkillList curList;
    SkillList allSkill;
    GameObject curSelect;
    int skillPoint;
    void Start()
    {
        GameObject skl = Tool.Instance.InstantiateObj("Prefabs/Loader/HeroSkillLoader", transform.root);
        Destroy(skl);
        RoleData heroData = HeroManager.Instance.GetCurHeroData();
        skillPoint = Tool.Instance.LvCheck(heroData.BaseExp, heroData.Exp, heroData.LV)[3] + 3;
        curList = HeroSkillManager.Instance.GetSkillList();
        allSkill = SkillInfoManager.Instance.GetSkillList();
        for (int i = 0; i < allSkill.skillList.Count - 2; i++)
        {
            //Debug.Log(allSkill.skillList[i].ID);
            SkillBase(allSkill.skillList[i].ID);
        }
        for (int i = 0; i < curList.skillList.Count; i++)
        {
            SkillLV(curList.skillList[i], curList.skillList[i].ID);
        }
        transform.Find("window/SkillPoints").GetComponent<Text>().text += "<color=red>" + skillPoint + "</color>";
    }
    void ShowDetail()
    {
        curSelect = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curSelect.transform.Find("ID").GetComponent<Text>().text, out id);
        SkillData skilldata = SkillInfoManager.Instance.GetSkill(id);
        Transform Detail = transform.Find("window/SkillDescribe");
        if (Detail.gameObject.activeSelf == false)
        {
            Detail.gameObject.SetActive(true);
        }
        Detail.Find("describe").GetComponent<Text>().text = skilldata.Name + ":\n" + skilldata.Detail;
    }
    void CanUp()
    {
        curSelect = EventSystem.current.currentSelectedGameObject;
        transform.Find("window/SkillDescribe/yes").GetComponent<Button>().onClick.RemoveAllListeners();
        transform.Find("window/SkillDescribe/yes").GetComponent<Button>().onClick.AddListener(LevelUp);
    }
    void LevelUp()
    {
        if (skillPoint == 0) return;
        int id;
        int.TryParse(curSelect.transform.Find("ID").GetComponent<Text>().text, out id);
        for (int i = 0; i < curList.skillList.Count; i++)
        {
            if (curList.skillList[i].ID == id)
            {
                if (curList.skillList[i].LV < 3)
                {
                    if (curList.skillList[i].LV == 0)
                    {
                        HeroManager.Instance.GetCurHeroData().StrSkillList += "#" + curList.skillList[i].ID;
                        HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, HeroManager.Instance.GetCurHeroData());
                    }

                    curList.skillList[i].LV += 1;
                    skillPoint -= 1;
                }
                if (curList.skillList[i].LV == 3)
                {
                    SkillData nextSkill = SkillInfoManager.Instance.GetSkill(curList.skillList[i].NextSkillID);
                    if (nextSkill != null)
                    {
                        nextSkill.Open = 1;
                        if (!curList.skillList.Contains(nextSkill))
                        {
                            curList.skillList.Add(nextSkill);
                        }
                    }
                }
                switch (curList.skillList[i].LV)
                {
                    case 1:
                        curSelect.transform.Find("star1/star").gameObject.SetActive(true);
                        break;
                    case 2:
                        curSelect.transform.Find("star1/star").gameObject.SetActive(true);
                        curSelect.transform.Find("star2/star").gameObject.SetActive(true);
                        break;
                    case 3:
                        curSelect.transform.Find("star1/star").gameObject.SetActive(true);
                        curSelect.transform.Find("star2/star").gameObject.SetActive(true);
                        curSelect.transform.Find("star3/star").gameObject.SetActive(true);
                        break;
                }
                break;
            }
        }

        HeroSkillManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curList);
        transform.Find("window/SkillPoints").GetComponent<Text>().text = "持有技能点：" + "<color=red>" + skillPoint + "</color>";



    }
    void SkillLV(SkillData skillData, int ID)
    {
        string first = ("" + ID).Substring(0, 1);
        string last = ("" + ID).Substring(("" + ID).Length - 1, 1);
        string type = "";
        string skill = "";
        switch (first)
        {
            case "1":
                type = "Zweihander";
                break;
            case "2":
                type = "SwordShield";
                break;
            case "3":
                type = "Wand";
                break;
        }
        switch (last)
        {
            case "1":
                skill = "skill1";
                break;
            case "2":
                skill = "skill2";
                break;
            case "3":
                skill = "skill3";
                break;
        }

        switch (skillData.LV)
        {
            case 1:
                transform.Find("window/" + type + "/" + skill + "/star1/star").gameObject.SetActive(true);
                skillPoint -= 1;
                break;
            case 2:
                transform.Find("window/" + type + "/" + skill + "/star1/star").gameObject.SetActive(true);
                transform.Find("window/" + type + "/" + skill + "/star2/star").gameObject.SetActive(true);
                skillPoint -= 2;
                break;
            case 3:
                transform.Find("window/" + type + "/" + skill + "/star1/star").gameObject.SetActive(true);
                transform.Find("window/" + type + "/" + skill + "/star2/star").gameObject.SetActive(true);
                transform.Find("window/" + type + "/" + skill + "/star3/star").gameObject.SetActive(true);
                skillPoint -= 3;
                break;
        }
        if (skillData.Open == 1)
        {
            transform.Find("window/" + type + "/" + skill).GetComponent<Button>().onClick.AddListener(CanUp);
        }
    }
    void SkillBase(int ID)
    {
        string first = ("" + ID).Substring(0, 1);
        string last = ("" + ID).Substring(("" + ID).Length - 1, 1);
        string type = "";
        string skill = "";
        switch (first)
        {
            case "1":
                type = "Zweihander";
                break;
            case "2":
                type = "SwordShield";
                break;
            case "3":
                type = "Wand";
                break;
        }
        switch (last)
        {
            case "1":
                skill = "skill1";
                break;
            case "2":
                skill = "skill2";
                break;
            case "3":
                skill = "skill3";
                break;
        }
        transform.Find("window/" + type + "/" + skill + "/ID").GetComponent<Text>().text = "" + ID;
        transform.Find("window/" + type + "/" + skill).GetComponent<Button>().onClick.AddListener(ShowDetail);
    }
}
