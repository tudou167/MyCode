using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    #region Panel
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }
    private UIManager()
    {
        ParseUIPanelTypeJson();
    }
    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private Dictionary<UIPanelType, BasePanel> panelDict;//存储所有实例化面板的BasePanel组件
    private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板Prefab的路径

    private Stack<BasePanel> panelStack;
    [System.Serializable]
    private class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList = null;
    }
    //入栈
    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    //出栈
    public void PopPanel()
    {

        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count <= 0) return;
        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }
        // BasePanel panel;
        // panelDict.TryGetValue(panelType,out panel);
        BasePanel panel = panelDict.TryGet(panelType);

        if (panel == null)
        {
            //如果找不到,那么就找这个面板的prefa的路径,然后u根据prefab实例化面板.
            // string path;
            // panelPathDict.TryGetValue(panelType,out path);
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();
        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);
        // for (int i = 0; i < jsonObject.infoList.Count; i++)
        // {
        // 	Debug.Log(jsonObject.infoList[i].panelType);
        // 	panelPathDict.Add(jsonObject.infoList[i].panelType,jsonObject.infoList[i].path);
        // }
        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType, info.path);
        }
    }
    // public void Test(){
    // 	string path;
    // 	panelPathDict.TryGetValue(UIPanelType.MainMenu,out path);
    // 	Debug.Log(path);
    // }
    #endregion
    
}