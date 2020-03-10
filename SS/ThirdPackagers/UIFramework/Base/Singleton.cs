
using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// 不继承MonoBehaviour的单例类（泛型），单例类只要继承此类就行
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : new()
{
    private static T instance;

    /// <summary>
    /// 它的继承类不需在写得到单例的方法
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// 在继承类中重写此方法可以将继承类的实例清空
    /// </summary>
    public virtual void Dispose()
    {
        instance = default(T);
    }
}