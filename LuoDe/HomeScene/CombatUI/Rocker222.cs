using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Rocker222 : MonoBehaviour ,IPointerDownHandler,IPointerUpHandler, IDragHandler
{
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("hero").GetComponent<Hero>();
    }
    Hero hero;
    public RectTransform rocker, rockerX;
    public float r = 50;
    public  float y, x;
    /// <summary>
    /// 滑动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
         rocker, eventData.position, eventData.pressEventCamera, out pos);
        if (pos .magnitude >r)
        {
            pos = pos.normalized * r;
        }
        rockerX.localPosition = pos;

       // Debug.Log(rockerX.localPosition);
        x = rockerX.localPosition.x/50;
        y = rockerX.localPosition.y/ 50;
        //Debug.Log("x="+x + "y=" + y);
    }

    /// <summary>
    /// 按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform, eventData.position, eventData.pressEventCamera, out pos);
        rocker.localPosition = pos;
    }
    /// <summary>
    /// 提起
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        rockerX.localPosition = Vector3.zero;
        rocker.localPosition = Vector3.zero;
        x = 0;
        y = 0;
     }

   void Update () {
       // hero.h = x ;
       // hero.v = y ;
       // hero.vRaw = y;
       // hero.hRaw = x;
       // Debug.Log(hero.h+"+" + hero.v);
    }
}
