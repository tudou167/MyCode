using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XiaoGuai8 : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        attackRange = 6;
    }

    protected override void Attack()
    {
        radius = 6;
        angle = 60;
        Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + "Enemy/xiaoguai8_atk", target.position, Quaternion.identity, transform.parent);
        base.Attack();
    }
}
