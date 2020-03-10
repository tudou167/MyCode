using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettlementController : MonoBehaviour
{
    Transform uiCanvas;
    Image img;

    void Start()
    {
        uiCanvas = GameObject.Find("/Canvas").transform;
        FightTmpData data = FightTmpManager.Instance.GetData();
        if (data.isBack && data.isEnd)
        {
            GameObject go = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/Settlement", uiCanvas);
            Transform content = go.transform.Find("Scroll View/Viewport/Content");

            Button closeBtn;
            closeBtn = go.transform.Find("CloseBtn").GetComponent<Button>();
            closeBtn.onClick.AddListener(() => { Destroy(go); });

            GameObject gold = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/ItemCell", content);
            gold.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Sprites/Ui_jinbi5");
            gold.transform.Find("Text").GetComponent<Text>().text = "Gold：" + data.gold;

            GameObject exp = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/ItemCell", content);
            exp.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Sprites/Ui_jingyan");
            exp.transform.Find("Text").GetComponent<Text>().text = "Exp：" + data.exp;

            List<Item> itemList = ItemManager.Instance.GetAllItem();
            quickSort(itemList);
            itemList.Reverse();
            int ssr = 0;
            int sr = 0;
            int r = 0;
            for (int i = 0; i < itemList.Count; i++)
            {
                switch (itemList[i].Quality)
                {
                    case ItemQuality.SSR:
                        ssr = i;
                        break;
                    case ItemQuality.SR:
                        sr = i;
                        break;
                    case ItemQuality.R:
                        r = i;
                        break;
                }
            }
            List<Item> resultList = new List<Item>();
            Item tmp = null;
            GameObject item = null;
            for (int i = 0; i < data.ssrNum; i++)
            {
                tmp = itemList[Random.Range(0, ssr)];
                resultList.Add(tmp);
                item = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/ItemCell", content);
                item.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(tmp.Icon);
                item.transform.Find("Text").GetComponent<Text>().text = "名称：" + tmp.Name + " 品质：" + tmp.Quality.ToString();
            }
            for (int i = 0; i < data.srNum; i++)
            {
                tmp = itemList[Random.Range(ssr + 1, sr)];
                resultList.Add(tmp);
                item = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/ItemCell", content);
                item.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(tmp.Icon);
                item.transform.Find("Text").GetComponent<Text>().text = "名称：" + tmp.Name + " 品质：" + tmp.Quality.ToString();
            }
            for (int i = 0; i < data.rNum; i++)
            {
                tmp = itemList[Random.Range(sr + 1, r)];
                resultList.Add(tmp);
                item = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/ItemCell", content);
                item.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(tmp.Icon);
                item.transform.Find("Text").GetComponent<Text>().text = "名称：" + tmp.Name + " 品质：" + tmp.Quality.ToString();
            }
            for (int i = 0; i < data.nNum; i++)
            {
                tmp = itemList[Random.Range(r + 1, itemList.Count)];
                resultList.Add(tmp);
                item = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/ItemCell", content);
                item.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(tmp.Icon);
                item.transform.Find("Text").GetComponent<Text>().text = "名称：" + tmp.Name + " 品质：" + tmp.Quality.ToString();
            }

            PackageData packageData = PackageManager.Instance.GetPackageData();
            PackageManager.Instance.Gold += data.gold;
            RoleData heroData = HeroManager.Instance.GetCurHeroData();
            heroData.Exp += data.exp;
            heroData.LV += Tool.Instance.LvCheck(heroData.BaseExp, heroData.Exp, heroData.LV)[3];
            bool isFind = false;
            for (int i = 0; i < resultList.Count; i++)
            {
                if (resultList[i] is Consumables)
                {
                    isFind = false;
                    for (int j = 0; j < packageData.consumablesList.Count; j++)
                    {
                        if (packageData.consumablesList[j].ID == resultList[i].ID)
                        {
                            isFind = true;
                            PackageManager.Instance.GetPackageData().consumablesList[j].count += 3;
                            break;
                        }
                    }
                    if (isFind == false)
                    {
                        Consumables con = new Consumables((resultList[i] as Consumables).itemData, 1);
                        PackageManager.Instance.AddItem(con);
                        packageData.consumablesList.Add(con);
                    }
                }
                else
                {
                    PackageManager.Instance.AddItem(new Equip((resultList[i] as Equip).itemData));
                }
            }
            HeroManager.Instance.SaveData(heroData);
            PackageManager.Instance.SaveData(PackageManager.Instance.GetPackageData());

        }
        Pool.Instance.ReSet();
        data = new FightTmpData();
        data.curPlieNum = 0;
        data.nNum = 0;
        data.rNum = 0;
        data.srNum = 0;
        data.ssrNum = 0;
        data.gold = 0;
        data.exp = 0;
        data.sword = null;
        data.ss = null;
        data.wand = null;
        data.consumable1 = null;
        data.consumable2 = null;
        data.consumable3 = null;
        data.isBack = false;
        data.isEnd = false;
        FightTmpManager.Instance.GetData(data);
    }


    public void quickSort(List<Item> array)
    {
        quickSort(array, 0, array.Count - 1);
    }

    private void quickSort(List<Item> array, int left, int right)
    {
        if (array == null || left >= right || array.Count <= 1)
        {
            return;
        }
        int mid = partition(array, left, right);
        quickSort(array, left, mid);
        quickSort(array, mid + 1, right);
    }

    private int partition(List<Item> array, int left, int right)
    {
        Item temp = array[left];
        while (right > left)
        {
            // 先判断基准数和后面的数依次比较
            while (temp.Quality <= array[right].Quality && left < right)
            {
                --right;
            }
            // 当基准数大于了 arr[left]，则填坑
            if (left < right)
            {
                array[left] = array[right];
                ++left;
            }
            // 现在是 arr[right] 需要填坑了
            while (temp.Quality >= array[left].Quality && left < right)
            {
                ++left;
            }
            if (left < right)
            {
                array[right] = array[left];
                --right;
            }
        }
        array[left] = temp;
        return left;
    }
}
