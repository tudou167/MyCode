using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{

    protected override void OnEnable()
    {
        base.OnEnable();
        attackRange = 2;
        radius = attackRange;
        angle = 90;
    }
    protected override void Skill1()
    {
        SkillData skill = SkillInfoManager.Instance.GetSkill(roledata.ID);
        skill.DelegateFunc(transform,skill.DelegateParameter);
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
            target.GetComponent<Hero>().Hurt(v3.normalized * 5, roledata.Weapon.ATK * roledata.LV,true);
        }

    }
    protected override void AttackCheck()
    {
        if (target != null)
        {
            float range = Vector3.Distance(transform.position, target.position);
            if (range < attackRange)
            {
                ani.SetFloat("h", 0);
                ani.SetFloat("v", 0);
                nav.isStopped = true;
                transform.LookAt(target);

                if (Random.Range(1, 101) > 50)
                {
                    ani.SetBool("Skill", false);
                    ani.SetTrigger("Attack");
                }
                else
                {
                    ani.SetBool("Attack", false);
                    ani.SetTrigger("Skill");
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
}
