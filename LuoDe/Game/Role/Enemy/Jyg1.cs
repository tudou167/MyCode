using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jyg1 : Enemy {

    protected override void Attack()
    {
        Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + "Enemy/jyg1_atk", transform.position + transform.up, transform.rotation, transform.parent).GetComponent<EnemyMageAtk>().callBack = MageAtkEvent;
    }
    protected override void Skill1()
    {
        if (isDead) return;
        radius = 3;
        angle = 65;
        float dis = Vector3.Distance(transform.position, target.transform.position);
        Vector3 dir1 = transform.forward;
        Vector3 dir2 = target.transform.position - transform.position;
        float dot = Vector3.Dot(dir1, dir2.normalized);
        float deg = Mathf.Acos(dot) * Mathf.Rad2Deg;
        Vector3 cross = Vector3.Cross(dir2, dir1);
        if ((cross.y >= 0 && deg <= angle || cross.y <= 0 && deg <= angle) && dis <= radius)
        {
            Vector3 v3 = target.transform.position - transform.position;
            target.GetComponent<Hero>().Hurt(v3.normalized * 5, roledata.ATK);
        }
    }
    protected override void AttackCheck()
    {
        if (target != null)
        {
            float attackRange = 8;
            float attackCloseRange = 4;
            float range = Vector3.Distance(transform.position, target.position);
            if (range < attackRange)
            {
                ani.SetFloat("h", 0);
                ani.SetFloat("v", 0);
                nav.isStopped = true;
                transform.LookAt(target);

                if (range < attackCloseRange)
                {
                    ani.SetTrigger("Skill");
                    ani.SetBool("Attack", false);
                }
                else
                {
                    ani.SetTrigger("Attack");
                    ani.SetBool("Skill", false);
                }
            }
            else
            {
                nav.isStopped = false;
                ani.SetFloat("h", 0);
                ani.SetFloat("v", 1);
                nav.SetDestination(target.position);
            }
        }
    }
    private void MageAtkEvent(GameObject enemy)
    {
        //击退
        Vector3 v3 = enemy.transform.position - transform.position;
        enemy.GetComponent<Hero>().Hurt(v3.normalized * (roledata.Repel / 10), roledata.ATK);
    }
}
