using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.AI;

public class BossController : MonoSingletion<BossController>
{
    private enum BossStates
    {
        Start,
        Idle,
        Walk,
        GetHit,
        GetHitByHp,
        Attack,
        Magic,
        Die
    }

    private Animator animator;
    private AnimatorStateInfo mStateInfo;
    private StateMachine<BossStates> fsm;
    private bool isStart;
    private bool isCanAttack = true;
    private bool isCanMagic = true;
    private bool isCanGetHit = true;
    private bool isDie;
    public float walkSpeed = 3;
    private CharacterController character;
    private NavMeshAgent nav;
    private GameObject player;
    public float attackDistance = 6;//这是攻击目标的距离，也是寻路的目标距离
    public float attackTime = 2;   //设置定时器时间 3秒攻击一次
    private float attackCounter = 0; //计时器变量
    public float magicDistance = 30;//这是攻击目标的距离，也是寻路的目标距离
    public float magicTime = 10;   //设置定时器时间 3秒攻击一次
    private float magicCounter = 0; //计时器变量

    public override void Awake()
    {
        base.Awake();
        fsm = StateMachine<BossStates>.Initialize(this);
        animator = GetComponent<Animator>();
        mStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        character = GetComponent<CharacterController>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        fsm.ChangeState(BossStates.Start);
    }

    public void Start_Update()
    {
        mStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (mStateInfo.normalizedTime > 0.95f)
        {
            fsm.ChangeState(BossStates.Idle);
        }
    }

    public void Idle_Enter()
    {
        //GameDebug.Log("Idle", "进入");
    }

    public void Idle_Exit()
    {
        //GameDebug.Log("Idle", "退出");
    }

    public void Idle_Update()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y = transform.position.y;//此处的作用将自身的Y轴值赋值给目标Y值
        transform.LookAt(targetPos);//旋转的时候就保证已自己Y轴为轴心旋转
        float distance = Vector3.Distance(targetPos, transform.position);
        magicCounter += Time.deltaTime;
        if (magicCounter > magicTime)//定时器功能实现
        {
            fsm.ChangeState(BossStates.Magic);
        }
        attackCounter += Time.deltaTime;
        if (distance <= attackDistance)
        {
            if (attackCounter > attackTime)//定时器功能实现
            {
                attackCounter = 0;
                fsm.ChangeState(BossStates.Attack);
            }
        }
        else
        {
            fsm.ChangeState(BossStates.Walk);
        }
    }

    public void Walk_Enter()
    {
        animator.SetBool("isCanWalk", true);
        //GameDebug.Log("Walk", "进入");
    }

    public void Walk_Exit()
    {
        animator.SetBool("isCanWalk", false);
        //GameDebug.Log("Walk", "退出");
    }

    public void Walk_Update()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y = transform.position.y;//此处的作用将自身的Y轴值赋值给目标Y值
        transform.LookAt(targetPos);//旋转的时候就保证已自己Y轴为轴心旋转
        float distance = Vector3.Distance(targetPos, transform.position);
        attackCounter += Time.deltaTime;
        character.Move(transform.forward * walkSpeed * Time.deltaTime);
        magicCounter += Time.deltaTime;
        if (magicCounter > magicTime)//定时器功能实现
        {
            fsm.ChangeState(BossStates.Magic);
        }
        if (distance <= attackDistance)
        {
            fsm.ChangeState(BossStates.Idle);
        }
    }

    public void Attack_Enter()
    {
        int num = Random.Range(0, 2);//攻击动画有两种，此处就利用随机数（【0】，【1】）切换两种动画
        animator.SetInteger("AttackInt", num);
        animator.SetTrigger("StartAttack");
    }

    public void Attack_Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            fsm.ChangeState(BossStates.Idle);
        }
    }

    public void Magic_Enter()
    {
        int num = Random.Range(0, 3);//攻击动画有两种，此处就利用随机数（【0】，【1】）切换两种动画
        animator.SetInteger("MagicInt", num);
        animator.SetTrigger("StartMagic");
        magicCounter = 0;
    }

    public void Magic_Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            fsm.ChangeState(BossStates.Idle);
        }
    }
    public void GetHitByHp(){
        fsm.ChangeState(BossStates.GetHitByHp);
    }
    private void GetHitByHp_Enter(){
        animator.SetTrigger("GetHitByHp");
    }
    private void GetHitByHp_Update(){
        mStateInfo=animator.GetCurrentAnimatorStateInfo(0);
        if (mStateInfo.IsName("Buff")&&mStateInfo.normalizedTime>=0.94f)
        {
            fsm.ChangeState(BossStates.Idle);
        }
    }
    public void SetBossDead(){
        fsm.ChangeState(BossStates.Die);
        animator.SetBool("isDie",true);
    }
}