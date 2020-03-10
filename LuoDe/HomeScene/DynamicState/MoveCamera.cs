using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MoveCamera : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static bool isCanRun = true;
    void Start()
    {
        vidicon = GameObject.Find("AimPoint");
    }
    GameObject vidicon;
    float pureX, pureY,tX,tY;
    private RaycastHit la;

    float x, y, z;
    bool kk,ty;
    /// <summary>
    /// 按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        pureX = eventData.position.x;
        pureY = eventData.position.y;
        tX = pureX;
        tY = pureY;

        Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pos.origin, pos.direction * 10, Color.yellow);
        Physics.Raycast(pos, out la);


        if (la.rigidbody != null)//&& EventSystem.current.currentSelectedGameObject)
        {
            x = la.transform.localScale.x;
            y = la.transform.localScale.y;
            z = la.transform.localScale.z;
            //Debug.Log(la.transform.name);
            la.transform.DOScale(new Vector3(x * 1.1f, y * 1.1f, z * 1.1f), 0.3f);
            kk = true;
        }


    }
    /// <summary>
    /// 滑动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (isCanRun == false) return;
        //Debug.Log("原位置"+eventData.position.x);
        //Debug.Log("改变的位置"+pureX);
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (eventData.position.x > pureX)
            {
                vidicon.transform.Translate(-0.2f, 0, 0);
                pureX = eventData.position.x;
                // Debug.Log("pureX--:"+pureX);
            }
            else
            {
                vidicon.transform.Translate(0.2f, 0, 0);
                pureX = eventData.position.x;
                //pureX++;
                //Debug.Log("pureX++:" + pureX);
            }
            if (vidicon.transform.position.x < 0)
            {
                vidicon.transform.position = new Vector3(0, vidicon.transform.position.y, vidicon.transform.position.z);
            }
            else if (vidicon.transform.position.x > 20)
            {
                vidicon.transform.position = new Vector3(20, vidicon.transform.position.y, vidicon.transform.position.z);
            }

            if (eventData.position.y > pureY)
            {
                vidicon.transform.Translate(0, -0.1f, 0);
                pureY = eventData.position.y;
                // pureY--;
            }
            else
            {
                vidicon.transform.Translate(0, 0.1f, 0);
                pureY = eventData.position.y;
                //pureY++;
            }
            if (vidicon.transform.position.y < -3)
            {
                // vidicon.transform.position = new Vector3(vidicon.transform.position.x, 1, vidicon.transform.position.z);
                vidicon.transform.Translate(0, 0.1f, 0);
            }
            else if (vidicon.transform.position.y > 5)
            {
                // vidicon.transform.position = new Vector3(vidicon.transform.position.x, 5, vidicon.transform.position.z);
                vidicon.transform.Translate(0, -0.1f, 0);
            }

        }
        //if (eventData.position.y > pureY)
        //{
        //    vidicon.transform.Rotate(-1f,0 , 0);
        //    pureY = eventData.position.y;
        //}
        //else
        //{
        //    vidicon.transform.Rotate(1f, 0, 0);
        //    pureY = eventData.position.y;
        //}
        //if (vidicon.transform.eulerAngles.x < 1)
        //{
        //    vidicon.transform.eulerAngles = new Vector3(1,vidicon.transform.eulerAngles.y, vidicon.transform.eulerAngles.z);
        //}
        //else if (vidicon.transform.eulerAngles.x > 99)
        //{
        //    vidicon.transform.eulerAngles = new Vector3(99,vidicon.transform.eulerAngles.y, vidicon.transform.eulerAngles.z);
        // }


        //if (eventData.position.y > pureY)
        //{
        //    vidicon.transform.Translate(0, -0.1f, 0);
        //    pureY = eventData.position.y;
        //    // pureY--;
        //}
        //else
        //{
        //    vidicon.transform.Translate(0, 0.1f, 0);
        //    pureY = eventData.position.y;
        //    //pureY++;
        //}
        //if (vidicon.transform.position.y < 1)
        //{
        //    vidicon.transform.position = new Vector3(vidicon.transform.position.x, 1, vidicon.transform.position.z);
        //}
        //else if (vidicon.transform.position.y > 5)
        //{
        //    vidicon.transform.position = new Vector3(vidicon.transform.position.x, 5, vidicon.transform.position.z);
        //}
    }
    Module module;
    /// <summary>
    /// 提起
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        // vidicon.transform.position = new Vector3(vidicon.transform.position.x, vidicon.transform.position.y, 55);
        if (kk)
        {
            la.transform.DOScale(new Vector3(x, y, z), 0.3f);
            if (tY == pureY &&tX == pureX)
            {
                if (la.transform != null)
                {
                module = la.transform.GetComponent<Module>();
                }
                else
                {
                    Debug.Log("这个东西没有事件");
                }
                if (module != null)
                {
                module.Func();
                }
               
                kk = false;
            }
           
        }
    }

    //void Update()
    //{

    //}


}
