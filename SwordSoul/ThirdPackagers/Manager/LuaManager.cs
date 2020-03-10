using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
public class LuaManager
{
    private static LuaManager _instance;
    public static LuaManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LuaManager();
            }
            return _instance;
        }
    }
    LuaEnv _Env = null;
    private LuaManager()
    {
        _Env = new LuaEnv();
        _Env.AddLoader(Myloader);
    }
    private byte[] Myloader(ref string filepath)
    {
        if (!File.Exists(AssetBundlesManager.Instance.assetBundlePath + "lua"))
        {
            return System.Text.Encoding.UTF8.GetBytes("print(\"" + filepath + "不存在\")");
        }
        else
        {
            TextAsset str = AssetBundlesManager.Instance.GetAssetBundleResounces<TextAsset>("lua", filepath + ".lua");
            return str.bytes;//System.Text.Encoding.UTF8.GetBytes();
        }

    }
    public object[] DoString(string luaName)
    {
        return _Env.DoString(luaName);
    }
    public void Dispose()
    {
        if (_Env == null)
        {
            Debug.Log("解析器不存在");
            return;
        }
        _Env.Dispose();
    }
    public LuaTable GetLuaTable(string tableName)
    {
        return _Env.Global.Get<LuaTable>(tableName);
    }
}
