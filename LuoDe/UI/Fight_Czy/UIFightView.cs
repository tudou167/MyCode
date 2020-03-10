using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFightView : MonoBehaviour
{
    private Image hp;
    private List<MaskableGraphic[]> itemTfList;
    private List<MaskableGraphic[]> skillTfList;
    private Text PlieShow;

    void Awake()
    {
        hp = transform.Find("HPImage/hp").GetComponent<Image>();
        itemTfList = new List<MaskableGraphic[]>();
        skillTfList = new List<MaskableGraphic[]>();
        Transform tmp1 = transform.Find("ItemList");
        PlieShow = transform.Find("PileShow").GetComponent<Text>();
        for (int i = 0; i < tmp1.childCount; i++)
        {
            itemTfList.Add(new MaskableGraphic[] { tmp1.GetChild(i).Find("Mask").GetComponent<Image>(), tmp1.GetChild(i).Find("CD").GetComponent<Text>(), tmp1.GetChild(i).Find("Icon").GetComponent<Image>(), tmp1.GetChild(i).Find("Count").GetComponent<Text>() });
        }

        Transform tmp2 = transform.Find("SkillList");
        for (int i = 0; i < tmp2.childCount; i++)
        {
            skillTfList.Add(new MaskableGraphic[] { tmp2.GetChild(i).Find("Mask").GetComponent<Image>(), tmp2.GetChild(i).Find("CD").GetComponent<Text>(), tmp2.GetChild(i).Find("Icon").GetComponent<Image>() });
        }
    }

    void Start()
    {
    }

    public void ChangePile()
    {
        PlieShow.text = "<color=red>地下" + (FightTmpManager.Instance.GetData().curPlieNum + 1).ToString() + "层</color>";
    }


    public IEnumerator ShowDamage(Transform target, string num, Transform parent)
    {
        GameObject go = Pool.Instance.GetObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/FightNum", target.position + target.up * 2, Quaternion.identity, parent);
        go.GetComponent<Text>().text = num;
        //TODO 缺 DoTween动画
        go.transform.localScale = Vector3.one;
        go.transform.position = target.position + target.up * 2;
        go.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(1);
        Pool.Instance.RecyleObj(go, "UI");
    }

    public void SkillCD(string cd, float scale, int index)
    {
        (skillTfList[index][0] as Image).fillAmount = scale;
        (skillTfList[index][1] as Text).text = cd;
    }
    public void ShowSkill(int index, string path)
    {
        //TODO换成动态
        //(itemTfList[index][2] as Image).sprite = Resources.Load<Sprite>(data.Icon);
        (skillTfList[index][2] as Image).sprite = Resources.Load<Sprite>(path);
    }
    public void ItemCD(string cd, float scale)
    {
        for (int i = 0; i < itemTfList.Count; i++)
        {
            (itemTfList[i][0] as Image).fillAmount = scale;
            (itemTfList[i][1] as Text).text = cd;
        }
    }

    public void ChangeHP(float scale)
    {
        hp.fillAmount = scale;
    }

    public void ShowConsumables(int index, Consumables data)
    {
        (itemTfList[index][2] as Image).sprite = Resources.Load<Sprite>(data == null ? "UI/Sprites/Ui_guanbi" : data.Icon);
        (itemTfList[index][3] as Text).text = "<b>" + (data == null ? "0" : data.count.ToString()) + "</b>";
    }
}
