using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellItemView : MonoBehaviour
{
    PackageData curPackage;
    GameObject curIcon;
    Transform content;
    Transform detail;
    Text gold;
    Equip curEquip;
    Consumables curConsum;
    RoleData curHeroData;
    void OnEnable()
    {
        content = transform.Find("Viewport/Content");
        Tool.Instance.DeleteChild(content);
        curPackage = PackageManager.Instance.GetPackageData();
        curHeroData = HeroManager.Instance.GetCurHeroData();
        gold = transform.parent.Find("gold/Text").GetComponent<Text>();
        gold.text = "<color=yellow>" + curPackage.Gold + "</color>";

        for (int i = 0; i < curPackage.equipList.Count; i++)
        {
            if (curPackage.equipList[i].itemID == curHeroData.WeaponId || curHeroData.ArmorId == curPackage.equipList[i].itemID)
            {
                continue;
            }
            GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", content);
            //改图标
            icon.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curPackage.equipList[i].Icon);

            icon.transform.Find("ID").GetComponent<Text>().text = "" + curPackage.equipList[i].itemID;
            icon.transform.Find("Count_BG").gameObject.SetActive(false);
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(ShowEquipDetail);
        }
        for (int i = 0; i < curPackage.consumablesList.Count; i++)
        {
            GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", content);
            //改图标
            icon.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(curPackage.consumablesList[i].Icon);

            icon.transform.Find("ID").GetComponent<Text>().text = "" + curPackage.consumablesList[i].itemID;
            icon.transform.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + curPackage.consumablesList[i].count + "</color>";
            icon.GetComponent<IconDrag>().enabled = false;
            icon.GetComponent<Button>().onClick.AddListener(ShowConsumableDetail);
        }
    }
    void Start()
    {

        //curPackage = PackageManager.Instance.GetPackageData();
        //gold = transform.parent.Find("gold/Text").GetComponent<Text>();
        //gold.text = "<color=yellow>" + curPackage.Gold + "</color>";
        //content = transform.Find("Viewport/Content");
        //for (int i = 0; i < curPackage.equipList.Count; i++)
        //{
        //    GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", content);
        //    //改图标
        //    icon.transform.Find("Image").GetComponent<Image>();

        //    icon.transform.Find("ID").GetComponent<Text>().text = "" + curPackage.equipList[i].itemID;
        //    icon.transform.Find("Count_BG").gameObject.SetActive(false);
        //    icon.GetComponent<IconDrag>().enabled = false;
        //    icon.GetComponent<Button>().onClick.AddListener(ShowEquipDetail);
        //}
        //for (int i = 0; i < curPackage.consumablesList.Count; i++)
        //{
        //    GameObject icon = Tool.Instance.InstantiateObjOffset("Prefabs/UI/ZQS/icon", content);
        //    //改图标
        //    icon.transform.Find("Image").GetComponent<Image>();

        //    icon.transform.Find("ID").GetComponent<Text>().text = "" + curPackage.consumablesList[i].itemID;
        //    icon.transform.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + curPackage.consumablesList[i].count + "</color>";
        //    icon.GetComponent<IconDrag>().enabled = false;
        //    icon.GetComponent<Button>().onClick.AddListener(ShowConsumableDetail);

    }

    private void ShowEquipDetail()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        curEquip = PackageManager.Instance.GetItem<Equip>(id);
        detail = transform.Find("Image");
        detail.Find("atk").GetComponent<Text>().gameObject.SetActive(true);
        detail.Find("def").GetComponent<Text>().gameObject.SetActive(true);
        detail.Find("atk").GetComponent<Text>().text = "攻击力：" + curEquip.ATK;
        detail.Find("def").GetComponent<Text>().text = "防御力：" + curEquip.DEF;
        detail.Find("name").GetComponent<Text>().text = curEquip.Name + "(" + curEquip.ATKSpeed + ")";
        detail.Find("price").GetComponent<Text>().text = "出售价：" + (int)(curEquip.Price * 0.1f);
        detail.Find("sell").GetComponent<Button>().onClick.RemoveAllListeners();
        detail.Find("sell").GetComponent<Button>().onClick.AddListener(SellEquip);
    }



    private void ShowConsumableDetail()
    {
        curIcon = EventSystem.current.currentSelectedGameObject;
        int id;
        int.TryParse(curIcon.transform.Find("ID").GetComponent<Text>().text, out id);
        curConsum = PackageManager.Instance.GetItem<Consumables>(id);
        detail = transform.Find("Image");
        detail.Find("atk").GetComponent<Text>().gameObject.SetActive(false);
        detail.Find("def").GetComponent<Text>().gameObject.SetActive(false);
        detail.Find("name").GetComponent<Text>().text = curConsum.Name;
        detail.Find("price").GetComponent<Text>().text = "出售价：" + (int)(curConsum.Price * 0.1f);
        detail.Find("sell").GetComponent<Button>().onClick.RemoveAllListeners();
        detail.Find("sell").GetComponent<Button>().onClick.AddListener(SellConsumable);
    }

    private void SellEquip()
    {
        Debug.Log("出售成功");
        PackageManager.Instance.DeleteItem(curEquip.itemID);
        Destroy(curIcon);
        PackageManager.Instance.Gold += (int)(curEquip.Price * 0.1f);
        curPackage = PackageManager.Instance.GetPackageData();
        PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
        gold.text = "<color=yellow>" + curPackage.Gold + "</color>";
        transform.Find("Image/sell").GetComponent<Button>().onClick.RemoveAllListeners();
        detail.Find("atk").GetComponent<Text>().text = "";
        detail.Find("def").GetComponent<Text>().text = "        请选择装备/道具";
        detail.Find("name").GetComponent<Text>().text = "";
        detail.Find("price").GetComponent<Text>().text = "";
    }
    private void SellConsumable()
    {
        Debug.Log("出售成功");
        if (PackageManager.Instance.GetItem<Consumables>(curConsum.itemID).count == 1)
        {
            PackageManager.Instance.DeleteItem(curConsum.itemID);
            Destroy(curIcon);
            transform.Find("Image/sell").GetComponent<Button>().onClick.RemoveAllListeners();
            detail = transform.Find("Image");
            detail.Find("atk").GetComponent<Text>().gameObject.SetActive(false);
            detail.Find("def").GetComponent<Text>().gameObject.SetActive(true);
            detail.Find("def").GetComponent<Text>().text = "        请选择装备/道具";
            detail.Find("name").GetComponent<Text>().text = "";
            detail.Find("price").GetComponent<Text>().text = "";
        }
        else
        {
            PackageManager.Instance.GetItem<Consumables>(curConsum.itemID).count -= 1;
            curIcon.transform.Find("Count_BG/Count").GetComponent<Text>().text = "<color=white>" + PackageManager.Instance.GetItem<Consumables>(curConsum.itemID).count + "</color>";
        }
        
        PackageManager.Instance.Gold += (int)(curConsum.Price * 0.1f);
        curPackage = PackageManager.Instance.GetPackageData();
        PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
        gold.text = "<color=yellow>" + curPackage.Gold + "</color>";
    }
}



