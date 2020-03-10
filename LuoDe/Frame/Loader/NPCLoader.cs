using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class NPCLoader : MonoBehaviour
{
    void Awake()
    {
        List<NPCData> data = JsonMapper.ToObject<List<NPCData>>(Resources.Load<TextAsset>(AllPath.Instance.jsonPath + "NPC").text);
        Dictionary<int, NPCData> dic = new Dictionary<int, NPCData>();
        for (int i = 0; i < data.Count; i++)
        {
            dic.Add(data[i].ID,new NPCData(
                    data[i].ID,
                    data[i].Name,
                    data[i].Icon,
                    data[i].Prefab,
                    data[i].ItemList
                ));
        }
        NPCManager.Instance.Bind(dic);
    }
}
