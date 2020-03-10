
using System;
using UnityEngine;
using System.Collections;
public class BaseMonoSingleTon<T> : MonoBehaviour where T : BaseMonoSingleTon<T>, new()
{
    private static T instance;//单例
    private static object iLock = new object();//对象锁
    public static bool IsAlive = true;//避免销毁时创建
    public static T Instance
    {
        get
        {
            string name = typeof(T).Name;
            //查找脚本
            var t = FindObjectOfType<T>();
            //脚本不存在 创建物体
            if (t == null)
            {
                lock (iLock)
                {
                    if (t == null)
                    {
                        if (IsAlive)
                        {
                            instance = new GameObject(name).AddComponent<T>();
                        }
                    }
                }
            }
            else
            {
                instance = t;
            }
            return instance;
        }
    }
    private void OnDestroy()
    {
        IsAlive = false;
    }
    private void OnApplicationQuit()
    {
        IsAlive = false;
    }
}