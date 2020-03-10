using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPackageListView : MonoBehaviour {

    private Transform content;

    void Awake()
    {
        content = transform.Find("Viewport/Content");
    }

    void Start()
    {
    }


    public void Refresh(List<EquipmentTemp.Row.Row2> data)
    {
        AllTools.Instance.DeleteAllChild(content);
        
        for (int i = 0; i < data.Count; i++)
        {
            GameObject cell = AllTools.Instance.LoadCell(AllPaths.Instance.packagePrefabs + "ItemCell", content);
            
            //TODO
            cell.transform.GetComponent<Button>().onClick.AddListener(() => { UIPackageController.Instance.ItemClick(); });

            UIItemView script = cell.GetComponent<UIItemView>();

            //transform.root.GetComponentInChildren<UIPackageController>().SortClick();
            script.DisPlay(data[i]);
        }
    }
    
}
