using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteHis : MonoBehaviour {
    /// <summary>
    /// 模式1(在指定的时间删除自己) 模式2(在指定的时间删除自己并且y移动)
    /// </summary>
    public enum Pattern
    {
        pattern1,
        pattern2,
        pattern3,
    }
    /// <summary>
    /// 要消除的物体
    /// </summary>
    public GameObject hh;
    /// <summary>
    /// 多少时间消除
    /// </summary>
    public float hi = 5;
    /// <summary>
    /// 模式 默认2
    /// </summary>
    public Pattern pattern = Pattern.pattern1;
	void Start () {
        if (pattern == Pattern.pattern1)
        {
           Destroy(hh, hi);
        }
       
            //for (int i = 0; i < 1000; i++)
            //{
            //  StartCoroutine("uul");
            //}
     }
    /// <summary>
    /// FlightSpeed飞行速度 FlightTime飞行时间<模式2>
    /// </summary>
    public float FlightSpeed, FlightTime = 0.1f;
    float f;
    /// <summary>
    /// 直接销毁的时间<模式3使用>hi大于这个时间就销毁
    /// </summary>
     public float debugTime = 0;
    private void Update()
    {
        debugTime +=  Time.deltaTime ;
        f += Time.deltaTime;
        if (pattern == Pattern.pattern2)
        {
            Destroy(hh, hi);
            if (f >= FlightTime)
            {
                transform.Translate(0, FlightSpeed, 0);
                f = 0;
            }
        }
        else if (pattern == Pattern.pattern3)
        {
            if (debugTime >= hi)
            {
                Destroy(hh);
            }
        }
        else
        {
            return;
        }
    }

    //IEnumerator uul()
    //   {
    //       transform.Translate(0, 0.01f, 0);
    //       yield return new WaitForSeconds(0.5f);
    //       transform.Translate(0, 0.01f, 0);
    //   }


}
