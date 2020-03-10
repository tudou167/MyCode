using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDetailsView : MonoBehaviour {

    private Image icon;
    private Text itemName;
    private Text price;
    private Text description;
    private Text quality;
    private Text atk;
    private Text def;
    private Text hp;
    private Text crit;
    private Text lv;

    public EquipmentTemp.Row.Row2 _Data;
    void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        itemName = transform.Find("Name").GetComponent<Text>();
        price = transform.Find("Price").GetComponent<Text>();
        description = transform.Find("Description").GetComponent<Text>();
        quality = transform.Find("Quality").GetComponent<Text>();
        atk = transform.Find("Atk").GetComponent<Text>();
        def = transform.Find("Def").GetComponent<Text>();
        hp = transform.Find("Hp").GetComponent<Text>();
        crit = transform.Find("Crit").GetComponent<Text>();
        lv = transform.Find("Lv").GetComponent<Text>();
    }
    void Start()
    {

    }
    public void RefreshDetails(EquipmentTemp.Row.Row2 data)
    {
        //TODO
        //Image icon = transform.Find("Icon").GetComponent<Image>();
        //Text itemName = transform.Find("Name").GetComponent<Text>();
        //Text price = transform.Find("Price").GetComponent<Text>();
        //Text description = transform.Find("Description").GetComponent<Text>();

        _Data = data;
        GameObject InfBtn = transform.Find("Intensify#Button").gameObject;
        if ((int)data.itemType >= 8)
        {
            InfBtn.SetActive(false);
        }
        else
        {
            InfBtn.SetActive(true);
        }
        icon.sprite = Resources.Load<Sprite>(AllPaths.Instance.allIconPath + data.eid);
        itemName.text = data.name;
        price.text =  "出售可以获得<Color=yellow>" + AllFormula.Instance.SellPrice(data.buy, data.InitialLevel).ToString() +  "</Color>金币";
        description.text = data.description;
        quality.text = "品质：" + data.quality.ToString();
        atk.text = "攻击力：" + data.atk.ToString();
        def.text = "防御力：" + data.def.ToString();
        hp.text = "生命力：" + data.hp.ToString();
        crit.text = "暴击率：" + data.crit.ToString() + "%";
        lv.text = "等级：" + data.InitialLevel.ToString();
    }


}
