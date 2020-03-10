using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntensifyView : MonoSingletion<UIIntensifyView>
{
    Text tips;

    public override void Awake()
    {
        base.Awake();
        tips =  transform.Find("Tips").GetComponent<Text>();
    }
    void Strat()
    {
        Init();
    }

    public void Init()
    {

        EquipmentTemp.Row.Row2 data = UIPackageController.Instance.idv._Data;

        int need = data.InitialLevel;
        if (need >= 5)
        {
            need = 5;
        }

        List<EquipmentTemp.Row.Row2> userPackage = AllToObject.Instance.GetAllUserPackge<EquipmentTemp>().User[UIPackageModel.Instance.userID].Data;
        EquipmentTemp.Row.Row2 temp = null;
        for (int i = 0; i < userPackage.Count; i++)
        {
            if ((int)userPackage[i].itemType == 8 && (int)userPackage[i].quality == (int)data.quality)
            {
                temp = userPackage[i];
            }
            
        }

        if (temp == null)
        {
            List<EquipmentTemp.Row.Row2> itemList = AllToObject.Instance.GetItemList();
            for (int j = 0; j < itemList.Count; j++)
            {
                if ((int)itemList[j].itemType == 8 && (int)itemList[j].quality == (int)data.quality)
                {
                    temp = itemList[j];
                    temp.count = 0;
                }
            }
        }
        tips.text = "当前等级："+ data.InitialLevel +"强化需要："+ need +"颗 "+ temp.name + " 现有："+ temp.count + "颗";
    }

    public void Yes()
    {
        EquipmentTemp.Row.Row2 data = UIPackageModel.Instance.Intensify(UIPackageController.Instance.idv._Data);
        UIPackageController.Instance.plv.Refresh(UIPackageModel.Instance.LoadUserPackageData());
        UIPackageController.Instance.idv.RefreshDetails(data);
        Init();
    }
    public void No()
    {
        Destroy(gameObject);
    }
}
