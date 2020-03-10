using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject package;
    Transform myParent;
    Equip curEquip = null;
    Consumables curConsumable = null;
    RoleData curHeroData;
    PackageData packageData;
    List<int> curList;

    void Start()
    {
        

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        packageData = PackageManager.Instance.GetPackageData();
        curHeroData = HeroManager.Instance.GetCurHeroData();
        curList = Tool.Instance.GetShortcut(curHeroData.Prefab);
        package = GameObject.Find("Canvas/Screen/knapsack");
        myParent = package.transform.Find("window/bēi_bāo/ScrollView/Viewport/Content");
        string name = transform.parent.parent.name;
        switch (name)
        {
            case "weapon":
                curEquip = PackageManager.Instance.GetItem<Equip>(curList[0]);
                ChangeCurrentString(0, 0);
                break;
            case "weaponSS":
                curEquip = PackageManager.Instance.GetItem<Equip>(curList[1]);
                ChangeCurrentString(1, 0);
                break;
            case "weaponWand":
                curEquip = PackageManager.Instance.GetItem<Equip>(curList[2]);
                ChangeCurrentString(2, 0);
                break;
            case "armor":
                curEquip = PackageManager.Instance.GetItem<Equip>(curHeroData.ArmorId);
                HeroManager.Instance.GetCurHeroData().ArmorId = 0;
                curHeroData = HeroManager.Instance.GetCurHeroData();
                break;
            case "blood":
                curConsumable = PackageManager.Instance.GetItem<Consumables>(curList[3]);
                ChangeCurrentString(3, 0);
                break;
            case "attack":
                curConsumable = PackageManager.Instance.GetItem<Consumables>(curList[4]);
                ChangeCurrentString(4, 0);
                break;
            case "speed":
                curConsumable = PackageManager.Instance.GetItem<Consumables>(curList[5]);
                ChangeCurrentString(5, 0);
                break;
            default:
                int id;
                int.TryParse(transform.Find("ID").GetComponent<Text>().text, out id);
                curEquip = PackageManager.Instance.GetItem<Equip>(id);
                if (curEquip == null)
                {
                    curConsumable = PackageManager.Instance.GetItem<Consumables>(id);
                }
                break;
        }
        transform.parent = package.transform;
        transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.GetComponent<RectTransform>(), Input.mousePosition, eventData.enterEventCamera, out worldPos))
        {
            transform.position = worldPos;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (curEquip != null)
        {
            if (curEquip.Type == ItemType.LongHandle)
            {
                ChangeCurrentPos(0, "weapon", curEquip.itemID, eventData);
            }
            if (curEquip.Type == ItemType.SwordShield)
            {
                ChangeCurrentPos(1, "weaponSS", curEquip.itemID, eventData);
            }
            if (curEquip.Type == ItemType.Wand)
            {
                ChangeCurrentPos(2, "weaponWand", curEquip.itemID, eventData);
            }
            if (curEquip.Type == ItemType.Armor)
            {
                #region 防具需要独立写
                Transform armorico = package.transform.Find("window/armor/bool/icon");
                Transform armorbool = package.transform.Find("window/armor/bool");
                if (armorico != null)
                {
                    if (eventData.pointerCurrentRaycast.gameObject == armorico.gameObject)
                    {
                        armorico.parent = myParent;
                        armorico.localPosition = Vector3.zero;
                        transform.parent = armorbool;
                        transform.localPosition = Vector3.zero;
                        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;

                        HeroManager.Instance.GetCurHeroData().ArmorId = curEquip.itemID;
                        curHeroData = HeroManager.Instance.GetCurHeroData();
                        HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
                        return;
                    }
                }
                if (eventData.pointerCurrentRaycast.gameObject == armorbool.gameObject)
                {
                    transform.parent = armorbool;
                    transform.localPosition = Vector3.zero;
                    transform.GetComponent<CanvasGroup>().blocksRaycasts = true;

                    HeroManager.Instance.GetCurHeroData().ArmorId = curEquip.itemID;
                    curHeroData = HeroManager.Instance.GetCurHeroData();
                    HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);

                    return;
                }
                transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                transform.parent = myParent;
                transform.localPosition = Vector3.zero;
                HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
                return;
                #endregion
            }
        }
        if (curConsumable != null)
        {
            if (curConsumable.ID >= 10001 && curConsumable.ID <= 10003)
            {
                ChangeCurrentPos(3, "blood", curConsumable.itemID, eventData);
            }
            if (curConsumable.ID >= 10004 && curConsumable.ID <= 10006)
            {
                ChangeCurrentPos(4, "attack", curConsumable.itemID, eventData);
            }
            if (curConsumable.ID >= 10007 && curConsumable.ID <= 10009)
            {
                ChangeCurrentPos(5, "speed", curConsumable.itemID, eventData);
            }
        }
        GameObject borderLong = package.transform.Find("window/weapon/Image").gameObject;
        GameObject borderSS = package.transform.Find("window/weaponSS/Image").gameObject;
        GameObject borderWand = package.transform.Find("window/weaponWand/Image").gameObject;
        Equip nowEquip = PackageManager.Instance.GetItem<Equip>(curHeroData.WeaponId);
        if(nowEquip == null)
        {
            borderLong.SetActive(false);
            borderSS.SetActive(false);
            borderWand.SetActive(false);
        }
        else
        {
            if (nowEquip.Type == ItemType.LongHandle)
            {
                borderLong.SetActive(true);
                borderSS.SetActive(false);
                borderWand.SetActive(false);
            }
            if (nowEquip.Type == ItemType.SwordShield)
            {
                borderLong.SetActive(false);
                borderSS.SetActive(true);
                borderWand.SetActive(false);
            }
            if (nowEquip.Type == ItemType.Wand)
            {
                borderLong.SetActive(false);
                borderSS.SetActive(false);
                borderWand.SetActive(true);
            }
        }
    }
    void ChangeCurrentPos(int index, string curPos, int curItemID, PointerEventData eventData)
    {
        Transform ico = package.transform.Find("window/" + curPos + "/bool/icon");
        Transform bol = package.transform.Find("window/" + curPos + "/bool");
        if (ico != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject == ico.gameObject)
            {
                ico.parent = myParent;
                ico.localPosition = Vector3.zero;
                transform.parent = bol;
                transform.localPosition = Vector3.zero;
                transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                if (curConsumable == null)
                {
                    HeroManager.Instance.GetCurHeroData().WeaponId = curItemID;
                }
                ChangeCurrentString(index, curItemID);
                HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
                return;
            }
        }
        if (eventData.pointerCurrentRaycast.gameObject == bol.gameObject)
        {
            transform.parent = bol;
            transform.localPosition = Vector3.zero;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            ChangeCurrentString(index, curItemID);
            if (curConsumable == null)
            {
                HeroManager.Instance.GetCurHeroData().WeaponId = curItemID;
            }
            HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
            return;
        }

        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.parent = myParent;
        transform.localPosition = Vector3.zero;
        if (HeroManager.Instance.GetCurHeroData().WeaponId == curItemID)
        {
            for (int i = 0; i < curList.Count; i++) 
            {
                if (curList[i] != 0)
                {
                    HeroManager.Instance.GetCurHeroData().WeaponId = curList[i];
                    break;
                }
            }
        }
        curHeroData = HeroManager.Instance.GetCurHeroData();
        HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
        return;
    }


    void ChangeCurrentPos2(int index, string curPos, int curItemID, PointerEventData eventData)
    {
        Transform ico = package.transform.Find("window/" + curPos + "/bool/icon");
        Transform bol = package.transform.Find("window/" + curPos + "/bool");
        if (ico != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject == ico.gameObject)
            {
                ico.parent = myParent;
                ico.localPosition = Vector3.zero;
                transform.parent = bol;
                transform.localPosition = Vector3.zero;
                transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                HeroManager.Instance.GetCurHeroData().WeaponId = curItemID;
                ChangeCurrentString(index, curItemID);
                HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
                return;
            }
        }
        if (eventData.pointerCurrentRaycast.gameObject == bol.gameObject)
        {
            transform.parent = bol;
            transform.localPosition = Vector3.zero;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            ChangeCurrentString(index, curItemID);
            HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
            return;
        }

        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.parent = myParent;
        transform.localPosition = Vector3.zero;
        if (HeroManager.Instance.GetCurHeroData().WeaponId == curItemID)
        {
            for (int i = 0; i < curList.Count; i++)
            {
                if (curList[i] != 0)
                {
                    HeroManager.Instance.GetCurHeroData().WeaponId = curList[i];
                    break;
                }
            }
        }
        curHeroData = HeroManager.Instance.GetCurHeroData();
        HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, curHeroData.RoleID, curHeroData);
        return;
    }


    void ChangeCurrentString(int j, int n)
    {
        curList[j] = n;
        string temArr = "";
        for (int i = 0; i < curList.Count; i++)
        {
            if (i < 2)
            {
                temArr += curList[i] + "#";
            }
            else if (i == 2)
            {
                temArr += curList[i] + "|";
            }
            else if (i < 5)
            {
                temArr += curList[i] + "#";
            }
            else
            {
                temArr += curList[i];
            }
        }
        HeroManager.Instance.GetCurHeroData().Prefab = temArr;
        curList = Tool.Instance.GetShortcut(HeroManager.Instance.GetCurHeroData().Prefab);
        curHeroData = HeroManager.Instance.GetCurHeroData();
    }
}
