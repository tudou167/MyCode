using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIFightController : MonoBehaviour
{
    UIFightView uifv;
    private Button configBtn;
    void Awake()
    {
        uifv = GetComponent<UIFightView>();
        configBtn = transform.Find("ConfigBtn").GetComponent<Button>();
        configBtn.onClick.AddListener(GoBack);
    }
    void Start()
    {
    }

    private void GoBack()
    {
        FightTmpData data = FightTmpManager.Instance.GetData();
        if (FightTmpManager.Instance.GetData().isEnd)
        {
            FightChange.Instance.FightEnd();
        }
        else
        {
            FightChange.Instance.FightOver();
        }
    }

    public void ChangePile()
    {
        uifv.ChangePile();
    }
    public void ShowDamage(Transform target, int damage, Transform parent)
    {
        StartCoroutine(uifv.ShowDamage(target, damage.ToString(), parent));
    }
    public void SkillCD(float cd, int skillCD, int index)
    {
        string final = cd.ToString();
        uifv.SkillCD(final.Substring(0, final.IndexOf('.') + 2), cd / skillCD, index);
    }
    public void ItemCD(float cd, int itemCD)
    {
        string final = cd.ToString();
        uifv.ItemCD(final.Substring(0, final.IndexOf('.') + 2), cd / itemCD);
    }
    public void ChangeHP(int curHP, int MaxHP)
    {
        uifv.ChangeHP((float)curHP / (float)MaxHP);
    }

    //刷新快捷栏物品信息
    public void ShowItem(int index, Consumables data)
    {
        uifv.ShowConsumables(index, data);
    }

    public void ShowSkill(int index, string path)
    {
        //TODO换成动态
        //(itemTfList[index][2] as Image).sprite = Resources.Load<Sprite>(data.Icon);
        uifv.ShowSkill(index, path);
    }

    public void BindAttackBtn(UnityAction callback)
    {
        transform.Find("Attack/Image").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindRollkBtn(UnityAction callback)
    {
        transform.Find("Roll/Image").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindSkill1Btn(UnityAction callback)
    {
        transform.Find("SkillList/Skill1/Icon").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindSkill2Btn(UnityAction callback)
    {
        transform.Find("SkillList/Skill2/Icon").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindSkill3Btn(UnityAction callback)
    {
        transform.Find("SkillList/Skill3/Icon").GetComponent<Button>().onClick.AddListener(callback);
    }

    public void BindItem1Btn(UnityAction callback)
    {
        transform.Find("ItemList/Item1/Button").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindItem2Btn(UnityAction callback)
    {
        transform.Find("ItemList/Item2/Button").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindItem3Btn(UnityAction callback)
    {
        transform.Find("ItemList/Item3/Button").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindTakeItemBtn(UnityAction callback)
    {
        transform.Find("TakeItem/Image").GetComponent<Button>().onClick.AddListener(callback);
    }
    public void BindChangeWeapon(UnityAction callback)
    {
        transform.Find("ChangeWeapon/Image").GetComponent<Button>().onClick.AddListener(callback);
    }

}
