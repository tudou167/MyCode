using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackageView : MonoBehaviour
{
    PackageData curPackage;
    GameObject curIcon;
    Transform content;
    Transform detail;
    GameObject isSelect;
    RoleData curHeroData;
    List<int> curList;
    void OnEnable()
    {
        curHeroData = HeroManager.Instance.GetCurHeroData();
        curList = Tool.Instance.GetShortcut(curHeroData.Prefab);
        curPackage = PackageManager.Instance.GetPackageData();
        if (isSelect != null && isSelect.transform.GetComponent<Toggle>() != null)
        {
            isSelect.transform.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            transform.Find("window/bēi_bāo/All_Toggle").GetComponent<Toggle>().isOn = false;
        }
        transform.Find("window/bēi_bāo/All_Toggle").GetComponent<Toggle>().isOn = true;
        transform.Find("window/gold/Text").GetComponent<Text>().text = "<color=yellow>" + curPackage.Gold + "</color>";
        content = transform.Find("window/bēi_bāo/ScrollView/Viewport/Content");
    }
    void Start()
    {
        Equip weapon = PackageManager.Instance.GetItem<Equip>(curList[0]);
        Equip weaponSS = PackageManager.Instance.GetItem<Equip>(curList[1]);
        Equip weaponWand = PackageManager.Instance.GetItem<Equip>(curList[2]);
        Equip armor = PackageManager.Instance.GetItem<Equip>(curHeroData.ArmorId);
        Consumables blood = PackageManager.Instance.GetItem<Consumables>(curList[3]);
        Consumables attack = PackageManager.Instance.GetItem<Consumables>(curList[4]);
        Consumables speed = PackageManager.Instance.GetItem<Consumables>(curList[5]);

        if (weapon != null)
        {
            ShowNowEquip("weapon", weapon.Icon, weapon.itemID);
        }
        if (weaponSS != null)
        {
            ShowNowEquip("weaponSS", weaponSS.Icon, weaponSS.itemID);
        }
        if (weaponWand != null)
        {
            ShowNowEquip("weaponWand", weaponWand.Icon, weaponWand.itemID);
        }
        if (armor != null)
        {
            ShowNowEquip("armor", armor.Icon, armor.itemID);
        }
        if (blood != null)
        {
            ShowNowConsumable("blood", blood.Icon, blood.itemID, blood.count);
        }
        if (attack != null)
        {
            ShowNowConsumable("attack", attack.Icon, attack.itemID, attack.count);
        }
        if (speed != null)
        {
            ShowNowConsumable("speed", speed.Icon, speed.itemID, speed.count);
        }
        transform.Find("window/gold/Text").GetComponent<Text>().text = "<color=yellow>" + curPackage.Gold + "</color>";
        content = transform.Find("window/bēi_bāo/ScrollView/Viewport/Content");
        curPackage = PackageManager.Instance.GetPackageData();
        PackageEquipList();
        PackageConsumableList();
        transform.Find("window/bēi_bāo/All_Toggle").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetAllItem(isOn); });
        transform.Find("window/bēi_bāo/equip_Toggle").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetEquip(isOn); });
        transform.Find("window/bēi_bāo/expend_Toggle").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetExpend(isOn); });
        transform.Find("window/bēi_bāo/else_Toggle").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetElse(isOn); });
    }
    void ShowNowEquip(string bolName,string ico,int itemid)
    {
        Transform bol = transform.Find("window/" + bolName + "/bool");
        GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", bol);
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(128, 128);
        //改图标
        icon.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(ico);

        icon.transform.Find("ID").GetComponent<Text>().text = "" + itemid;
        icon.transform.Find("Count_BG").gameObject.SetActive(false);

        icon.GetComponent<Button>().onClick.AddListener(ShowEquipDetail);
    }
    void ShowNowConsumable(string bolName, string ico, int itemid,int count)
    {
        Transform bol = transform.Find("window/" + bolName + "/bool");
        GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", bol);
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(128, 128);
        //改图标
        icon.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(ico);

        icon.transform.Find("ID").GetComponent<Text>().text = "" + itemid;
        icon.transform.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + count + "</color>";
        icon.GetComponent<Button>().onClick.AddListener(ShowConsumableDetail);
    }
    void PackageEquipList()
    {
        for (int i = 0; i < curPackage.equipList.Count; i++)
        {
            if (curPackage.equipList[i].itemID == curList[0] || curPackage.equipList[i].itemID == curList[1] || curPackage.equipList[i].itemID == curList[2] || curHeroData.ArmorId == curPackage.equipList[i].itemID)
            {
                continue;
            }
            GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", content);
            icon.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curPackage.equipList[i].Icon);
            icon.transform.Find("ID").GetComponent<Text>().text = "" + curPackage.equipList[i].itemID;
            icon.transform.Find("Count_BG").gameObject.SetActive(false);
            icon.GetComponent<Button>().onClick.AddListener(ShowEquipDetail);
        }
    }
    void PackageConsumableList()
    {
        for (int i = 0; i < curPackage.consumablesList.Count; i++)
        {
            if (curPackage.consumablesList[i].itemID == curList[3] || curPackage.consumablesList[i].itemID == curList[4] || curPackage.consumablesList[i].itemID == curList[5]) 
            {
                continue;
            }
            GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", content);
            //改图标
            icon.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curPackage.consumablesList[i].Icon);

            icon.transform.Find("ID").GetComponent<Text>().text = "" + curPackage.consumablesList[i].itemID;
            icon.transform.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + curPackage.consumablesList[i].count + "</color>";
            icon.GetComponent<Button>().onClick.AddListener(ShowConsumableDetail);
        }
    }
    private void GetAllItem(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(content);
        PackageEquipList();
        PackageConsumableList();
    }

    private void GetEquip(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(content);
        PackageEquipList();
    }

    private void GetExpend(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(content);
        PackageConsumableList();
    }

    private void GetElse(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(content);
    }


    void OnDisable()
    {
        Tool.Instance.DeleteChild(content);
        if (detail != null && detail.gameObject.activeSelf)
        {
            detail.gameObject.SetActive(false);
        }
    }
    void ShowEquipDetail()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        Equip curEquip = PackageManager.Instance.GetItem<Equip>(id);
        detail = transform.Find("window/Detail");
        if (!detail.gameObject.activeSelf)
        {
            detail.gameObject.SetActive(true);
        }
        
        detail.Find("ATK").GetComponent<Text>().gameObject.SetActive(true);
        detail.Find("DEF").GetComponent<Text>().gameObject.SetActive(true);
        detail.Find("ATK").GetComponent<Text>().text = "ATK:" + curEquip.ATK;
        detail.Find("DEF").GetComponent<Text>().text = "DEF:" + curEquip.DEF;
        detail.Find("Name").GetComponent<Text>().text = curEquip.Name + "(" + curEquip.ATKSpeed + ")";
        detail.Find("Description").GetComponent<Text>().text = curEquip.Detail;
        detail.Find("Price").GetComponent<Text>().text = "" + curEquip.Price;
    }
    void ShowConsumableDetail()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        Consumables curConsum = PackageManager.Instance.GetItem<Consumables>(id);
        Transform detail = transform.Find("window/Detail");
        detail.Find("ATK").GetComponent<Text>().gameObject.SetActive(false);
        detail.Find("DEF").GetComponent<Text>().gameObject.SetActive(false);
        detail.Find("Name").GetComponent<Text>().text = curConsum.Name;
        detail.Find("Description").GetComponent<Text>().text = curConsum.Detail;
        detail.Find("Price").GetComponent<Text>().text = "" + curConsum.Price;
    }
}
