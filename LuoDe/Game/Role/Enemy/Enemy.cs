using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Role
{
    protected Transform target;
    protected NavMeshAgent nav;
    GameObject hpUI;
    GameObject nameUI;
    Vector3 ShowUIPos;
    public float ShowUIPosUp;
    Slider hp;
    Text nameText;
    float h;
    float v;

    // 距离
    public float radius;
    // 角度
    public int angle;
    // 触发攻击的距离
    public float attackRange;
    // 死亡委托
    public UnityEngine.Events.UnityAction<RoleData, RoleType, Vector3> deadcallback;
    new void Awake()
    {
        h = 0;
        v = 0;
        ShowUIPosUp = 1.8f;
        ShowUIPos = transform.position + transform.up * ShowUIPosUp;
        base.Awake();
    }
    protected virtual void OnEnable()
    {
        switch (roleType)
        {
            case RoleType.xiaoguai:
                attackRange = 2f;
                break;
            case RoleType.jyg:
                attackRange = 2f;
                break;
            case RoleType.boss:
                attackRange = 2f;
                break;
        }
        radius = attackRange;
        angle = 85;
        isDead = false;
        if (cc != null) cc.enabled = true;
        if (nav != null) nav.enabled = true;

        hpUI = Pool.Instance.GetObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/EnemyHp", ShowUIPos, Quaternion.identity, hud);
        hp = hpUI.GetComponent<Slider>();
        hpUI.transform.localScale = Vector3.one;
        hpUI.transform.localRotation = Quaternion.identity;
        hpUI.gameObject.SetActive(false);

        nameUI = Pool.Instance.GetObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/EnemyName", ShowUIPos, Quaternion.identity, hud);
        nameText = nameUI.GetComponent<Text>();
        nameUI.transform.localScale = Vector3.one;
        nameUI.transform.localRotation = Quaternion.identity;
        nameUI.gameObject.SetActive(false);

    }
    protected override void Start()
    {
        base.Start();
        target = HeroManager.Instance.GetCurHero();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 1;
        string str = "";
        switch (roleType)
        {
            case RoleType.xiaoguai:
                str = "<color=white><b>" + roledata.Name + "</b></color>";
                break;
            case RoleType.jyg:
                str = "<color=green><b>" + roledata.Name + "</b></color>";
                break;
            case RoleType.boss:
                str = "<color=yellow><b>" + roledata.Name + "</b></color>";
                break;
        }
        nameText.text = str;

        hpUI.transform.SetParent(hud, false);
        nameUI.transform.SetParent(hud, false);

        hpUI.transform.position = ShowUIPos;
        nameUI.transform.position = ShowUIPos;
    }
    void Update()
    {
        if (!cc.isGrounded) transform.position -= transform.up * Time.deltaTime;
        if (isDead) return;
        HPCheck();
        UICheck();
        aniInfo = ani.GetCurrentAnimatorStateInfo(0);
        if (IsBack()) return;
        AttackCheck();

    }

    private void UICheck()
    {
        if (hpUI.gameObject.activeSelf == false || nameUI.gameObject.activeSelf == false)
        {
            hpUI.gameObject.SetActive(true);
            nameUI.gameObject.SetActive(true);
        }
        ShowUIPos = transform.position + transform.up * ShowUIPosUp;
        hpUI.transform.position = ShowUIPos;
        nameUI.transform.position = hpUI.transform.position + transform.up * 0.1f;
        hp.value = (float)curHp / (float)roledata.HP;
    }

    private void HPCheck()
    {
        if (curHp > 0) return;
        Pool.Instance.RecyleObj(hpUI, "UI");
        Pool.Instance.RecyleObj(nameUI, "UI");
        nav.isStopped = true;
        cc.enabled = false;
        nav.enabled = false;
        ani.SetTrigger("Die");
        isDead = true;
        return;
    }
    protected virtual void AttackCheck()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {
                ani.SetFloat("h", 0);
                ani.SetFloat("v", 0);
                nav.isStopped = true;

                transform.LookAt(target);
                if (roleType != RoleType.xiaoguai)
                {
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
                    ani.SetTrigger("Attack");
                }

            }
            else
            {
                if (!aniInfo.IsName("Move")) return;
                if (aniInfo.IsName("Die")) return;
                if (nav.enabled == true)
                {
                    nav.isStopped = false;
                    nav.SetDestination(target.position);
                    ani.SetBool("Skill", false);
                    ani.SetBool("Attack", false);
                    ani.SetFloat("h", 0);
                    ani.SetFloat("v", 1);
                }
            }
        }
    }

    protected virtual void Attack()
    {
        if (isDead) return;
        //距离
        float dis = Vector3.Distance(transform.position, target.transform.position);
        //正前方
        Vector3 dir1 = transform.forward;
        //指向目标的向量
        Vector3 dir2 = target.transform.position - transform.position;
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
            Vector3 v3 = target.transform.position - transform.position;
            target.GetComponent<Hero>().Hurt(v3.normalized * 5, roledata.ATK);
        }

    }
    //private void MageAtkEvent(GameObject enemy)
    //{
    //    //击退
    //    Vector3 v3 = enemy.transform.position - transform.position;
    //    enemy.GetComponent<Hero>().Hurt(v3.normalized * (roledata.Repel / 10), roledata.ATK);
    //}

    protected virtual void Skill1()
    {
        switch (roleType)
        {
            case RoleType.jyg:
                Attack();
                break;
            case RoleType.boss:
                break;
        }
    }
    private void Dead()
    {
        if (deadcallback != null) deadcallback(roledata, roleType, transform.position);
        //一定要最后
        Pool.Instance.RecyleObj(gameObject, "Role");
    }
}
