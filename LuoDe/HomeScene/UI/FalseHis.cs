using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class FalseHis : MonoBehaviour {


    void Start () {
        yes = start;
        no = close;
    }
    public UnityAction yes,no;
    public GameObject hh;
    public float hi;
    /// <summary>
    /// 只允许代码更改
    /// </summary>
   public  float f;
    void Update () {
        f += Time.deltaTime;
        if (f >= hi)
        {
            close();
            f = 0;
        }
    }
    /// <summary>
    /// 启动聊天框
    /// </summary>
    public void start()
    {
        hh.SetActive(true);
        Sequence s = DOTween.Sequence();
        //s.Append(hh.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0f));
        s.Append(hh.transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.6f));
        s.Append(hh.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
        f = 0;
    }
    /// <summary>
    /// 关闭聊天框
    /// </summary>
    public void close()
    {
       
        Sequence s = DOTween.Sequence();
        //s.Append(hh.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
        s.Append(hh.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.6f));
        //s.Append(hh.transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.6f));
        hh.SetActive(false);
    }
    //private void OnEnable()
    //{

    //}
}
