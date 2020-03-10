using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.U2D;
using UnityEngine.Events;
using UnityEngine.UI;

public class AllTools : CSharpSingletion<AllTools>
{
    public Transform FindChild(Transform Father, string name)
    {
        if (Father.name == name)
        {
            return Father;
        }
        if (Father.childCount < 0)
        {
            return null;
        }
        Transform target;
        for (int i = 0; i < Father.childCount; i++)
        {
            Transform temp = Father.GetChild(i);
            target = FindChild(temp, name);
            if (target != null)
            {
                return target;
            }

        }
        return null;
    }
    public Sprite LoadItemIcon(string icon)
    {
        string[] paths = icon.Split('#');
        return Resources.Load<SpriteAtlas>(paths[0]).GetSprite(paths[1]);
    }
    public GameObject Load(string path)
    {
        //加载登陆预制体
        GameObject prefab = Resources.Load<GameObject>(path);
        //实例化
        GameObject page = Object.Instantiate<GameObject>(prefab);
        //避免游戏对象产生Clone
        page.name = prefab.name;
        //设置Canvas为父物体
        page.transform.SetParent(GameObject.Find("/Canvas").transform);
        //position初始化
        page.transform.localPosition = Vector3.zero;
        page.transform.localRotation = Quaternion.identity;
        page.transform.localScale = Vector3.one;
        //设置为最后一个物体
        page.transform.SetAsLastSibling();

        //四锚点的四个外边距归零
        RectTransform rect = page.transform as RectTransform;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        return page;
    }

    public GameObject LoadCell(string path, Transform parent)
    {
        //加载登陆预制体
        GameObject prefab = Resources.Load<GameObject>(path);
        //实例化
        GameObject page = Object.Instantiate<GameObject>(prefab);
        //避免游戏对象产生Clone
        page.name = prefab.name;
        //设置Canvas为父物体
        page.transform.SetParent(parent);
        //position初始化
        page.transform.localPosition = Vector3.zero;
        page.transform.localRotation = Quaternion.identity;
        page.transform.localScale = Vector3.one;
        //设置为最后一个物体
        page.transform.SetAsLastSibling();

        return page;
    }

    public void Alert(string message, UnityAction callback = null)   //回调方法
    {

        GameObject page =Load(AllPaths.Instance.Reg_logPrefabs+"Alert");

        UIAlert script = page.GetComponent<UIAlert>();
        script.Message.text = message;
        script.Callback = callback;

    }
    
    public void DeleteAllChild(Transform trf)
    {
        //拿到子元素数量
        int count = trf.childCount;
        //获得所有子物体
        for (int i = count - 1; i > -1; i--)
        {
            //获得子元素
            //Destroy不会在本行代码就会删除GameObject，在本帧流程的最后，删除GameObject
            //DestroyImmediate会立即删除游戏对象，尽量不使用
            Object.Destroy(trf.GetChild(i).gameObject);
        }
    }

    public void HiddenAllChild(Transform trf)
    {
        //拿到子元素数量
        int count = trf.childCount;
        //获得所有子物体
        for (int i = count - 1; i > -1; i--)
        {
            trf.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void ShowAllChild(Transform trf)
    {
        //拿到子元素数量
        int count = trf.childCount;
        //获得所有子物体
        for (int i = count - 1; i > -1; i--)
        {
            trf.GetChild(i).gameObject.SetActive(true);
        }
    }

    /**
（坑：不要使用IO的FileStream和Application.dataPath的绝对路径 ，
因为打包后的绝对路径会找不到文件，建议使用Resources.Load）
     */
    // //读取json数据,根据id查找(表格第一行),返回id和类对象的字典
    // public static Dictionary<string, T> ReadJson<T>(string fileName)
    // {
    //     TextAsset ta = Resources.Load(fileName) as TextAsset;
    //     if (ta.text == null)
    //     {
    //         Debug.Log("根据路径未找到对应表格数据");
    //     }
    //      Debug.Log(JsonMapper.ToObject(ta.text).ToString());
    //     Dictionary<string, T> d = JsonMapper.ToObject<Dictionary<string, T>>(ta.text);
    //     return d;
    // }
    // //写入json数据,传入类.
    // public static void WriteJson(string path, object jsonData)
    // {
    //     JsonMapper.ToJson(jsonData);
    // }
}
