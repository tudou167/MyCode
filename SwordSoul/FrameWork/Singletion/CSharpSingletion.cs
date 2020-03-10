using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CSharpSingletion<T> where T : class, new()
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    protected CSharpSingletion()
    {
        if (instance != null)
        {
            throw new System.Exception("This " + (typeof(T)).ToString() + " Instance is not null!");
        }
        Init();
    }
    

    protected virtual void Init()
    {
        
    }
}