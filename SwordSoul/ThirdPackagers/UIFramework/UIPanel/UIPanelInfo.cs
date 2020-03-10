using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UIPanelInfo : ISerializationCallbackReceiver
{
    [System.NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString;
    //{
    // get{
    // 	return panelType.ToString();
    // }
    // set{
    // 	UIPanelType type=(UIPanelType)System.Enum.Parse(typeof(UIPanelType),value);
    // 	panelType=type;
    // }
    //}
    public string path;

    public void OnBeforeSerialize()
    {

    }
    //反序列化 从文本信息到对象
    public void OnAfterDeserialize()
    {
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        panelType = type;
    }
}
