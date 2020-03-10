using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIPackageController : MonoSingletion<UIPackageController> {
    public UIItemDetailsView idv;
    public UIPackageListView plv;

    //public Dropdown dropdown;
    //private Button[] BtnArr;
    public override void Awake()
    {
        base.Awake();
       // dropdown = transform.Find("Dropdown").GetComponent<Dropdown>();
    }
    void Start()
    {
        Debug.Log(Application.persistentDataPath);

        //TODO 拖进去的话会获取不了 和 配置不了各种 组件与数据
        //BtnArr = transform.Find("Scroll View").GetComponentsInChildren<Button>(true);
        //for (int i = 0; i < BtnArr.Length; i++)
        //{
        //    BtnArr[i].onClick.AddListener(() => { ItemClick(); });
        //}
        Init();
        
    }

    public void Init()
    {
        plv = transform.GetComponentInChildren<UIPackageListView>();
        idv = transform.GetComponentInChildren<UIItemDetailsView>();
        List<EquipmentTemp.Row.Row2> data = UIPackageModel.Instance.LoadUserPackageData();

        plv.Refresh(data);
        if (data.Count == 0)
        {
            AllTools.Instance.HiddenAllChild(idv.transform);
            return;
        }
        else
        {
            AllTools.Instance.ShowAllChild(idv.transform);
        }
        idv.RefreshDetails(data[0]);
    }

    public void SortClick()
    {
        int value = int.Parse(EventSystem.current.currentSelectedGameObject.transform.name) ;

        //TODO
        List<EquipmentTemp.Row.Row2> data = UIPackageModel.Instance.LoadUserPackageData();
        if (data.Count == 0)
        {
            return;
        }
       data = UIPackageModel.Instance.Sort(data,value);

        plv.Refresh(data);
        idv.RefreshDetails(data[0]);
    }
    public void SellClick()
    {
        List<EquipmentTemp.Row.Row2> data = UIPackageModel.Instance.Sell(idv._Data);
        plv.Refresh(data);

        if (data.Count == 0)
        {
            AllTools.Instance.HiddenAllChild(idv.transform);
            return;
        }
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].eid == idv._Data.eid)
            {
               idv.RefreshDetails(data[i]);
                return;
            }
        }
        idv.RefreshDetails(data[0]);

    }
    public void ItemClick()
    {
        //TODO
        //UIItemDetailsView idv = transform.GetComponentInChildren<UIItemDetailsView>();
        idv.RefreshDetails(EventSystem.current.currentSelectedGameObject.GetComponent<UIItemView>()._Data);
    }



}

