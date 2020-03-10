using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    private Dictionary<int, NPCData> npcDic;


    public NPCData GetNPC(int id)
    {
        if (npcDic != null && npcDic.ContainsKey(id))
        {
            return npcDic[id];
        }
        return null;
    }
    public List<NPCData> GetNPCList()
    {
        if (npcDic == null) return null;

        List<NPCData> npcList = new List<NPCData>() { };
        foreach (NPCData item in npcDic.Values)
        {
            npcList.Add(item);
        }
        return npcList;
    }

    public void Bind(Dictionary<int, NPCData> npcDic)
    {
        this.npcDic = npcDic;
    }
}
