using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingletion<T> : MonoBehaviour where T : MonoBehaviour
{

    private static string MonoSingletionName = "MonoSingletionRoot";
    private static GameObject MonoSingletionRoot;
    private static T instance;

    public static T Instance
    {
        get
        {
            if (MonoSingletionRoot == null)//如果是第一次调用单例类型就查找所有单例类的总结点
            {
                MonoSingletionRoot = GameObject.Find(MonoSingletionName);
                if (MonoSingletionRoot == null)//如果没有找到则创建一个所有继承MonoBehaviour单例类的节点
                {
                    MonoSingletionRoot = new GameObject();
                    MonoSingletionRoot.name = MonoSingletionName;
                    DontDestroyOnLoad(MonoSingletionRoot);//防止被销毁
                }
            }
            //instance = MonoSingletionRoot.AddComponent<T>();
            return instance;
        }
    }
    public virtual void Awake()
    {
        instance = this as T;
        BaseManager<MonoSingletion<T>>.Instance.Register(this);
    }

    public static void SetInstance(T value)
    {
        instance = value;
    }

}