using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour {
    private Transform _TargetPos1; //左
    private Transform _TargetPos2; //右
    private Transform player;
    private Transform playerPos;
    private void Awake () {
        _TargetPos1 = AllTools.Instance.FindChild (transform, "Sword_L");
        _TargetPos2 = AllTools.Instance.FindChild (transform, "Sword_R");
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        if (player == null) {
            GameDebug.LogError ("Not Find player");
        }
    }
    private void AtkCondition2 (Transform _TargetPos, float radius, float _angle, string Debug = null, int Magnification = 1) {
        Collider[] colliderArr = Physics.OverlapSphere (_TargetPos.position, radius, LayerMask.GetMask ("Player"));
        for (int i = 0; i < colliderArr.Length; i++) {
            Vector3 v3 = colliderArr[i].gameObject.transform.position - _TargetPos.position;
            float angle = Vector3.Angle (v3, _TargetPos.forward);
            if (angle < _angle) {
                // 距离和角度条件都满足了
                if (Debug != null && HoshiController.Instance.isCanDamage) {
                    GameDebug.Log (Debug, "击中");
                    HoShiHp.Instance.HoshiGetHit (Magnification);
                }

            }
        }
    }

    public void GetPlayerPos () {
        playerPos = player;
    }

    private void Attact1 () {
        AtkCondition2 (_TargetPos2, 6, 180, "Attact1");
    }

    private void Attact2 () {
        AtkCondition2 (_TargetPos1, 6, 180, "Attact2");
    }

    private void Magic0 () {
        MyGameObjectPool.Instance.Get ("BossEffect4", "BossEffect4", playerPos.position, Quaternion.identity);
    }

    private void Magic1 () {
        MyGameObjectPool.Instance.Get ("BossEffect", "BossEffect", playerPos.position + new Vector3 (-9.15f, 7.89f, 2), Quaternion.Euler (-35.37f, -74.3f, 0));
    }

    private void Magic2 () {
        MyGameObjectPool.Instance.Get ("BossEffect3", "BossEffect3", playerPos.position, Quaternion.identity);
    }
    private void MagicAttack(){
        AtkCondition2 (playerPos, 4, 360, "Attact2",4);
    }
}