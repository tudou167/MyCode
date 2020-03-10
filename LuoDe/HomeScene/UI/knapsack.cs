using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class knapsack : MonoBehaviour
{

    public GameObject player;
    Transform knap, play;
    void Start()
    {


    }
    /// <summary>
    /// 打开背包
    /// </summary>
    public void FindPlayer()
    {
        if (knap == null)
        {
            knap = "Prefabs/UI/ZQS/knapsack".toLad("Canvas/Screen");
            //player = GameObject.FindGameObjectWithTag("elseCamera");
            play = knap.transform.Find("Camera");
        }
        Sequence s = DOTween.Sequence();
        s.Append(knap.transform.Find("window").DOScale(new Vector3(0.8f, 0.8f, 1), 0.2f));
        s.Append(knap.transform.Find("window").DOScale(new Vector3(1f, 1f, 1), 0.2f));
        knap.gameObject.SetActive(true);
        MoveCamera.isCanRun = false;
    }
    /// <summary>
    /// 关闭背包
    /// </summary>
    public void back()
    {
        player.gameObject.SetActive(false);
        MoveCamera.isCanRun = true;

    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void initialize()
    {


    }
}
