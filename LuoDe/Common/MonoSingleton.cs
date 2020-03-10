using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static string MonoSingletonName = "MonoSingletonRoot";
    private static GameObject MonoSingletonRoot;
    private static T _instance;
    //private static readonly object _lock = new Object();

    public static T Instance
    {
        get
        {
            //如果是第一次调用单例类型就查找所有单例类的总结点
            if (MonoSingletonRoot == null)
            {
                MonoSingletonRoot = GameObject.Find(MonoSingletonName);
                //如果没有找到则创建一个所有继承MonoBehaviour单例类的节点
                if (MonoSingletonRoot == null)
                {
                    MonoSingletonRoot = new GameObject();
                    MonoSingletonRoot.name = MonoSingletonName;
                    DontDestroyOnLoad(MonoSingletonRoot);
                }
            }
            _instance = MonoSingletonRoot.AddComponent<T>();
            return _instance;
        }

    }

    public virtual void Awake()
    {
        _instance = this as T;
    }

}
