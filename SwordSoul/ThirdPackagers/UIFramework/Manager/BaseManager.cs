using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T : class, new()
{
    private static BaseManager<T> _instance;
    public static BaseManager<T> Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BaseManager<T>();
            }
            return _instance;
        }
    }
    private Dictionary<Type, T> dic = new Dictionary<Type, T>();
    public void Register(T baseCon)
    {
        if (baseCon == null) return;
        Type type = baseCon.GetType();
        if (dic.ContainsKey(type))
        {
            Debug.Log(type.ToString() + "已存在");
            return;
        }
        dic.Add(type, baseCon);
    }
    public T Get(Type type)
    {
        if (dic.ContainsKey(type))
        {
            return dic[type];
        }
        return null;
    }
}
