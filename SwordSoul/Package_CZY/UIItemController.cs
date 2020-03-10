using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemController : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Vector3 curPos;
    private UIItemView itemV;
    public Canvas sortLv;

    void Awake()
    {
        sortLv = GetComponent<Canvas>();
    }

    void Start()
    {
        itemV = GetComponent<UIItemView>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        curPos = itemV.rectTransform.position;
        //TODO
        if (transform.root.Find("RoleAllStatus") == null)
        {
            StartDrag(false);
        }
        else
        {
            StartDrag(true);
            UIRoleAllStatusController.Instance.rasc.SetAsLastSibling();
        }
        UIRoleAllStatusController.Instance.Init();
    }
    public void OnDrag(PointerEventData eventData)
    {
        itemV.DragItem(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform finalPos = UIRoleAllStatusController.Instance.Wear(itemV._Data);
        bool isIn = RectTransformUtility.RectangleContainsScreenPoint(finalPos, eventData.position, eventData.pressEventCamera);
        if (isIn)
        {
            Destroy(gameObject);
            EquipmentTemp.Row.Row2 original =  UIPackageModel.Instance.WearEquip(itemV._Data);
            UIPackageController.Instance.Init();
            CharacterInfo userData = PlayerModel.Instance._characterInfo;

            if (original != null)
            {
                userData.atk -= original.atk;
                userData.def -= original.def;
                userData.crit -= original.crit;
                userData.maxHp -= original.hp;

            }

            userData.atk += itemV._Data.atk;
            userData.def += itemV._Data.def;
            userData.crit += itemV._Data.crit;
            userData.maxHp += itemV._Data.hp;
            PlayerModel.Instance.SaveCharacterInfo(userData);

            UIRoleAllStatusController.Instance.Init();
            itemV.setItemPos(finalPos.position);
            
        }
        else
        {
            itemV.setItemPos(curPos);
        }
        sortLv.overrideSorting = false;

        UIRoleAllStatusController.Instance.rasc.gameObject.SetActive(false);
    }


    public void StartDrag(bool isFirst)
    {
        sortLv.overrideSorting = true;
        //TODO
        if (isFirst == false)
        {
            AllTools.Instance.Load(AllPaths.Instance.roleaAllStatusPrefabs + "RoleAllStatus");
        }
        else
        {
            UIRoleAllStatusController.Instance.rasc.gameObject.SetActive(true);

        }
    }

}
