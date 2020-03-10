using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Singleton<Tool>
{
    public GameObject InstantiateObj(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject go = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        go.name = prefab.name;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.SetAsLastSibling();

        return go;
    }
    public GameObject InstantiateObjOffset(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject go = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        go.name = prefab.name;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.SetAsLastSibling();

        RectTransform rect = go.transform as RectTransform;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        return go;
    }

    /// <summary>
    /// 返回->下级需要的经验 当前等级的经验 当前等级还需要的经验 当前的等级
    /// </summary>
    /// <param name="nextExp"></param>
    /// <param name="remaining"></param>
    /// <param name="curLv"></param>
    /// <returns></returns>
    public int[] LvCheck(int nextExp, int remaining, int curLv = 1)
    {
        int needExp = nextExp * curLv;
        if (remaining < needExp)
        {
            //          下级需要的经验 当前等级的经验 当前等级还需要的经验 当前的等级
            return new int[] { nextExp, remaining, remaining - needExp, curLv };
        }
        curLv++;
        return LvCheck(needExp, remaining - needExp, curLv);
    }

    public List<int> GetShortcut(string str)
    {
        string[] tempArr = str.Split('|');
        string[] tempArr2 = tempArr[0].Split('#');
        string[] tempArr3 = tempArr[1].Split('#');

        List<int> dataList = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            if (i >= 3)
            {
                dataList.Add(int.Parse(tempArr3[i - 3]));
            }
            else
            {
                dataList.Add(int.Parse(tempArr2[i]));
            }
        }
        return dataList;
    }

    public void DeleteChild(Transform tf)
    {
        for (int i = 0; i < tf.childCount; i++)
        {
            Object.Destroy(tf.GetChild(i).gameObject);
        }
    }
    public T JsonToObj<T>(string path) where T : class, new()
    {
        return LitJson.JsonMapper.ToObject<T>(System.IO.File.ReadAllText(path));
    }

    public string ObjToJson(object data)
    {
        return LitJson.JsonMapper.ToJson(data);
    }
}
