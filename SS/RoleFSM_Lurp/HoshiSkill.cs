using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoshiSkill : MonoBehaviour {
    private Transform targetPos;
    GameObject go;
    private void Awake () {
        targetPos = AllTools.Instance.FindChild (transform, "targetPos");
    }
    public void Attack8Effect () {
        AtkCondition2 (targetPos, 1.5f, 180, "Attack8");
    }
    public void Attack9Effect () {
        AtkCondition2 (targetPos, 1.5f, 180, "Attack9");
    }
    public void Attack10Effect () {
        AtkCondition2 (targetPos, 3f, 180, "Attack10", 2);
    }
    public void Attack11Effect () {
        AtkCondition2 (targetPos, 3, 360, "Attack11", 3);
        MyGameObjectPool.Instance.Get ("HoshiEffect0", "HoshiEffect0", transform.position + transform.forward * 2, Quaternion.identity);
    }
    public void Attack2 () {
        AtkCondition2 (targetPos, 2f, 180, "Attack2");
    }
    public void Attack18 () {
        AtkCondition2 (targetPos, 2f, 180, "HoshiEffect1", 2);
        go=MyGameObjectPool.Instance.Get ("HoshiEffect1", "HoshiEffect1", transform.position, Quaternion.identity);
    }
    public void Dragon () {
        AtkCondition2 (transform, 5f, 360, "Dragon", 4);
    }
    private void AtkCondition2 (Transform _TargetPos, float radius, float _angle, string Debug = null, int Magnification = 1) {
        Collider[] colliderArr = Physics.OverlapSphere (_TargetPos.position, radius, LayerMask.GetMask ("Boss"));
        for (int i = 0; i < colliderArr.Length; i++) {
            Vector3 v3 = colliderArr[i].gameObject.transform.position - _TargetPos.position;
            float angle = Vector3.Angle (v3, _TargetPos.forward);
            if (angle < _angle) {
                // 距离和角度条件都满足了
                if (Debug != null) GameDebug.Log (Debug, "击中");
                HoShiHp.Instance.BossGetHit (Magnification);
            }
        }
    }
}