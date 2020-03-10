using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* 请实现一个AB管理器
		单例模式
		依赖问题（能够处理指定AB包的依赖关系）
		防止AB包重复加载（字典存储AB包）
		AB包的存储路径可以指定为统一的一个路径
		提供AB包名称就可以加载AB包
		提供AB包名称和提供资源名称，就可以从AB包内加载资源
*/
public class AssetBundlesManager
{
    //单例模式
    private static AssetBundlesManager _instance;
    public static AssetBundlesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AssetBundlesManager();
            }
            return _instance;
        }
    }
    AssetBundle main;
    AssetBundleManifest assetBundleManifest;
    public AssetBundlesManager()
    {
        Init();
    }
    public void Init()
    {
        if (!dic.ContainsKey(mainPath))
        {
            dic.Add(mainPath, AssetBundle.LoadFromFile(assetBundlePath + mainPath));
        }
        main = dic[mainPath];
        assetBundleManifest = main.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    }
    //防止AB包重复加载（字典存储AB包）
    public Dictionary<string, AssetBundle> dic = new Dictionary<string, AssetBundle>();
    //AB包的存储路径可以指定为统一的一个路径
    public string assetBundlePath = Application.dataPath + "/../AssetBundles/";
    public string mainPath = "AssetBundles";
    //依赖问题（能够处理指定AB包的依赖关系）
    public void GetDependencies(string targetName)
    {
        if (!File.Exists(assetBundlePath + targetName) || targetName == "" || !File.Exists(assetBundlePath + mainPath))
        {
            Debug.LogError("AB包路径错误");
            return;
        }
        string[] name = assetBundleManifest.GetAllDependencies(targetName);
        for (int i = 0; i < name.Length; i++)
        {
            if (!dic.ContainsKey(name[i]))
            {
                //AssetBundle.LoadFromFile(assetBundlePath + name[i]);
                dic.Add(name[i], AssetBundle.LoadFromFile(assetBundlePath + name[i]));
            }
        }
    }
    //提供AB包名称就可以加载AB包
    public AssetBundle GetAssetBundle(string targetName)
    {
        if (!dic.ContainsKey(targetName))
        {
            //AssetBundle.LoadFromFile(assetBundlePath + name[i]);
            dic.Add(targetName, AssetBundle.LoadFromFile(assetBundlePath + targetName));
        }
        GetDependencies(targetName);
        return dic[targetName];
    }
    //提供AB包名称和提供资源名称，就可以从AB包内加载资源
    public T GetAssetBundleResounces<T>(string targetName, string assetName) where T : class, new()
    {
        AssetBundle temp = GetAssetBundle(targetName);
        if (temp.LoadAsset(assetName) is T)
        {
            return temp.LoadAsset(assetName) as T;
        }
        else
        {
            Debug.Log("加载资源的类型不一致");
        }
        return default(T);
    }
}
