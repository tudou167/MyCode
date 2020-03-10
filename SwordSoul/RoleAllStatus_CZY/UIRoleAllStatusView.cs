using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoleAllStatusView : MonoBehaviour {

    //TODO
    //private Text icon;
    private Text id;
    private Text userName;
    private Text atk;
    private Text def;
    private Text crit;
    private Text lv;
    private Text expText;
    private Text hpText;

    private Slider exp;
    private Slider hp;


    public void Awake()
    {
        //icon = transform.Find("Icon").GetComponent<Text>();
        id = transform.Find("Property/property/ID/Text (7)").GetComponent<Text>();
        userName = transform.Find("Property/property/Nickname/Text (7)").GetComponent<Text>();
        atk = transform.Find("Property/property/Atk/Text (7)").GetComponent<Text>();
        def = transform.Find("Property/property/defense/Text (7)").GetComponent<Text>();
        crit = transform.Find("Property/property/critical strike/Text (7)").GetComponent<Text>();
        lv = transform.Find("Property/property/class/Text (7)").GetComponent<Text>();
        expText = transform.Find("Property/property/exp").GetComponent<Text>();
        hpText = transform.Find("Property/property/HPText").GetComponent<Text>();

        exp = transform.Find("Property/property/Experience").GetComponent<Slider>();
        hp = transform.Find("Property/property/HP").GetComponent<Slider>();
    }

    public void Refresh(List<EquipmentTemp.Row.Row2> data)
    {

        CharacterInfo userData = PlayerModel.Instance._characterInfo;// UIMainMenRoleCountView.Instance.DisplayCharacterInfo();

        //TODO
        id.text = userData.id.ToString();
        userName.text = userData.name;
        atk.text = userData.atk.ToString();
        def.text = userData.def.ToString();
        crit.text = userData.crit.ToString();

        int[] temp = AllFormula.Instance.GetExpInfo(PlayerDataInfoView.Instance.DisplayInfo().exp);

        lv.text = temp[2].ToString();


        expText.text = "当前经验：<Color=yellow>" + temp[0] + "</Color>点 升级需要<Color=yellow>" + temp[1] + "</Color>点";
        hpText.text = "生命值：<Color=yellow>" + userData.hp + "</Color>/" + userData.maxHp + "";

        exp.value = temp[0] / temp[3];
        hp.value = userData.hp / userData.maxHp;

        for (int i = 0; i < UIRoleAllStatusController.Instance.equipTemp.childCount; i++)
        {
            for (int j = 0; j < data.Count; j++)
            {
                UIRoleAllStatusController.Instance.equipTemp.GetChild(i).GetComponent<Image>().sprite = null;
                if (UIRoleAllStatusController.Instance.equipTemp.GetChild(i).name == ((int)data[j].itemType).ToString() && data[j].isWear == 1)
                {
                    UIRoleAllStatusController.Instance.equipTemp.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    UIRoleAllStatusController.Instance.equipTemp.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(AllPaths.Instance.allIconPath + data[j].eid);
                    break;
                }
                else
                {
                    UIRoleAllStatusController.Instance.equipTemp.GetChild(i).GetComponent<Image>().color = new Color(0,0,0,0) ;
                }

            }
        }
    }

}
