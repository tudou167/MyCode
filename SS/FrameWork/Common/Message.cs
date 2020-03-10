using System.Collections.Generic;
/// <summary>
/// 自定义事件方法委托
/// </summary>
/// <param name="e"></param>
public delegate void EventDelegateCallBack(MessageEventInfo e);
public class Message:CSharpSingletion<Message>
{
    /// <summary>
    /// 事件管理类总事件存储类
    /// </summary>
    private Dictionary<string, EventDelegateCallBack> _eventDict = new Dictionary<string, EventDelegateCallBack>();
    

    /// <summary>
    /// 添加特定事件方法入字典中
    /// </summary>
    /// <param name="type"></param>
    /// <param name="call"></param>
    public void AddEvent(string type, EventDelegateCallBack call)
    {
        if (!_eventDict.ContainsKey(type))
        {
            _eventDict[type] = call;
        }
        else
        {
            _eventDict[type] += call;
        }
    }

    /// <summary>
    /// 从字典移除特定事件方法
    /// </summary>
    /// <param name="type"></param>
    /// <param name="call"></param>
    public void RemoveEvent(string type, EventDelegateCallBack call)
    {
        if (_eventDict.ContainsKey(type))
        {
            _eventDict[type] -= call;
            if (_eventDict[type] == null)
            {
                _eventDict.Remove(type);
            }
        }
    }

    /// <summary>
    /// 根据类型派发事件
    /// </summary>
    /// <param name="e"></param>
    public void DispathEvent(MessageEventInfo e)
    {
        if (_eventDict.ContainsKey(e.type) && _eventDict[e.type] != null)
        {
            _eventDict[e.type](e);
        }
    }
    /// <summary>
    /// 移除字典中所有的事件
    /// </summary>
    public void RemoveAllEvent()
    {
        _eventDict.Clear();
    }
}

/// <summary>
/// 事件参数结构体
/// </summary>
public struct MessageEventInfo
{
    public string type;
    public object vaule;
    public MessageEventInfo(string t, object v)
    {
        type = t;
        vaule = v;
    }
}