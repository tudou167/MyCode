using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ForgeshopView : MonoBehaviour
{
    RoleData curHero;
    PackageData curPackage;
    Equip curWeapon = null;
    Equip curArmor = null;
    Consumables weaponStone = null;
    Consumables armorStone = null;
    Transform weaponIcon;
    Transform armorIcon;
    Transform stoneIcon;

    Transform effects_yp, effects_bb, sj, pop;
    // public  GameObject pop,ui;
    public UnityAction Func;
    void Start()
    {
        effects_yp = transform.Find("window/effects-yp");
        effects_bb = transform.Find("window/effects-bb");
        sj = transform.Find("window/effects-sj");
        effects_yp.DOShakePosition(10f, 20f, 1, 20).SetLoops(-1);

        curHero = HeroManager.Instance.GetCurHeroData();
        curPackage = PackageManager.Instance.GetPackageData();
        weaponIcon = transform.Find("window/weapon");
        armorIcon = transform.Find("window/armor");
        stoneIcon = transform.Find("window/StrongFossil");

        for (int i = 0; i < curPackage.consumablesList.Count; i++)
        {
            if (curPackage.consumablesList[i].Type == ItemType.WeaponMaterial)
            {
                weaponStone = new Consumables();
                weaponStone = PackageManager.Instance.GetItem<Consumables>(curPackage.consumablesList[i].itemID);
            }
            if (curPackage.consumablesList[i].Type == ItemType.ArmorMaterial)
            {
                armorStone = new Consumables();
                armorStone = PackageManager.Instance.GetItem<Consumables>(curPackage.consumablesList[i].itemID);
            }
        }

        curWeapon = PackageManager.Instance.GetItem<Equip>(curHero.WeaponId);
        curArmor = PackageManager.Instance.GetItem<Equip>(curHero.ArmorId);
        if (curWeapon != null)
        {
            //改图标
            weaponIcon.Find("bool/ico").GetComponent<Image>().sprite = Resources.Load<Sprite>(curWeapon.Icon);
            weaponIcon.Find("Text").GetComponent<Text>().text = "<color=yellow>" + curWeapon.ATKSpeed + "</color>级";
            weaponIcon.GetComponent<Button>().onClick.AddListener(WeaponSelect);
        }
        if (curArmor != null)
        {
            //改图标
            armorIcon.Find("bool/ico").GetComponent<Image>().sprite = Resources.Load<Sprite>(curArmor.Icon);
            armorIcon.Find("Text").GetComponent<Text>().text = "<color=yellow>" + curArmor.ATKSpeed + "</color>级";
            armorIcon.GetComponent<Button>().onClick.AddListener(ArmorSelect);
        }
    }
    public void effects()
    {
        Sequence s = DOTween.Sequence();
        s.Append(effects_bb.DOLocalMoveY(-1274, 2));
        pop = "Prefabs/UI/ZQS/pop-upWindows/effects-sj".toLad("Canvas/Screen");
        //   GameObject go = Instantiate(pop.gameObject);
        RectTransform goo = pop.transform as RectTransform;
        goo.sizeDelta = new Vector2(657, 864);
        // go.transform.localScale=new Vector3(5, 5, 5);
        s.Append(effects_bb.DOLocalMoveY(1274, 0));
    }

    private void WeaponSelect()
    {
        armorIcon.Find("bool/ico").GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        weaponIcon.Find("bool/ico").GetComponent<Image>().color = new Color(255, 255, 255, 1);
        if (weaponStone == null)
        {
            //改图标
            stoneIcon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(ItemManager.Instance.GetItem(60001).Icon);
            stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + 0 + "</color>";
            transform.Find("window/Text_details").GetComponent<Text>().text = "<color=yellow>强化石不足</color>";
            return;
        }
        //改图标
        stoneIcon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponStone.Icon);
        stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + weaponStone.count + "</color>";
        if (curWeapon.ATKSpeed * 2 > weaponStone.count)
        {
            transform.Find("window/Text_details").GetComponent<Text>().text = "<color=yellow>强化石不足</color>";
        }
        else
        {
            transform.Find("window/Text_details").GetComponent<Text>().text = "<color=yellow>需要<color=red>" + curWeapon.ATKSpeed * 2 + "</color>个强化石</color>";
            transform.Find("window/UpgradeOnce").GetComponent<Button>().onClick.RemoveAllListeners();
            transform.Find("window/UpgradeOnce").GetComponent<Button>().onClick.AddListener(WeaponLevelUp);
        }

    }

    private void ArmorSelect()
    {
        weaponIcon.Find("bool/ico").GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        armorIcon.Find("bool/ico").GetComponent<Image>().color = new Color(255, 255, 255, 1);
        if (armorStone == null)
        {
            //改图标
            stoneIcon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(ItemManager.Instance.GetItem(70001).Icon);
            stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + 0 + "</color>";
            transform.Find("window/Text_details").GetComponent<Text>().text = "<color=yellow>强化石不足</color>";
            return;
        }
        stoneIcon.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(armorStone.Icon);
        stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + armorStone.count + "</color>";
        if (curArmor.ATKSpeed * 2 > armorStone.count)
        {
            transform.Find("window/Text_details").GetComponent<Text>().text = "<color=yellow>强化石不足</color>";
        }
        else
        {
            transform.Find("window/Text_details").GetComponent<Text>().text = "<color=yellow>需要<color=red>" + curArmor.ATKSpeed * 2 + "</color>个强化石</color>";
            transform.Find("window/UpgradeOnce").GetComponent<Button>().onClick.RemoveAllListeners();
            transform.Find("window/UpgradeOnce").GetComponent<Button>().onClick.AddListener(ArmorLevelUp);
        }
    }

    private void WeaponLevelUp()
    {
        if (curWeapon.ATKSpeed * 2 <= weaponStone.count)
        {
            for (int i = 0; i < curPackage.consumablesList.Count; i++)
            {
                if (weaponStone.itemID == curPackage.consumablesList[i].itemID)
                {
                    if (weaponStone.count == curWeapon.ATKSpeed * 2)
                    {
                        stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + 0 + "</color>";
                        PackageManager.Instance.DeleteItem(weaponStone.itemID);
                        weaponStone = null;
                        curPackage = PackageManager.Instance.GetPackageData();
                    }
                    else
                    {
                        PackageManager.Instance.GetPackageData().consumablesList[i].count -= curWeapon.ATKSpeed * 2;
                        curPackage = PackageManager.Instance.GetPackageData();
                        weaponStone.count = curPackage.consumablesList[i].count;
                        stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + weaponStone.count + "</color>";
                    }
                }
            }
            for (int i = 0; i < curPackage.equipList.Count; i++)
            {
                if (curWeapon.itemID == curPackage.equipList[i].itemID)
                {
                    PackageManager.Instance.GetPackageData().equipList[i].ATKSpeed += 1;
                    curPackage = PackageManager.Instance.GetPackageData();
                    curWeapon.ATKSpeed = curPackage.equipList[i].ATKSpeed;
                }
            }

            weaponIcon.Find("Text").GetComponent<Text>().text = "<color=yellow>" + curWeapon.ATKSpeed + "</color>级";
            PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
            Debug.Log("武器成功");
            effects();
            transform.Find("window/UpgradeOnce").GetComponent<Button>().onClick.RemoveAllListeners();
            WeaponSelect();
        }
    }

    private void ArmorLevelUp()
    {
        if (curArmor.ATKSpeed * 2 <= armorStone.count)
        {
            for (int i = 0; i < curPackage.consumablesList.Count; i++)
            {
                if (armorStone.itemID == curPackage.consumablesList[i].itemID)
                {
                    if (armorStone.count == curArmor.ATKSpeed * 2)
                    {
                        stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + 0 + "</color>";
                        PackageManager.Instance.DeleteItem(armorStone.itemID);
                        armorStone = null;
                        curPackage = PackageManager.Instance.GetPackageData();
                    }
                    else
                    {
                        PackageManager.Instance.GetPackageData().consumablesList[i].count -= curArmor.ATKSpeed * 2;
                        curPackage = PackageManager.Instance.GetPackageData();
                        armorStone.count = curPackage.consumablesList[i].count;
                        stoneIcon.Find("Text").GetComponent<Text>().text = "<color=red>" + armorStone.count + "</color>";
                    }
                }
            }
            for (int i = 0; i < curPackage.equipList.Count; i++)
            {
                if (curArmor.itemID == curPackage.equipList[i].itemID)
                {
                    PackageManager.Instance.GetPackageData().equipList[i].ATKSpeed += 1;
                    curPackage = PackageManager.Instance.GetPackageData();
                    curArmor.ATKSpeed = curPackage.equipList[i].ATKSpeed;
                }
            }
            armorIcon.Find("Text").GetComponent<Text>().text = "<color=yellow>" + curArmor.ATKSpeed + "</color>级";
            PackageManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, HeroManager.Instance.GetCurHeroData().RoleID, curPackage);
            Debug.Log("防具成功");
            effects();
            transform.Find("window/UpgradeOnce").GetComponent<Button>().onClick.RemoveAllListeners();
            ArmorSelect();
        }
    }
}
