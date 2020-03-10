using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebug
{
    public static bool ShowLog = true;
    public static void Log(object log)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (ShowLog)
        {
            Log(log.ToString());
        }
#endif
    }
    public static void Log(string log,string forword=null)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (ShowLog)
        {
            Debug.Log("<color=#3399ffFF>[" + DateTime.Now.ToString("HH:mm:ss:ffff") + "]</color>  " +"<color=#ff8000ff>[" + forword + "]</color> "+ log);
        }
#endif 
    }

    public static void LogObjs(params object[] objs)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (ShowLog)
        {
            string str = "";
            foreach (var s in objs)
            {
                str += s.ToString() + "\t";
            }
            if (str.Length > 0)
            {
                str.Remove(str.Length - 1);
            }
            Log(str);
        }
#endif
    }

    public static void LogWarning(object log)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (ShowLog)
        {
            LogWarning(log.ToString());
        }
#endif
    }

    public static void LogWarning(string log,string forword=null)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (ShowLog)
        {
            Debug.LogWarning("<color=#FFFF00FF>[" + DateTime.Now.ToString("HH:mm:ss:ffff") + "]</color>  " +"<color=#ff8000ff>[" + forword + "]</color> "+ log);
        }
#endif
    }

    public static void LogError(object log)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (ShowLog)
        {
            LogError(log);
        }
#endif
    }

    public static void LogError(string log,string forword=null)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (ShowLog)
        {
            Debug.LogError("<color=#ff2828FF>[" + DateTime.Now.ToString("HH:mm:ss:ffff") + "]</color>  " +"<color=#ff8000ff>[" + forword + "]</color> "+ log + "\n" + GetStackTrace());
        }
#endif
    }

    private static string GetStackTrace()
    {
        System.Diagnostics.StackTrace ss = new System.Diagnostics.StackTrace(true);
        return ss.ToString();
    }
}