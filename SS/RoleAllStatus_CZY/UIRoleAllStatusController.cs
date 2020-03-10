using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoleAllStatusController : MonoSingletion<UIRoleAllStatusController>
{

    UIRoleAllStatusView rasv;
    public Transform equipTemp;
    public Transform rasc;

    //TODO 东西放里面会获取不到
    public override void Awake()
    {
        base.Awake();
        equipTemp = transform.Find("Equip");
        rasv = transform.GetComponent<UIRoleAllStatusView>();
        rasc = transform;
    }
    void Start()
    {
        //TODO
        Init();

    }
    public void Init()
    {
        List<EquipmentTemp.Row.Row2> data = UIPackageModel.Instance.LoadUserPackageData();
        rasv.Refresh(data);
    }

    public RectTransform Wear(EquipmentTemp.Row.Row2 data)
    {
        for (int i = 0; i < equipTemp.childCount; i++)
        {
            if (equipTemp.GetChild(i).transform.name == ((int)data.itemType).ToString())
            {
                return equipTemp.GetChild(i).transform as RectTransform;
            }
        }

        return null;

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
