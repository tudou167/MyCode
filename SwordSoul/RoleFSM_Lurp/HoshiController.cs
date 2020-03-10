using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;
public class HoshiController : MonoSingletion<HoshiController> {
    enum HoshiStates {
        IdleOrWalkOrRun,
        Attack0,
        Attack1,
        Rolling,
        Damage,
        Dead,
        Falling
    }
    private float h;
    private float v;
    private Animator animator;
    private AnimatorStateInfo mStateInfo;
    StateMachine<HoshiStates> fsm;
    private bool isCanIdle = true;
    public bool isCanDamage = true;
    private bool isCanFalling = true;
    private bool isCanRolling = true;
    private float cd;
    public float walkSpeed = 5;
    private Camera mainCamera;
    private CharacterController character;
    //private float gravity = 9.8f;
    public float maxCanStandInGroud = 2;
    private float fallingTime = 0;
    public float rollingCd = 1;
    private float rollingTime = 0;
    private GameObject WeaponTrail_distort;
    private GameObject WeaponTrail_short;
    public override void Awake () {
        base.Awake ();
        fsm = StateMachine<HoshiStates>.Initialize (this);
        animator = GetComponent<Animator> ();
        mStateInfo = animator.GetCurrentAnimatorStateInfo (0);
        mainCamera = Camera.main;
        character = GetComponent<CharacterController> ();
        WeaponTrail_distort = AllTools.Instance.FindChild (transform, "WeaponTrail_distort").gameObject;
        WeaponTrail_short = AllTools.Instance.FindChild (transform, "WeaponTrail_short").gameObject;
        WeaponTrail_short.SetActive (false);
        WeaponTrail_distort.SetActive (false);
    }
    private void Start () {
        fsm.ChangeState (HoshiStates.IdleOrWalkOrRun);
    }
    public void Rotate (Transform transform, float horizontal, float vertical, float fRotateSpeed) {
        Vector3 targetDir = new Vector3 (horizontal, 0, vertical);
        if (targetDir != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation (targetDir, Vector3.up);
            transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation,
                fRotateSpeed);
            //transform.position+=transform.forward*Time.deltaTime*walkSpeed;
            character.SimpleMove (targetDir * walkSpeed);
            // mStateInfo = animator.GetCurrentAnimatorStateInfo (0);
            // if (character.isGrounded == false && isCanFalling) {
            //     fsm.ChangeState (HoshiStates.Falling);
            //     animator.SetBool ("isGrounded", character.isGrounded);
            // }
        }
    }
    private void Update () {
        rollingTime += Time.deltaTime;
        h = Input.GetAxis ("Horizontal");
        v = Input.GetAxis ("Vertical");
        if (Input.GetKeyDown (KeyCode.Space) && isCanRolling && rollingTime >= rollingCd) {
            fsm.ChangeState (HoshiStates.Rolling);
        }

    }
    public void Rolling_Enter () {
        animator.SetTrigger ("ToRolling");
        isCanDamage = false;
    }
    public void Rolling_Exit () {
        rollingTime = 0;
        isCanDamage = true;
    }
    public void Rolling_Update () {
        mStateInfo = animator.GetCurrentAnimatorStateInfo (0);

        h *= 3;
        v *= 3;
        animator.SetFloat ("h", h);
        animator.SetFloat ("v", v);
        Rotate (transform, h, v, walkSpeed);
        if (mStateInfo.normalizedTime > 0.8f) {
            fsm.ChangeState (HoshiStates.IdleOrWalkOrRun);
        }
    }
    #region Idle 走路 跑
    public void IdleOrWalkOrRun_Enter () {

    }
    public void IdleOrWalkOrRun_Exit () {

    }
    public void IdleOrWalkOrRun_Update () {
        if (Input.GetMouseButtonDown (0)) {
            fsm.ChangeState (HoshiStates.Attack0);
        }
        if (Input.GetMouseButtonDown (1)) {
            fsm.ChangeState (HoshiStates.Attack1);
        }
        if (Input.GetKey (KeyCode.LeftShift)) {
            h *= 2;
            v *= 2;
        }
        animator.SetFloat ("h", h);
        animator.SetFloat ("v", v);
        Rotate (transform, h, v, walkSpeed);
        moveVector = Vector3.zero;
        if (character.isGrounded == false) {

            moveVector += Physics.gravity;
        }
        character.SimpleMove (moveVector * Time.deltaTime);

    }

    #endregion
    Vector3 moveVector;
    // public void Falling_Enter () {
    //     GameDebug.Log ("Falling","进入");
    //     fallingTime = 0;
    //     animator.SetBool ("Rolling", isCanRolling = false);
    //     isCanDamage = false;
    // }
    // public void Falling_Exit () {
    //     animator.SetBool ("Rolling", isCanRolling = true);
    // }
    // public void Falling_FixedUpdate () {
    //     moveVector = Vector3.zero;
    //     if (character.isGrounded == false) {

    //         moveVector += Physics.gravity;
    //     }
    //     character.SimpleMove (moveVector * Time.deltaTime);
    //     fallingTime += Time.deltaTime;
    //     if (character.isGrounded == true) {
    //         if (fallingTime >= maxCanStandInGroud) {
    //             animator.SetBool ("isCanDownEnd", false);
    //         } else {
    //             animator.SetBool ("isCanDownEnd", true);
    //         }
    //         animator.SetBool ("isGrounded", character.isGrounded);
    //         mStateInfo = animator.GetCurrentAnimatorStateInfo (0);
    //         if (mStateInfo.normalizedTime >= 0.8f && (mStateInfo.IsName ("Falling_Front_Down_Up") || mStateInfo.IsName ("Falling_Front_End"))) {
    //             fsm.ChangeState (HoshiStates.IdleOrWalkOrRun);
    //             GameDebug.Log ("Idle","进入");
    //         }
    //     }

    // }
    #region 连击

    public void Attack0_Enter () {
        animator.SetTrigger ("Attack0");
        WeaponTrail_short.SetActive (true);
        WeaponTrail_distort.SetActive (true);
    }
    public void Attack0_Exit () {
        WeaponTrail_short.SetActive (false);
        WeaponTrail_distort.SetActive (false);
    }
    public void Attack0_Update () {
        //获取状态信息
        mStateInfo = animator.GetCurrentAnimatorStateInfo (0);
        cd += Time.deltaTime;
        if (mStateInfo.normalizedTime >= 0.85f) {
            fsm.ChangeState (HoshiStates.IdleOrWalkOrRun);
        }
        if (Input.GetMouseButtonDown (0) && cd >= 0.2f) {
            if (mStateInfo.IsName ("Attack11")) return;
            animator.SetTrigger ("Attack0");
            cd = 0;
        }
    }
    public void Attack1_Enter () {
        animator.SetTrigger ("Attack1");
        WeaponTrail_short.SetActive (true);
        WeaponTrail_distort.SetActive (true);
    }
    public void Attack1_Exit () {
        WeaponTrail_short.SetActive (false);
        WeaponTrail_distort.SetActive (false);
    }
    public void Attack1_Update () {
        //获取状态信息
        mStateInfo = animator.GetCurrentAnimatorStateInfo (0);
        cd += Time.deltaTime;
        if (mStateInfo.normalizedTime >= 0.85f) {
            fsm.ChangeState (HoshiStates.IdleOrWalkOrRun);
        }
        if (Input.GetMouseButtonDown (1) && cd >= 0.2f) {
            if (mStateInfo.IsName ("Attack18")) return;
            animator.SetTrigger ("Attack1");
            cd = 0;
        }
    }
    #endregion
    public void GetHit () {
        if (isCanDamage) fsm.ChangeState (HoshiStates.Damage);
    }
    private void Damage_Enter () {
        if (Random.value < 0.5) animator.SetTrigger ("Damage1");
        else animator.SetTrigger ("Damage2");
    }
    private void Damage_Update () {
        mStateInfo = animator.GetCurrentAnimatorStateInfo (0);
        if (mStateInfo.normalizedTime > 0.625f && (mStateInfo.IsName ("Damage_01") || mStateInfo.IsName ("Damage_02"))) {
            fsm.ChangeState (HoshiStates.IdleOrWalkOrRun);
        }
    }
    public void SetHoshiDead () {
        fsm.ChangeState (HoshiStates.Dead);
        animator.SetBool ("isDie", true);
    }
    public void Dead_Enter () {
        isCanIdle = false;
        isCanDamage = false;
        isCanFalling = false;
        isCanRolling = false;
    }
}