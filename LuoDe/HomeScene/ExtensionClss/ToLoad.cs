using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToLad
{
    /// <summary>
    /// 加载Rsources资源(路径:path，目的地的标签:target)
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="target">目的地</param>
    public static Transform toLad(this string path, string target)
    {
        GameObject go = Resources.Load<GameObject>(path);
        GameObject ta = GameObject.Find(target);
        GameObject pa = Object.Instantiate(go);
        pa.transform.SetParent(ta.transform);
        pa.name = go.name;
        RectTransform rpa = pa.transform as RectTransform;
        rpa.transform.localPosition = Vector3.zero;
        rpa.transform.localScale = Vector3.one;
        rpa.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rpa.offsetMax = Vector3.zero;
        rpa.offsetMin = Vector3.zero;
        return rpa;
    }

}
