using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public EffectData data;
    public UnityEngine.Events.UnityAction callBack;
    protected virtual void OnEnable()
    {
        StartCoroutine(Recyle());
    }
    public IEnumerator Recyle()
    {
        //等待一帧 让那边的参数能传过来
        yield return null;
        RangeCheck();
        yield return new WaitForSeconds(data.recyleTime);
        Pool.Instance.RecyleObj(gameObject, "Effect");
        if (callBack != null) callBack();
    }
    protected void RangeCheck()
    {
        // 距离 范围
        float radius = data.range;
        // 角度
        int angle = data.angleNum;
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, 1 << LayerMask.NameToLayer(LayerType.enemy.ToString()));
        for (int i = 0; i < cols.Length; i++)
        {
            //距离
            float dis = Vector3.Distance(transform.position, cols[i].transform.position);
            //英雄正前方
            Vector3 dir1 = transform.forward;
            //英雄指向目标的向量
            Vector3 dir2 = cols[i].transform.position - transform.position;
            //点乘
            float dot = Vector3.Dot(dir1, dir2.normalized);
            //角度            余弦            弧度转角度
            float deg = Mathf.Acos(dot) * Mathf.Rad2Deg;
            //叉乘
            Vector3 cross = Vector3.Cross(dir2, dir1);
            //Debug.LogFormat("deg:{0},位置:{1},dis:{2}", deg, cross.y > 0 ? "左边" : "右边", dis);
            if ((cross.y >= 0 && deg <= angle || cross.y <= 0 && deg <= angle) && dis <= radius)
            {
                //击退
                Vector3 v3 = cols[i].transform.position - transform.position;
                cols[i].GetComponent<Enemy>().Hurt(v3.normalized * data.repel, data.damage,true);
            }
        }
    }
}
