using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    private Dictionary<string, List<GameObject>> gameObjPool = new Dictionary<string, List<GameObject>>();

    private GameObject PoolObj;

    public GameObject GetObj(string path, Vector3 pos, Quaternion rotation, Transform parent)
    {
        GameObject result = null;
        string[] strArr = path.Split('/');
        string name = strArr[strArr.Length - 1];
        if (gameObjPool.ContainsKey(name))
        {
            if (gameObjPool[name].Count > 0)
            {
                result = gameObjPool[name][0];
                result.transform.SetParent(parent, false);
                result.transform.name = name;
                result.transform.position = pos;
                result.transform.rotation = rotation;
                result.transform.localScale = Vector3.one;
                result.SetActive(true);
                gameObjPool[name].RemoveAt(0);

                return result;
            }
        }
        result = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(path), pos, rotation, parent);
        result.transform.SetParent(parent, false);
        result.transform.name = name;
        result.transform.localScale = Vector3.one;
        return result;
    }
    public void RecyleObj(GameObject go, string poolName)
    {
        if (PoolObj == null)
        {
            PoolObj = GameObject.Find("/GamePool");
        }
        go.SetActive(false);
        if (!gameObjPool.ContainsKey(go.name))
        {
            gameObjPool.Add(go.name, new List<GameObject>() { go });
        }
        else
        {
            gameObjPool[go.name].Add(go);
        }
        go.transform.SetParent(PoolObj.transform.Find(poolName));
    }

    public void ReSet()
    {
        gameObjPool = new Dictionary<string, List<GameObject>>();
    }
}
