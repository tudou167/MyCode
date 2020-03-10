using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : Role
{
    Transform weapon;
    Transform weapon2;
    ItemType weaponType;
    Transform rightHard;
    Transform leftHard;

    public float h;
    public float v;
    float hRaw;
    float vRaw;
    public float moveSpeed;
    //获取英雄所拥有的所有技能
    SkillList skillList;
    //是否能切换武器
    bool canChange;
    //物品快捷栏
    List<Consumables> consumablesList;
    //当前物品CD
    float curItemCD;
    //物品CD
    int ItemCD;
    //翻滚 及 其他移动
    public Vector3 rollDir;
    //获取当前武器的技能CD
    List<SkillData> curSkillList;
    //获取当前使用的技能
    public SkillData curSkill;
    //捡到的物品
    public List<GameObject> takeList;
    //是否能移动
    //public bool canMove;
    private int atkNum;
    //旋转速度
    int rotationSpeed = 14;
    //战斗数据
    FightTmpData fightData;
    //当前攻击力
    public int curATK;
    //当前击退及硬值
    public int curRepel;
    //记录能否旋转
    System.Text.StringBuilder curAniName;

    AudioClip[] sound, Playsound;
    AudioSource voice;
    new void Awake()
    {
        base.Awake();

        sound = new AudioClip[7];
        Playsound = new AudioClip[4];
        sound = Resources.LoadAll<AudioClip>("Voice/combat/Skill");
        Playsound = Resources.LoadAll<AudioClip>("Voice/combat/player");
        voice = GameObject.Find("voice").GetComponent<AudioSource>();
        voice.playOnAwake = false;

    }

    /// <summary>
    /// 播放技能声音
    /// </summary>
    public void Voice()
    {
        if (weaponType == ItemType.LongHandle)//J
        {
            if (oo == 1)
            {
                voice.clip = sound[4];
            }
            else if (oo == 2)
            {
                voice.clip = sound[5];
            }
            else if (oo == 3)
            {
                voice.clip = sound[6];
            }
            voice.Play();
        }
        else if (weaponType == ItemType.SwordShield)//D
        {
            if (oo == 1)
            {
                voice.clip = sound[0];
            }
            else if (oo == 2)
            {
                voice.clip = sound[1];
            }
            else if (oo == 3)
            {
                voice.clip = sound[7];
            }
            voice.Play();
        }
        else if (weaponType == ItemType.Wand)//F
        {
            if (oo == 1)
            {
                voice.clip = sound[7];
            }
            else if (oo == 2)
            {
                voice.clip = sound[2];
            }
            else if (oo == 3)
            {
                voice.clip = sound[3];
            }
            voice.Play();
        }
    }
    public int oo;
    void OnEnable()
    {
        //TODO 要开启
        //Init();
    }
    protected override void Start()
    {
        base.Start();
        Init();
        uifc.BindAttackBtn(AttackTrigger);
        uifc.BindRollkBtn(RollTrigger);
        uifc.BindItem1Btn(Item1Trigger);
        uifc.BindItem2Btn(Item2Trigger);
        uifc.BindItem3Btn(Item3Trigger);
        uifc.BindSkill1Btn(Skill1Trigger);
        uifc.BindSkill2Btn(Skill2Trigger);
        uifc.BindSkill3Btn(Skill3Trigger);
        uifc.BindTakeItemBtn(TakeItem);
        uifc.BindChangeWeapon(ChangeWeaponTrigger);
    }
    public void Init()
    {
        RoleData tempData = HeroManager.Instance.GetCurHeroData();
        roledata = new RoleData(
            tempData.ID,
            tempData.Name,
            tempData.Prefab,
            tempData.HP,
            tempData.BaseExp,
            tempData.LV,
            tempData.Exp,
            tempData.ATK,
            tempData.DEF,
            tempData.Speed,
            tempData.ATKSpeed,
            tempData.Crit,
            tempData.Repel,
            tempData.WeaponId,
            tempData.ArmorId,
            //TODO
            //因为先加载了角色加载器 所以背包管理器是空的 所以拿不到背包里面的东西
            tempData.Weapon,
            tempData.Armor,
            tempData.StrSkillList,
            tempData.SkillList
            );
        //Debug.Log(PackageManager.Instance.GetItem<Equip>(roledata.WeaponId));
        curATK = (int)(roledata.ATK * (roledata.LV / 2 + 0.5f)) + PackageManager.Instance.GetItem<Equip>(roledata.WeaponId).ATK;
        curRepel = roledata.Repel + PackageManager.Instance.GetItem<Equip>(roledata.WeaponId).Repel - 3;
        moveSpeed = roledata.Speed;

        takeList = new List<GameObject>();
        rightHard = transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R");
        leftHard = transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_L/Shoulder_L/Elbow_L/Hand_L");
        weapon = rightHard.GetChild(rightHard.childCount - 1);
        curSkill = null;
        //cc.enabled = true;
        //canMove = true;
        atkNum = 0;
        isDead = false;
        rollDir = Vector3.zero;
        curHp = roledata.HP * roledata.LV;
        ItemCD = -1;
        curItemCD = 0;
        skillList = HeroSkillManager.Instance.GetSkillList();
        weaponType = ItemType.LongHandle;
        curAniName = new System.Text.StringBuilder();
        BindSkillCD();
        //排序技能顺序
        quickSort(curSkillList);

        fightData = FightTmpManager.Instance.GetData();
        //TODO 要改为 roledata.Prefab
        //List<int> aa = Tool.Instance.GetShortcut("1#3#4|4#0#0");
        List<int> aa = Tool.Instance.GetShortcut(roledata.Prefab);
        fightData.sword = PackageManager.Instance.GetItem<Equip>(aa[0]);
        fightData.ss = PackageManager.Instance.GetItem<Equip>(aa[1]);
        fightData.wand = PackageManager.Instance.GetItem<Equip>(aa[2]);
        fightData.consumable1 = PackageManager.Instance.GetItem<Consumables>(aa[3]);
        fightData.consumable2 = PackageManager.Instance.GetItem<Consumables>(aa[4]);
        fightData.consumable3 = PackageManager.Instance.GetItem<Consumables>(aa[5]);

        consumablesList = new List<Consumables>() { fightData.consumable1, fightData.consumable2, fightData.consumable3 };
        BindItemCD();
    }

    void Update()
    {
        if (isDead != true && curHp <= 0)
        {
            ani.SetTrigger("Die");
            //cc.enabled = false;
            isDead = true;
            FightTmpManager.Instance.GetData().isBack = true;
            StartCoroutine(FightChange.Instance.LoadScene());
            return;
        }
        else if (isDead)
        {
            return;
        }
        if (!cc.isGrounded) transform.position -= transform.up * Time.deltaTime;
        CDCheck();
        ItemAni();
        aniInfo = ani.GetCurrentAnimatorStateInfo(0);
        //if (!canMove) return;
        //改变数值可提前行走 但会造成一定程度的滑步
        if (rollDir.magnitude > 0.01f) Roll();
        PlayerUse();
        if (IsBack()) return;
        PlayAction();
        //PlayeMove();
        PlayeMoveOnPhone();
    }

    private void PlayeMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        hRaw = Input.GetAxisRaw("Horizontal");
        vRaw = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        ani.SetFloat("h", h);
        ani.SetFloat("v", v);
        if (hRaw != 0 || vRaw != 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);

            if (!aniInfo.IsName("Move"))
            {
                rotationSpeed = 14 / 2;
            }
            else
            {
                rotationSpeed = 14;
            }

            if (!aniInfo.IsName(curAniName.ToString()))
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed);
            }
        }
        if (!aniInfo.IsName("Move")) return;
        atkNum = 0;
        cc.SimpleMove(dir * Time.deltaTime * moveSpeed);
    }
    private void PlayeMoveOnPhone()
    {
        Vector3 dir = new Vector3(h, 0, v);

        ani.SetFloat("h", h);
        ani.SetFloat("v", v);
        if (h != 0 || v != 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);

            if (!aniInfo.IsName("Move"))
            {
                rotationSpeed = 14 / 2;
            }
            else
            {
                rotationSpeed = 14;
            }

            if (!aniInfo.IsName(curAniName.ToString()))
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed);
            }
        }
        if (!aniInfo.IsName("Move")) return;
        atkNum = 0;
        cc.SimpleMove(dir * Time.deltaTime * moveSpeed);
    }

    private void PlayAction()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            curHp = 99999;
            curATK = 99999;
        }
        //拾取
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeItem();
        }
        //平A
        if (Input.GetKeyDown(KeyCode.J))
        {
            AttackTrigger();
        }
        //翻滚
        else if (Input.GetKeyDown(KeyCode.K))
        {
            RollTrigger();
        }
        //技能1
        else if (Input.GetKeyDown(KeyCode.U))
        {
            Skill1Trigger();
        }
        //技能2
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Skill2Trigger();
        }
        //技能3
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Skill3Trigger();
        }
        //切换长剑
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeWeapon1Trigger();
        }
        //切换剑盾
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeWeapon2Trigger();
        }
        //切换法杖
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeWeapon3Trigger();
        }
    }
    private void PlayerUse()
    {
        //使用道具
        if (curItemCD != 0) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Item1Trigger();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Item2Trigger();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Item3Trigger();
        }
    }

    public void AttackTrigger()
    {
        ani.SetTrigger("Attack");
    }

    public void RollTrigger()
    {
        if (aniInfo.IsName("Move"))
        {
            //             方向        与距离
            rollDir = transform.forward * 9;
            ani.SetTrigger("RollForward");
            voice.clip = Playsound[4];
            voice.pitch = 1f;
            voice.Play();
        }
    }

    public void Skill1Trigger()
    {
        if (curCDList[0] > 0 || !aniInfo.IsName("Move")) return;
        ani.SetTrigger("Skill1");
        oo = 1;
    }
    public void Skill2Trigger()
    {
        if (curCDList.Count < 2 || curCDList[1] > 0 || !aniInfo.IsName("Move")) return;
        ani.SetTrigger("Skill2");
        oo = 2;
    }
    public void Skill3Trigger()
    {
        if (curCDList.Count < 3 || curCDList[2] > 0 || !aniInfo.IsName("Move")) return;
        ani.SetTrigger("Skill3");
        oo = 3;
    }

    public void ChangeWeapon1Trigger()
    {
        if (canChange == false || aniInfo.IsName("Attack") || fightData.sword == null) return;
        weaponType = ItemType.LongHandle;
        roledata.WeaponId = fightData.sword.itemID;
        ChangeWeapon();
    }


    public void ChangeWeapon2Trigger()
    {
        if (canChange == false || aniInfo.IsName("Attack") || fightData.ss == null) return;
        weaponType = ItemType.SwordShield;
        roledata.WeaponId = fightData.ss.itemID;
        ChangeWeapon();
    }


    public void ChangeWeapon3Trigger()
    {
        if (canChange == false || aniInfo.IsName("Attack") || fightData.wand == null) return;
        weaponType = ItemType.Wand;
        roledata.WeaponId = fightData.wand.itemID;
        ChangeWeapon();
    }


    public void Item1Trigger()
    {
        if (curItemCD != 0) return;
        if (consumablesList[0] != null && consumablesList[0].count > 0)
        {
            switch (consumablesList[0].Quality)
            {
                case ItemQuality.N:
                    curHp += 150;
                    break;
                case ItemQuality.R:
                    curHp += 500;
                    break;
                case ItemQuality.SR:
                    curHp += 1000;
                    break;
            }
            uifc.ChangeHP(curHp, roledata.HP * roledata.LV);
            consumablesList[0].count -= 1;
            uifc.ShowItem(0, consumablesList[0]);
            //TODO
            //默认CD为 品质 * 5秒
            ItemCD = (int)consumablesList[0].Quality * 5;
            curItemCD = ItemCD;
        }
    }
    public void Item2Trigger()
    {
        if (curItemCD != 0) return;
        if (consumablesList[1] != null && consumablesList[1].count > 0)
        {
            switch (consumablesList[1].Quality)
            {
                case ItemQuality.N:
                    curATK += 30;
                    break;
                case ItemQuality.R:
                    curATK += 100;
                    break;
                case ItemQuality.SR:
                    curATK += 220;
                    break;
                default:
                    break;
            }
            consumablesList[1].count -= 1;
            uifc.ShowItem(1, consumablesList[1]);
            ItemCD = (int)consumablesList[1].Quality * 5;
            curItemCD = ItemCD;
        }
    }
    public void Item3Trigger()
    {
        if (curItemCD != 0) return;
        if (consumablesList[2] != null && consumablesList[2].count > 0)
        {
            switch (consumablesList[1].Quality)
            {
                case ItemQuality.N:
                    moveSpeed += 10;
                    break;
                case ItemQuality.R:
                    moveSpeed += 30;
                    break;
                case ItemQuality.SR:
                    moveSpeed += 50;
                    break;
                default:
                    break;
            }
            consumablesList[2].count -= 1;
            uifc.ShowItem(2, consumablesList[2]);
            ItemCD = (int)consumablesList[2].Quality * 5;
            curItemCD = ItemCD;
        }
    }

    public void ChangeWeaponTrigger()
    {
        if (canChange == false || aniInfo.IsName("Attack")) return;
        switch (weaponType)
        {
            case ItemType.LongHandle:
                if (fightData.ss != null)
                {
                    weaponType = ItemType.SwordShield;
                    roledata.WeaponId = fightData.ss.itemID;
                    ChangeWeapon();
                }
                else if (fightData.wand != null)
                {
                    weaponType = ItemType.Wand;
                    roledata.WeaponId = fightData.wand.itemID;
                    ChangeWeapon();
                }
                else
                {
                    return;
                }
                break;
            case ItemType.SwordShield:
                if (fightData.wand != null)
                {
                    weaponType = ItemType.Wand;
                    roledata.WeaponId = fightData.wand.itemID;
                    ChangeWeapon();
                }
                else if (fightData.sword != null)
                {
                    weaponType = ItemType.LongHandle;
                    roledata.WeaponId = fightData.sword.itemID;
                    ChangeWeapon();
                }
                else
                {
                    return;
                }
                break;
            case ItemType.Wand:
                if (fightData.sword != null)
                {
                    weaponType = ItemType.LongHandle;
                    roledata.WeaponId = fightData.sword.itemID;
                    ChangeWeapon();
                }
                else if (fightData.ss != null)
                {
                    weaponType = ItemType.SwordShield;
                    roledata.WeaponId = fightData.ss.itemID;
                    ChangeWeapon();
                }
                else
                {
                    return;
                }
                break;
        }

    }

    private void ChangeWeapon()
    {
        if (canChange == false) return;
        Destroy(weapon.gameObject);
        if (weapon2 != null) Destroy(weapon2.gameObject);
        switch (weaponType)
        {
            case ItemType.LongHandle:
                //加载武器及状态机
                weapon = Tool.Instance.InstantiateObj(AllPath.Instance.weaponPrefabsPath + "SM_Wep_GreatSword_02", transform.root).transform;
                weapon.SetParent(rightHard, true);
                weapon.localPosition = new Vector3(6.3f, 4.9f, 0.4f);
                weapon.localEulerAngles = new Vector3(48.854f, 73.55701f, -81.581f);
                ani.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(AllPath.Instance.animatorPath + "HeroSword");

                curATK = (int)(roledata.ATK * (roledata.LV / 2 + 0.5f)) + fightData.sword.ATK;
                curRepel = roledata.Repel + fightData.sword.Repel - 3;
                break;
            case ItemType.SwordShield:
                weapon = Tool.Instance.InstantiateObj(AllPath.Instance.weaponPrefabsPath + "Big Sword 01 Red 1", transform.root).transform;
                weapon.SetParent(rightHard, true);
                weapon.localPosition = new Vector3(6.3f, 4.9f, 0.4f);
                weapon.localEulerAngles = new Vector3(102.9f, 269.2f, -81.581f);
                weapon.localScale = new Vector3(80, 90, 80);
                weapon2 = Tool.Instance.InstantiateObj(AllPath.Instance.weaponPrefabsPath + "Big Shield 02 Red 1", transform.root).transform;
                weapon2.SetParent(leftHard, true);
                weapon2.localPosition = new Vector3(6.3f, -5.8f, 0.4f);
                weapon2.localEulerAngles = new Vector3(282.9f, 269.2f, -81.581f);
                weapon2.localScale = new Vector3(80, 90, 80);
                ani.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(AllPath.Instance.animatorPath + "HeroSS");

                curATK = (int)(roledata.ATK * (roledata.LV / 2 + 0.5f)) + fightData.ss.ATK;
                curRepel = roledata.Repel + fightData.ss.Repel - 3;
                break;
            case ItemType.Wand:
                weapon = Tool.Instance.InstantiateObj(AllPath.Instance.weaponPrefabsPath + "SM_Wep_Goblin_Staff_01", transform.root).transform;
                weapon.SetParent(rightHard, true);
                weapon.localPosition = new Vector3(6.3f, 4.9f, 0.4f);
                weapon.localEulerAngles = new Vector3(48.854f, 73.55701f, -81.581f);
                ani.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(AllPath.Instance.animatorPath + "HeroWand");

                curATK = (int)(roledata.ATK * (roledata.LV / 2 + 0.5f)) + fightData.wand.ATK;
                curRepel = roledata.Repel + fightData.wand.Repel - 3;
                break;
        }
        BindSkillCD();
    }
    private void Attack()
    {
        if (weaponType == ItemType.Wand)
        {
            MageAtk mage = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + "fashi_ATk", transform.position + transform.up, transform.rotation, transform.parent).GetComponent<MageAtk>();
            mage.callBack = MageAtkEvent;
        }
        else
        {
            rollDir = transform.forward * 4;
            Roll(20);
            int tmpAtk = curATK;
            switch (atkNum)
            {
                case 1:
                    tmpAtk = (int)(tmpAtk * 1.2f);
                    break;
                case 2:
                    tmpAtk = (int)(tmpAtk * 1.3f);
                    break;
            }
            // 距离
            float radius = 2;
            // 角度
            int angle = 85;
            Collider[] cols = Physics.OverlapSphere(transform.position, radius, 1 << LayerMask.NameToLayer(LayerType.enemy.ToString()));
            for (int i = 0; i < cols.Length; i++)
            {
                //距离
                float dis = Vector3.Distance(transform.position, cols[i].transform.position);
                //英雄正前方
                Vector3 dir1 = transform.forward;
                //英雄指向目标的向量
                Vector3 dir2 = cols[i].transform.position - transform.position;
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
                    Vector3 v3 = cols[i].transform.position - transform.position;
                    cols[i].GetComponent<Enemy>().Hurt(v3.normalized * (curRepel / 10), tmpAtk, true);
                }
            }
            voice.clip = Playsound[3];
            voice.pitch = 1f;
            voice.Play();
            atkNum++;
        }
        if (aniInfo.IsName("Attack"))
        {
            curAniName.Remove(0, curAniName.Length);
            curAniName.Append("Attack");
        }
        else if (aniInfo.IsName("HeroAtk2"))
        {
            curAniName.Remove(0, curAniName.Length);
            curAniName.Append("HeroAtk2");
        }
        else if (aniInfo.IsName("HeroAtk3"))
        {
            curAniName.Remove(0, curAniName.Length);
            curAniName.Append("HeroAtk3");
        }
    }

    private void MageAtkEvent(GameObject enemy)
    {
        //击退
        Vector3 v3 = enemy.transform.position - transform.position;
        enemy.GetComponent<Enemy>().Hurt(v3.normalized * (curRepel / 10), curATK, true);
    }

    private void Skill1()
    {
        curCDList[0] = curSkillList[0].CD;
        curSkill = curSkillList[0];
        curSkillList[0].DelegateFunc(transform, curSkillList[0].DelegateParameter);
        Voice();
    }
    private void Skill2()
    {
        curCDList[1] = curSkillList[1].CD;
        curSkill = curSkillList[1];
        curSkillList[1].DelegateFunc(transform, curSkillList[1].DelegateParameter);
        Voice();
    }
    private void Skill3()
    {
        curCDList[2] = curSkillList[2].CD;
        curSkill = curSkillList[2];
        curSkillList[2].DelegateFunc(transform, curSkillList[2].DelegateParameter);
        Voice();
    }

    //绑定技能CD
    private void BindSkillCD()
    {
        curCDList = new List<float>();
        curSkillList = new List<SkillData>();

        //根据首数字添加对应的技能CD槽
        for (int i = 0; i < skillList.skillList.Count; i++)
        {
            if (skillList.skillList[i].LV != 0 && (int)(skillList.skillList[i].ID / 10000) == (int)weaponType)
            {
                //需要初始CD值就换成skillList.skillList[i].CD
                //创建相同数量的技能栏
                curCDList.Add(0);
                //获取对应的技能CD
                curSkillList.Add(skillList.skillList[i]);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (i >= curSkillList.Count)
            {
                uifc.ShowSkill(i, "UI/Sprites/Ui_guanbi");
            }
            else
            {
                uifc.ShowSkill(i, curSkillList[i].Icon);
            }
        }
    }
    //绑定物品CD
    private void BindItemCD()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i >= consumablesList.Count)
            {
                uifc.ShowItem(i, null);
            }
            else
            {
                uifc.ShowItem(i, consumablesList[i]);
            }
        }
    }

    private void HPCheck()
    {
    }
    //CD检测
    private void CDCheck()
    {
        bool isGone = true;
        for (int i = 0; i < curCDList.Count; i++)
        {
            if (curCDList[i] < 0)
            {
                curCDList[i] = 0;
            }
            else if (curCDList[i] > 0)
            {
                isGone = false;
                curCDList[i] -= Time.deltaTime;
            }
            uifc.SkillCD(curCDList[i], curSkillList[i].CD, i);
        }
        canChange = isGone;
        if (curItemCD < 0)
        {
            curItemCD = 0;
        }
        else if (curItemCD > 0)
        {
            curItemCD -= Time.deltaTime;
        }
        if (ItemCD != -1)
        {
            uifc.ItemCD(curItemCD, ItemCD);
        }

    }
    public void TakeItem()
    {
        // 距离
        float radius = 4;
        // 角度
        int angle = 180;
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, 1 << LayerMask.NameToLayer(LayerType.drop.ToString()));
        for (int i = 0; i < cols.Length; i++)
        {
            Vector3 dir1 = transform.forward;
            Vector3 dir2 = cols[i].transform.position - transform.position;
            float dot = Vector3.Dot(dir1, dir2.normalized);
            float deg = Mathf.Acos(dot) * Mathf.Rad2Deg;
            Vector3 cross = Vector3.Cross(dir2, dir1);
            if ((cross.y >= 0 && deg <= angle || cross.y <= 0 && deg <= angle) && Vector3.Distance(transform.position, cols[i].transform.position) <= radius)
            {
                takeList.Add(cols[i].gameObject);
            }
        }
    }

    private void ItemAni()
    {
        if (takeList.Count == 0) return;
        for (int i = 0; i < takeList.Count; i++)
        {
            takeList[i].transform.position = Vector3.Lerp(takeList[i].transform.position, transform.position + transform.up, 0.1f);
            if (Vector3.Distance(takeList[i].transform.position, transform.position + transform.up) < 0.1f)
            {
                ItemQuality quality = (ItemQuality)System.Enum.Parse(typeof(ItemQuality), takeList[i].gameObject.name);
                FightTmpData getData = FightTmpManager.Instance.GetData();
                switch (quality)
                {
                    case ItemQuality.N:
                        getData.nNum += 1;
                        break;
                    case ItemQuality.R:
                        getData.rNum += 1;
                        break;
                    case ItemQuality.SR:
                        getData.srNum += 1;
                        break;
                    case ItemQuality.SSR:
                        getData.ssrNum += 1;
                        break;
                }
                Pool.Instance.RecyleObj(takeList[i].gameObject, "Item");
                takeList.RemoveAt(i);
            }
        }
    }

    private void Dead()
    { }

    public void Roll(int attenuation = 10)
    {
        cc.SimpleMove(rollDir);
        //衰减值 attenuation
        //方向 rollDir
        //模长
        float length = rollDir.magnitude;
        //限制最大最小值
        length = Mathf.Clamp(length - Time.deltaTime * attenuation, 0, length);
        //总方向
        rollDir = rollDir.normalized * length;
        return;
    }
    #region 临时用快排
    public void quickSort(List<SkillData> array)
    {
        quickSort(array, 0, array.Count - 1);
    }

    private void quickSort(List<SkillData> array, int left, int right)
    {
        if (array == null || left >= right || array.Count <= 1)
        {
            return;
        }
        int mid = partition(array, left, right);
        quickSort(array, left, mid);
        quickSort(array, mid + 1, right);
    }

    private int partition(List<SkillData> array, int left, int right)
    {
        SkillData temp = array[left];
        while (right < left)
        {
            // 先判断基准数和后面的数依次比较
            while (temp.CD <= array[right].CD && left < right)
            {
                --right;
            }
            // 当基准数大于了 arr[left]，则填坑
            if (left < right)
            {
                array[left] = array[right];
                ++left;
            }
            // 现在是 arr[right] 需要填坑了
            while (temp.CD >= array[left].CD && left < right)
            {
                ++left;
            }
            if (left < right)
            {
                array[right] = array[left];
                --right;
            }
        }
        array[left] = temp;
        return left;
    }
    #endregion
    //public void HeroSSSkill3CallBack()
    //{
    //    canMove = true;
    //}




}
