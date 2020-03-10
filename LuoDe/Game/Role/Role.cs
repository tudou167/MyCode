using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    protected UIFightController uifc;
    public Transform hud;

    protected CharacterController cc;
    protected Animator ani;
    protected AnimatorStateInfo aniInfo;
    protected RoleType roleType;

    public RoleData roledata;
    public Vector3 backDir;
    public List<float> curCDList;
    public bool isBack;
    protected bool isDead;

    public int curHp;
    private FightTmpData fData;
    //衰退值
    public float deathBackDir = 10;

    protected void Awake()
    {
        curHp = 1;
        cc = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        backDir = Vector3.zero;
        //roleType = tag == RoleType.xiaoguai.ToString() ? RoleType.xiaoguai : tag == RoleType.jyg.ToString() ? RoleType.jyg : tag == RoleType.boss.ToString() ? RoleType.boss : RoleType.hero;
        roleType = (RoleType)System.Enum.Parse(typeof(RoleType), tag.ToString());
        if (roleType == RoleType.hero) HeroManager.Instance.SetCurHero(transform);

    }
    protected virtual void Start()
    {
        hud = GameObject.Find("/HUDCanvas").transform;
        uifc = GameObject.Find("/UI/Canvas/PhoneFightUI").GetComponent<UIFightController>();
        fData = FightTmpManager.Instance.GetData();
    }
    public virtual void Hurt(Vector3 backDir, int damage, bool back = false)
    {
        if (isDead || fData.isBack || roledata == null) return;
        this.backDir = backDir;
        isBack = back;
        curHp -= damage;
        uifc.ShowDamage(transform, damage, hud);
        if (roleType == RoleType.hero)
        {
            uifc.ChangeHP(curHp, roledata.HP * roledata.LV);
        }
    }

    protected bool IsBack()
    {
        if (cc.enabled == false || roleType == RoleType.hero) return false;
        //magnitude == 模长
        if (backDir.magnitude > 0.01f)
        {
            cc.SimpleMove(backDir);
            float dir = backDir.magnitude;
            dir = Mathf.Clamp(dir - Time.deltaTime * deathBackDir, 0, dir);
            backDir = backDir.normalized * dir;
            return true;
        }
        return false;
    }
}
