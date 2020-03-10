using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sell : MonoBehaviour {

    FalseHis bubble;
    string nam;
    void Start () {
        bubble= transform.Find("L2D/ChatBubble").GetComponent<FalseHis>();
     Text n =  transform.Find("Image/name").GetComponent<Text>();
        nam = n.text;
        Debug.Log(n.text);
	}
	
    public void Onsell()
    {
        bubble.yes();
        bubble.transform.Find("Text").GetComponent<Text>();

    }
	public void OnHint()
    {
    Transform hint  =  "Prefabs/UI/ZQS/BeInCommonUse/Hint".toLad("Canvas/Screen");
        hint.Find("window/Image/Text").GetComponent<Text>().text = "<color=red>温馨提示</color>";
        hint.Find("window/text/TextContent").GetComponent<Text>().text = "确定出售<color=red>" + nam + "</color>这个物品吗";
        hint.Find("window/text/login/Text").GetComponent<Text>().text = "出售";
        hint.Find("window/text/back/Text").GetComponent<Text>().text = "取消";
        Back back = hint.Find("window/text/back").gameObject.AddComponent<Back>();
        back.transform.GetComponent<Button>().onClick.AddListener(back.back);
        back.Game = hint.gameObject;
        //hint.Find("window/text/login").GetComponent<Button>().onClick.AddListener(yes);
    }
    /// <summary>
    /// 调用出售方法
    /// </summary>
    // public void yes()
    //{

    //}
	//void Update () { }
}
