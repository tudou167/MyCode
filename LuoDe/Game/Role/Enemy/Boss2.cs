using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Enemy {

    protected override void OnEnable()
    {
        base.OnEnable();
        attackRange = 8;
        radius = 2.5f;
        angle = 90;
    }
    protected override void Attack()
    {
        if (isDead) return;
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
    protected override void Skill1()
    {
        SkillData skill = SkillInfoManager.Instance.GetSkill(roledata.ID);
        skill.DelegateFunc(transform,skill.DelegateParameter);
    }
    protected override void AttackCheck()
    {
        if (target != null)
        {
            float attackCloseRange = radius;
            float range = Vector3.Distance(transform.position, target.position);
            if (range < attackRange)
            {
                ani.SetFloat("h", 0);
                ani.SetFloat("v", 0);
                nav.isStopped = true;
                transform.LookAt(target);

                if (range < attackCloseRange)
                {
                    ani.SetTrigger("Attack");
                    ani.SetBool("Skill", false);
                }
                else
                {
                    ani.SetTrigger("Skill");
                    ani.SetBool("Attack", false);
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
    public void MageAtkEvent(GameObject enemy)
    {
        //击退
        Vector3 v3 = enemy.transform.position - transform.position;
        enemy.GetComponent<Hero>().Hurt(v3.normalized * (roledata.Repel / 10), roledata.ATK,true);
    }
}
