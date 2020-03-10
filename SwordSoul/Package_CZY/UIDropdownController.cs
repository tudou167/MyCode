using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIDropdownController : MonoBehaviour {
    Transform list;
    void Awake()
    {
        list = transform.Find("List");
        for (int i = 0; i < list.childCount; i++)
        {
            list.GetChild(i).GetComponent<Button>().onClick.AddListener(() => {  UIPackageController.Instance.SortClick(); });
            
        }
        
    }

    void Start()
    {

    }

    private bool isOpen = false;
    public void Show()
    {
        if (isOpen)
        {
            isOpen = false;
            list.DOScaleY(0,0.25f);
        }
        else
        {
            isOpen = true;
            list.DOScaleY(1, 0.25f);
        }
    }


}
