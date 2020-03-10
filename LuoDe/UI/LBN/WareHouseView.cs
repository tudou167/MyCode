using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class WareHouseView : MonoBehaviour
{
    PackageData curPackage;
    WarehouseData curWare;
    RoleData curHeroData;
    Transform packageContent;
    Transform wareContent;
    GameObject curIcon;
    GameObject isSelect;
    List<int> curList;

    void Start()
    {
        curPackage = PackageManager.Instance.GetPackageData();
        curWare = WarehouseManager.Instance.GetPackageData();
        curHeroData = HeroManager.Instance.GetCurHeroData();
        curList = Tool.Instance.GetShortcut(curHeroData.Prefab);
        packageContent = transform.Find("window/bēi_bāo/ScrollView/Viewport/Content");
        wareContent = transform.Find("window/cāng_kù/ScrollView/Viewport/Content");
        PackageEquipList();
        PackageConsumableList();
        WareEquipList();
        WareConsumableList();
        transform.Find("window/cāng_kù/Toggle/all").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetAllItem(isOn); });
        transform.Find("window/cāng_kù/Toggle/equip").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetEquip(isOn); });
        transform.Find("window/cāng_kù/Toggle/expend").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetExpend(isOn); });
        transform.Find("window/cāng_kù/Toggle/else").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { GetElse(isOn); });
    }
    void PackageEquipList()
    {
        for (int i = 0; i < curPackage.equipList.Count; i++)
        {
            if (curPackage.equipList[i].itemID == curList[0] || curPackage.equipList[i].itemID == curList[1] || curPackage.equipList[i].itemID == curList[2] || curHeroData.ArmorId == curPackage.equipList[i].itemID)
            {
                continue;
            }
            Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", packageContent).transform;
            //改图标
            icon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curPackage.equipList[i].Icon);

            icon.Find("ID").GetComponent<Text>().text = "" + curPackage.equipList[i].itemID;
            icon.Find("Count_BG").gameObject.SetActive(false);
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(EquipInsert);
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
            Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", packageContent).transform;
            //改图标
            icon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curPackage.consumablesList[i].Icon);

            icon.Find("ID").GetComponent<Text>().text = "" + curPackage.consumablesList[i].itemID;
            icon.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + curPackage.consumablesList[i].count + "</color>";
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(consumableInsert);

        }
    }
    void WareEquipList()
    {
        for (int i = 0; i < curWare.equipList.Count; i++)
        {
            Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", wareContent).transform;
            //改图标
            icon.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curWare.equipList[i].Icon);

            icon.transform.Find("ID").GetComponent<Text>().text = "" + curWare.equipList[i].itemID;
            icon.transform.Find("Count_BG").gameObject.SetActive(false);
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(equipTake);
        }
    }
    void WareConsumableList()
    {
        for (int i = 0; i < curWare.consumablesList.Count; i++)
        {
            Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", wareContent).transform;
            //改图标
            icon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curWare.consumablesList[i].Icon);

            icon.Find("ID").GetComponent<Text>().text = "" + curWare.consumablesList[i].itemID;
            icon.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + curWare.consumablesList[i].count + "</color>";
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(consumableTake);
        }
    }
    void EquipInsert()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        int inWareId = WarehouseManager.Instance.AddItem(PackageManager.Instance.GetItem<Equip>(id));
        PackageManager.Instance.DeleteItem(id);
        curPackage = PackageManager.Instance.GetPackageData();
        curWare = WarehouseManager.Instance.GetPackageData();
        Destroy(curIcon);
        Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", wareContent).transform;
        //改图标
        icon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(WarehouseManager.Instance.GetItem<Equip>(inWareId).Icon);

        icon.Find("ID").GetComponent<Text>().text = "" + inWareId;
        icon.Find("Count_BG").gameObject.SetActive(false);
        icon.GetComponent<IconDrag>().enabled = false;
        icon.GetComponent<Button>().onClick.AddListener(equipTake);


        PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
        WarehouseManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curWare);
    }
    void equipTake()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        int inPackId = PackageManager.Instance.AddItem(WarehouseManager.Instance.GetItem<Equip>(id));
        WarehouseManager.Instance.DeleteItem(id);
        curPackage = PackageManager.Instance.GetPackageData();
        curWare = WarehouseManager.Instance.GetPackageData();
        Destroy(curIcon);
        Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", packageContent).transform;
        //改图标
        icon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(PackageManager.Instance.GetItem<Equip>(inPackId).Icon); ;

        icon.Find("ID").GetComponent<Text>().text = "" + inPackId;
        icon.Find("Count_BG").gameObject.SetActive(false);
        icon.GetComponent<IconDrag>().enabled = false;
        icon.GetComponent<Button>().onClick.AddListener(EquipInsert);

        PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
        WarehouseManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curWare);
    }
    void consumableInsert()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        Consumables curConsum = PackageManager.Instance.GetItem<Consumables>(id);
        Consumables isInWare = null;
        for (int i = 0; i < curWare.consumablesList.Count; i++)
        {
            if (curWare.consumablesList[i].ID == curConsum.ID)
            {
                isInWare = curWare.consumablesList[i];
                break;
            }
        }
        
        if (isInWare == null)
        {
            Consumables newCon = new Consumables(PackageManager.Instance.GetItem<Consumables>(curConsum.itemID).itemData, 1);
            int newId = WarehouseManager.Instance.AddItem(newCon);

            Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", wareContent).transform;
            //改图标
            icon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(newCon.Icon);

            icon.Find("ID").GetComponent<Text>().text = "" + newId;
            icon.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + WarehouseManager.Instance.GetItem<Consumables>(newId).count + "</color>";
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(consumableTake);
        }
        else
        {
            WarehouseManager.Instance.GetItem<Consumables>(isInWare.itemID).count += 1;
            for (int i = 0; i < wareContent.childCount; i++)
            {
                if (wareContent.GetChild(i).Find("ID").GetComponent<Text>().text == "" + isInWare.itemID)
                {
                    wareContent.GetChild(i).Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + WarehouseManager.Instance.GetItem<Consumables>(isInWare.itemID).count + "</color>";
                }
            }
        }
        if (curConsum.count == 1)
        {
            PackageManager.Instance.DeleteItem(id);
            Destroy(curIcon);
        }
        else
        {
            PackageManager.Instance.GetItem<Consumables>(curConsum.itemID).count -= 1;
            curIcon.transform.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + PackageManager.Instance.GetItem<Consumables>(curConsum.itemID).count + "</color>";
        }
        curPackage = PackageManager.Instance.GetPackageData();
        curWare = WarehouseManager.Instance.GetPackageData();
        PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
        WarehouseManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curWare);
    }


    void consumableTake()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        Consumables curConsum = WarehouseManager.Instance.GetItem<Consumables>(id);
        Consumables isInPack = null;
        for (int i = 0; i < curPackage.consumablesList.Count; i++)
        {
            if (curPackage.consumablesList[i].ID == curConsum.ID)
            {
                isInPack = curPackage.consumablesList[i];
                break;
            }
        }
        
        if (isInPack == null)
        {
            Consumables newCon = new Consumables(WarehouseManager.Instance.GetItem<Consumables>(curConsum.itemID).itemData, 1);
            int newId = PackageManager.Instance.AddItem(newCon);

            Transform icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", packageContent).transform;
            //改图标
            icon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(newCon.Icon);

            icon.Find("ID").GetComponent<Text>().text = "" + newId;
            icon.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + PackageManager.Instance.GetItem<Consumables>(newId).count + "</color>";
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(consumableInsert);
        }
        else
        {
            PackageManager.Instance.GetItem<Consumables>(isInPack.itemID).count += 1;
            for (int i = 0; i < packageContent.childCount; i++)
            {
                if (packageContent.GetChild(i).Find("ID").GetComponent<Text>().text == "" + isInPack.itemID)
                {
                    packageContent.GetChild(i).Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + PackageManager.Instance.GetItem<Consumables>(isInPack.itemID).count + "</color>";
                }
            }
        }
        if (curConsum.count == 1)
        {
            WarehouseManager.Instance.DeleteItem(id);
            Destroy(curIcon);
        }
        else
        {
            WarehouseManager.Instance.GetItem<Consumables>(curConsum.itemID).count -= 1;
            curIcon.transform.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + curConsum.count + "</color>";
        }
        curPackage = PackageManager.Instance.GetPackageData();
        curWare = WarehouseManager.Instance.GetPackageData();
        PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
        WarehouseManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curWare);
    }

    private void GetAllItem(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(packageContent);
        Tool.Instance.DeleteChild(wareContent);
        PackageEquipList();
        PackageConsumableList();
        WareEquipList();
        WareConsumableList();
    }

    private void GetEquip(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(packageContent);
        Tool.Instance.DeleteChild(wareContent);
        PackageEquipList();
        WareEquipList();
    }

    private void GetExpend(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(packageContent);
        Tool.Instance.DeleteChild(wareContent);
        PackageConsumableList();
        WareConsumableList();
    }

    private void GetElse(bool isOn)
    {
        isSelect = EventSystem.current.currentSelectedGameObject;
        isOn = true;
        Tool.Instance.DeleteChild(packageContent);
        Tool.Instance.DeleteChild(wareContent);
    }
}
