using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

public class BaseButton : MonoBehaviour
{
    protected Button[] BtnArr;
    protected Toggle[] TogArr;
    protected virtual void Start()
    {
        //获取子集下所有Button的集合
        BtnArr = transform.GetComponentsInChildren<Button>(true);
        //将所有Button的点击事件绑定到HandlerNotification方法上
        for (int i = 0; i < BtnArr.Length; i++)
        {
            Transform Target = BtnArr[i].transform;
            //Target.gameObject.AddComponent<AudioSource>().clip = Resources.Load<AudioClip>("button");
            //Target.gameObject.GetComponent<AudioSource>().volume = 0.3f;

            BtnArr[i].onClick.AddListener(() =>
            {
                HandlerNotification(Target);
                //Target.gameObject.GetComponent<AudioSource>().Play();
            });
        }
        //获取子集下所有Toggle的集合
        TogArr = transform.GetComponentsInChildren<Toggle>(true);
        //将所有Toggle的点击事件绑定到ToggleHandlerNotification方法上
        for (int i = 0; i < TogArr.Length; i++)
        {
            Transform Target = TogArr[i].transform;
            TogArr[i].onValueChanged.AddListener((bool isOn) =>
            {
                ToggleHandlerNotification(Target);
            });
        }
    }
    public virtual void HandlerNotification(Transform Target)
    {
        switch (Target.name)
        {
            //通过不同的名称可以知道当前点击了哪个Button然后对其处理
            case "GoBack":
                {
                    transform.DOScale(0, 0.3f);
                    Invoke("GoBack", 0.3f);

                    //Destroy(gameObject,0.3f);
                    break;
                }
            case "Package#Button":
                {
                    Transform temp = LoadPrefabs(AllPaths.Instance.packagePrefabs, Target.name);
                    temp.DOScale(1, 0.3f);
                    UIPackageController.Instance.Init();
                    break;
                }
            case "Shop#Button":
                {
                    Transform temp = LoadPrefabs(AllPaths.Instance.shopPrefabs, Target.name);
                    temp.DOScale(1, 0.3f);
                    UIShopController.Instance.Init();
                    break;
                }
            case "Intensify#Button":
                {
                    Transform temp = LoadPrefabs(AllPaths.Instance.packagePrefabs, Target.name);
                    temp.DOScale(1, 0.3f);
                    UIIntensifyView.Instance.Init();
                    break;
                }
            case "Lottery#Button":
                {
                    Transform temp = LoadPrefabs(AllPaths.Instance.lotteryPrefabs, Target.name);
                    temp.DOScale(1, 0.3f);
                    break;
                }
            case "Log_in#Button":
                {
                    Transform temp = LoadPrefabs(AllPaths.Instance.Reg_logPrefabs, Target.name);
                    temp.DOScale(1, 0.3f);

                    break;
                }


        }
    }
    public void GoBack()
    {
        gameObject.SetActive(false);
    }
    public Transform LoadPrefabs(string AllPath, string path)
    {
        path = path.Split('#')[0];
        Transform temp = transform.root.Find(path);
        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            temp.transform.SetAsLastSibling();
            return temp;
        }
        GameObject go = AllTools.Instance.Load(AllPath + path);
        go.transform.localScale = Vector3.zero;
        if (go != null)
        {
            return go.transform;
        }
        Debug.LogError(AllPath + path + "不存在");
        return null;
    }
    public virtual void ToggleHandlerNotification(Transform Target)
    {
        switch (Target.name)
        {
            //通过不同的名称可以知道当前点击了哪个Toggle然后对其处理
            case "01":
                transform.GetComponent<BasePanel>().OnPushPanel("Game");
                break;
            case "02":
                transform.GetComponent<BasePanel>().OnPushPanel("CreatePlayer");
                break;
            case "骑士":
                GameObject temp = Target.Find("Select").gameObject;
                Target.Find("Select").localScale = Vector3.zero;
                if (Target.GetComponent<Toggle>().isOn)
                {
                    Target.Find("Select").DOScale(1, 0.2f);
                    break;
                }
                else
                {
                    Target.Find("Select").DOScale(0, 0.2f);
                    break;
                }

        }
    }
}
