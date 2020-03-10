using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScene : MonoBehaviour
{
    List<Transform> enemyTfList;
    Transform heroTf;
    public float curTime;
    public float rebirthTime;
    public int deadLimit;
    public int rebirthNum;
    public int curDeadNum;
    public bool isEnd;
    FightTmpData data;
    List<RoleData> roleList;
    int roleLv;
    int roleType;
    System.Text.StringBuilder path;
    void Awake()
    {
        FightChange.Instance.FightStart(transform.parent);

        heroTf = GameObject.Find("/Herobirth").transform;
        enemyTfList = new List<Transform>();
        FindRebirth();
        curTime = 0;
        //生成间隔
        rebirthTime = 2;
        //生成数量 和 通过当前层条件
        deadLimit = 5;
        curDeadNum = 0;
        rebirthNum = 0;
        isEnd = false;
    }
    void Start()
    {
        HeroManager.Instance.GetCurHero().transform.position = heroTf.position;
        data = FightTmpManager.Instance.GetData();
        data.isEnd = false;
        GameObject.Find("/UI/Canvas/PhoneFightUI").GetComponent<UIFightController>().ChangePile();

        roleList = new List<RoleData>();
        List<RoleData> tmpList = RoleManager.Instance.GetAllRole();
        roleLv = 0;
        roleType = 1;

        path = new System.Text.StringBuilder();
        if (data.curPlieNum == 4)
        {
            //精英层
            roleLv = 50;
            roleType = 2;
            deadLimit = 3;
        }
        else if (data.curPlieNum == 9)
        {
            //精英层
            roleLv = 50;
            roleType = 2;
            deadLimit = 6;
        }
        else if (data.curPlieNum == 14)
        {
            //Boss层
            roleLv = 60;
            roleType = 3;
            deadLimit = 1;
        }
        else if (data.curPlieNum < 5)
        {
            roleLv = 20;
            roleType = 1;
        }
        else if (data.curPlieNum < 10)
        {
            roleLv = 30;
            roleType = 1;
        }
        else if (data.curPlieNum < 15)
        {
            roleLv = 40;
            roleType = 1;
        }

        int min = 0;
        int max = 0;
        switch (roleType)
        {
            case 1:
                min = 20000;
                max = 30000;
                path.Append("XiaoGuai/");
                break;
            case 2:
                min = 30000;
                max = 40000;
                path.Append("JYG/");
                break;
            case 3:
                min = 40000;
                max = 50000;
                path.Append("Boss/");
                break;
        }
        for (int i = 0; i < tmpList.Count; i++)
        {
            if (tmpList[i].ID > min && tmpList[i].ID < max && tmpList[i].LV <= roleLv)
            {
                roleList.Add(tmpList[i]);
            }
        }

        //HeroManager.Instance.GetCurHero().GetComponent<Hero>().curHp = HeroManager.Instance.GetCurHeroData().HP;
    }

    private void FindRebirth(int index = 1)
    {
        GameObject go = GameObject.Find("/Rebirth" + index);
        if (go == null)
        {
            return;
        }
        enemyTfList.Add(go.transform);
        index++;
        FindRebirth(index);
    }

    void Update()
    {
        if (isEnd == false && curDeadNum >= deadLimit)
        {
            data.curPlieNum += 1;
            data.isEnd = true;
            FightChange.Instance.FightEnd();
            isEnd = true;
            return;
        }
        if (rebirthNum >= deadLimit) return;
        curTime += Time.deltaTime;
        if (curTime > rebirthTime)
        {
            rebirthNum++;
            GetRole();
            curTime = 0;
        }
    }

    private void GetRole()
    {
        int rebirthIndex = Random.Range(0, enemyTfList.Count);
        if (roleType == 3)
        {
            rebirthIndex = 4;
        }
        RoleData tmp = roleList[Random.Range(0, roleList.Count)];
        Enemy enemy = Pool.Instance.GetObj(AllPath.Instance.rolePrefabsPath + path + tmp.ID, enemyTfList[rebirthIndex].position, Quaternion.identity, transform).GetComponent<Enemy>();
        enemy.roledata = tmp;
        enemy.curHp = enemy.roledata.HP;
        enemy.deadcallback = DeadEvent;

    }

    private void DeadEvent(RoleData roleData, RoleType roleType, Vector3 pos)
    {
        curDeadNum++;
        data = FightTmpManager.Instance.GetData();
        //掉落
        int dropPoint = Random.Range(1, 101);
        if (dropPoint > 50)
        {
            switch (roleType)
            {
                case RoleType.xiaoguai:
                    Pool.Instance.GetObj(AllPath.Instance.dropPrefabsPath + ItemQuality.N.ToString(), pos, Quaternion.identity, transform.parent);
                    break;
                case RoleType.jyg:
                    Pool.Instance.GetObj(AllPath.Instance.dropPrefabsPath + ItemQuality.R.ToString(), pos, Quaternion.identity, transform.parent);
                    break;
                case RoleType.boss:
                    if (Random.Range(1, 101) > 50)
                    {
                        Pool.Instance.GetObj(AllPath.Instance.dropPrefabsPath + ItemQuality.SR.ToString(), pos, Quaternion.identity, transform.parent);
                    }
                    else
                    {
                        Pool.Instance.GetObj(AllPath.Instance.dropPrefabsPath + ItemQuality.SSR.ToString(), pos, Quaternion.identity, transform.parent);
                    }
                    break;
            }
        }
        data.gold += roleData.LV * 10;
        data.exp += roleData.Exp;
    }
}
