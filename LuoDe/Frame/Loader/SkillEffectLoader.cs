using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectLoader : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SkillEffectManager.Instance.AddSkillEffect("重刺", HeroSwordSkill1);
        SkillEffectManager.Instance.AddSkillEffect("大地震颤", HeroSwordSkill2);
        SkillEffectManager.Instance.AddSkillEffect("旋风斩", HeroSwordSkill3);
        SkillEffectManager.Instance.AddSkillEffect("盾击", HeroSSSkill1);
        SkillEffectManager.Instance.AddSkillEffect("突袭", HeroSSSkill2);
        SkillEffectManager.Instance.AddSkillEffect("自然治愈", HeroSSSkill3);
        SkillEffectManager.Instance.AddSkillEffect("疾风步", HeroWandSkill1);
        SkillEffectManager.Instance.AddSkillEffect("空爆术", HeroWandSkill2);
        SkillEffectManager.Instance.AddSkillEffect("炎柱", HeroWandSkill3);
        SkillEffectManager.Instance.AddSkillEffect("重砸", BossSkill1);
        SkillEffectManager.Instance.AddSkillEffect("月黑天冲", BossSkill2);

        SkillEffectManager.Instance.AddSkillEffect("小血瓶", HpItem);
        SkillEffectManager.Instance.AddSkillEffect("中血瓶", HpItem);
        SkillEffectManager.Instance.AddSkillEffect("大血瓶", HpItem);

        SkillEffectManager.Instance.AddSkillEffect("速度卷轴I", SpeedItem);
        SkillEffectManager.Instance.AddSkillEffect("速度卷轴II", SpeedItem);
        SkillEffectManager.Instance.AddSkillEffect("速度卷轴III", SpeedItem);

        SkillEffectManager.Instance.AddSkillEffect("攻击卷轴I", AtkItem);
        SkillEffectManager.Instance.AddSkillEffect("攻击卷轴II", AtkItem);
        SkillEffectManager.Instance.AddSkillEffect("攻击卷轴III", AtkItem);
    }

    public void HeroSwordSkill1(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position + tf.forward, tf.rotation, null);
        EffectController efc = go.GetComponent<EffectController>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recyleTime"></param>
        /// <param name="range"></param>
        /// <param name="angleNum"></param>
        /// <param name="damage"></param>
        /// <param name="repel"></param>
        efc.data = new EffectData(2, 4, 40,
                (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
    public void HeroSwordSkill2(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position + tf.forward * 3, tf.rotation, null);
        EffectController efc = go.GetComponent<EffectController>();
        efc.data = new EffectData(3, 4, 180,
                (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
    public void HeroSwordSkill3(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position, tf.rotation, null);
        EffectController efc = go.GetComponent<EffectController>();
        efc.data = new EffectData(3, 4, 180,
                (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
    public void HeroSSSkill1(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position + tf.up + tf.forward * 0.5f, tf.rotation, null);
        EffectController efc = go.GetComponent<EffectController>();
        efc.data = new EffectData(2, 4, 50,
                (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
    public void HeroSSSkill2(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        //移动方式使用翻滚的函数
        hero.rollDir = tf.forward * 10;
        hero.Roll();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position + tf.up + tf.forward * 2, tf.rotation, tf.root);
        EffectController efc = go.GetComponent<EffectController>();
        efc.data = new EffectData(3, 4f, 85,
                (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
    public void HeroSSSkill3(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position + tf.up * 2, tf.rotation, tf);
        EffectController efc = go.GetComponent<EffectController>();
        efc.data = new EffectData(6, 1f, 1,
                (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
    public void HeroWandSkill1(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position, tf.rotation, tf);
        go.transform.eulerAngles = new Vector3(-90,0,0);
        SpeedBuff efc = go.GetComponent<SpeedBuff>();
        efc.recycleTime = 3 * hero.curSkill.LV;
        efc.ratio = 1 + 0.2f * hero.curSkill.LV;
    }
    public void HeroWandSkill2(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        int num = 1;
        while (num <= hero.curSkill.LV * 2)
        {
            GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position + tf.forward * num * 2, tf.rotation, null);
            EffectController efc = go.GetComponent<EffectController>();
            efc.data = new EffectData(3, 3, 180,
                    (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                    (int)data["Parameter3"] / 10
                );
            num++;
        }
    }
    public void HeroWandSkill3(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + hero.curSkill.ID, tf.position + tf.forward, tf.rotation, null);
        EffectController efc = go.GetComponent<EffectController>();
        efc.data = new EffectData(5, 4, 180,
                (int)data["Parameter1"] + hero.curSkill.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
    public void BossSkill1(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Boss1 enemy = tf.GetComponent<Boss1>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + enemy.roledata.ID, tf.position + tf.forward, tf.rotation, null);
        EnemyEffectController efc = go.GetComponent<EnemyEffectController>();
        efc.data = new EffectData(5, 4, 180,
                (int)data["Parameter1"] + enemy.roledata.LV * (int)data["Parameter2"],
                (int)data["Parameter3"] / 10
            );
    }
        
    public void BossSkill2(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Boss2 enemy = tf.GetComponent<Boss2>();
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.effectPrefabsPath + enemy.roledata.ID, tf.position + tf.forward, tf.rotation, null);
        go.GetComponent<EnemyMageAtk>().callBack = enemy.MageAtkEvent;
    }
    public void HpItem(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        hero.curHp += (int)data["Parameter1"];
    }
    public void AtkItem(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        hero.curATK += (int)data["Parameter1"];
    }
    public void SpeedItem(Transform tf, string strJson)
    {
        JsonData data = JsonMapper.ToObject(strJson);
        Hero hero = tf.GetComponent<Hero>();
        hero.moveSpeed += (int)data["Parameter1"];
    }

}
