using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemSliding : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler {

    Button article1, article2, article3;
   public  Transform Location1, Location2, Location3;
    void Start () {
        article1 = transform.Find("Article/article1").GetComponent<Button>();
        article2 = transform.Find("Article/article2").GetComponent<Button>();
        article3 = transform.Find("Article/article3").GetComponent<Button>();
        article1.onClick.AddListener(RedElixir1);
        article2.onClick.AddListener(RedElixir2);
        article3.onClick.AddListener(RedElixir3);
        Location1 = transform.Find("Article/Location1");
        Location2 = transform.Find("Article/Location2");
        Location3 = transform.Find("Article/Location3");
        Fac = OnResets;
    }
	
	
	void Update () {
        


    }
    public void RedElixir1()
    {
        Debug.Log("你点击了按钮1");
    }
    public void RedElixir2()
    {
        Debug.Log("你点击了按钮2");
    }
    public void RedElixir3()
    {
        Debug.Log("你点击了按钮3");
    }
    float x;
    /// <summary>
    /// 按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("按下");
        x = eventData.position.x;
        a1 = true; 
        // PointerEventData.InputButton
        //eventData.useDragThreshold = true;
        //d = eventData.button;

    }

    public UnityAction Fac;
    /// <summary>
    /// 提起
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("提起了");

        Fac();
    }
    /// <summary>
    /// 滑动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.position.x);
        if (eventData.position.x >x)
        {
            article1.transform.Translate(1f,0,0);
            article2.transform.Translate(1f, 0, 0);
            article3.transform.Translate(1f, 0, 0);
        }
        else if(eventData.position.x < x)
        {
            article1.transform.Translate(-1f, 0, 0);
            article2.transform.Translate(-1f, 0, 0);
            article3.transform.Translate(-1f, 0, 0);
        }
        x = eventData.position.x;
        if (article1.transform.localPosition.x < -243)
        {
            article1.transform.position = Location3.position;
        }
        else if (article1.transform.localPosition.x > 243)
        {
            article1.transform.position = Location1.position;
        }
        if (article2.transform.localPosition.x < -243)
        {
            article2.transform.position = Location3.position;
        }
        else if (article2.transform.localPosition.x > 243)
        {
            article2.transform.position = Location1.position;
        }

        if (article3.transform.localPosition.x < -243)
        {
            article3.transform.position = Location3.position;
        }
        else if (article3.transform.localPosition.x > 243)
        {
            article3.transform.position = Location1.position;
        }

    }
    bool a1 = true;
    public void OnResets()
    {
        Debug.Log("执行了OnReset函数");
        while (a1 )
        {
            Debug.Log("进入了循环");
            if (article1.transform.localPosition.x > -60 && article1.transform.localPosition.x < -0.001f 
                || article2.transform.localPosition.x > -60 && article2.transform.localPosition.x < 0.001f
                || article3.transform.localPosition.x > -60 && article3.transform.localPosition.x < 0.01f)
            {
                article1.transform.Translate(0.1f, 0, 0);
                article2.transform.Translate(0.1f, 0, 0);
                article3.transform.Translate(0.1f, 0, 0);
            }
            else if (article1.transform.localPosition.x > 60 && article1.transform.localPosition.x < 0.001f
                || article2.transform.localPosition.x > 60 && article2.transform.localPosition.x < -0.01f
                || article3.transform.localPosition.x > 60 && article3.transform.localPosition.x < -0.01f)
            {
                article1.transform.Translate(-0.1f, 0, 0);
                article2.transform.Translate(-0.1f, 0, 0);
                article3.transform.Translate(-0.1f, 0, 0);
            }
            else
            {
                a1 = false;
            }

            //if (article2.transform.localPosition.x > -60 && article2.transform.localPosition.x < 0.001f)
            //{
            //    article1.transform.Translate(1f, 0, 0);
            //    article2.transform.Translate(1f, 0, 0);
            //    article3.transform.Translate(1f, 0, 0);
            //}
            //else if (article2.transform.localPosition.x > 60 && article2.transform.localPosition.x < -0.01f)
            //{
            //    article1.transform.Translate(-1f, 0, 0);
            //    article2.transform.Translate(-1f, 0, 0);
            //    article3.transform.Translate(-1f, 0, 0);
            //}
            //else
            //{
            //    a2 = false;
            //}

            //if (article3.transform.localPosition.x > -60 && article3.transform.localPosition.x < 0.01f)
            //{
            //    article1.transform.Translate(1f, 0, 0);
            //    article2.transform.Translate(1f, 0, 0);
            //    article3.transform.Translate(1f, 0, 0);
            //}
            //else if (article3.transform.localPosition.x > 60 && article3.transform.localPosition.x < -0.01f)
            //{
            //    article1.transform.Translate(-1f, 0, 0);
            //    article2.transform.Translate(-1f, 0, 0);
            //    article3.transform.Translate(-1f, 0, 0);
            //}
            //else
            //{
            //    a3 = false;
            //}
        }
    }

   
}
