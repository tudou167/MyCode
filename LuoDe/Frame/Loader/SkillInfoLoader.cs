using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfoLoader : MonoBehaviour
{
    void Awake()
    {
        //需要比SkillEffectLoader 的 SkillEffectManager.Instance.AddSkillEffect（）慢
        List<SkillData> data = JsonMapper.ToObject<List<SkillData>>(Resources.Load<TextAsset>(AllPath.Instance.jsonPath + "Effect").text);
        Dictionary<int, SkillData> dic = new Dictionary<int, SkillData>();
        
        for (int i = 0; i < data.Count; i++)
        {
            data[i].DelegateFunc = SkillEffectManager.Instance.GetEffect(data[i].DelegateName);
            dic.Add(data[i].ID, data[i]);
        }
        SkillInfoManager.Instance.Bind(dic);
    }
}
