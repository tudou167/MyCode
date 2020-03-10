using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_In : MonoSingletion<Sign_In>
{
    public Button[] buttons;
    public Button sky_1;
    public Button sky_2;
    public Button sky_3;
    public Button sky_4;
    public Button sky_5;
    public Button sky_6;
    public Button sky_7;
    public Button sky_7_SS;  //宝箱

    public List<GameObject> dTf = new List<GameObject>();

    PlayerDataInfo characterInfo;

    public Text registerDay;



    void Start()
    {
        registerDay = AllTools.Instance.FindChild(transform, "registerDay").GetComponent<Text>();
        But();
        DegisterDay();
        if (characterInfo.registerDay >= 7)
        {
            PlayerModel.Instance.AddRegisterDay_zero(0);
            Debug.Log(PlayerModel.Instance.AddRegisterDay_zero(0));
        }
        D_RegisterDay(characterInfo.registerDay);

    }
    public void Sky_1()
    {
        if (characterInfo.registerDay ==0)
        {

            dTf[0].SetActive(true);
            dTf[0].GetComponentInParent<Button>().interactable = false;

            PlayerModel.Instance.AddRegisterDay(1);
            PlayerModel.Instance.AddGold(1888);
            DegisterDay();
            AllTools.Instance.Alert("签到获得金币x1888");

        }
 
    }

    public void Sky_2()
    {
        if (characterInfo.registerDay == 1) {


            dTf[1].SetActive(true);
            dTf[1].GetComponentInParent<Button>().interactable = false;

            PlayerModel.Instance.AddRegisterDay(1);

            UIPackageModel.Instance.InItem(UIPackageModel.Instance.GetSomeItemByName("小红药瓶"),2);

            DegisterDay();
            AllTools.Instance.Alert("签到获得小红药水x2");
        }

    }
    public void Sky_3()
    {

        if (characterInfo.registerDay == 2)
        {

            dTf[2].SetActive(true);
            dTf[2].GetComponentInParent<Button>().interactable = false;
            PlayerModel.Instance.AddRegisterDay(1);
            UIPackageModel.Instance.InItem(UIPackageModel.Instance.GetSomeItemByName("普通强化石"), 2);
            DegisterDay();
            AllTools.Instance.Alert("签到获得普通强化石x2");
        }

    }

    public void Sky_4()
    {
        if (characterInfo.registerDay == 3)
        {
            dTf[3].SetActive(true);
            dTf[3].GetComponentInParent<Button>().interactable = false;
            PlayerModel.Instance.AddRegisterDay(1);
            UIPackageModel.Instance.InItem(UIPackageModel.Instance.GetSomeItemByName("攻击药瓶"), 2);
            DegisterDay();
            AllTools.Instance.Alert("签到获得攻击药水x2");
        }    

    }
    public void Sky_5()
    {
        if (characterInfo.registerDay == 4)
        {
            dTf[4].SetActive(true);
            dTf[4].GetComponentInParent<Button>().interactable = false;
            PlayerModel.Instance.AddRegisterDay(1);
            UIPackageModel.Instance.InItem(UIPackageModel.Instance.GetSomeItemByName("普通强化石"), 2);
            DegisterDay();
            AllTools.Instance.Alert("签到获得普通强化石x2");
        }

    }
    public void Sky_6()
    {
        if (characterInfo.registerDay == 5)
        {
            dTf[5].SetActive(true);
            dTf[5].GetComponentInParent<Button>().interactable = false;
            PlayerModel.Instance.AddRegisterDay(1);
            PlayerModel.Instance.AddDiamond(188);
            Debug.Log("获得钻石188");
            DegisterDay();
            AllTools.Instance.Alert("签到获得钻石x188");
        }
    
    }
    public void Sky_7()
    {
        if (characterInfo.registerDay == 6)
        {

            dTf[6].SetActive(true);
            dTf[6].GetComponentInParent<Button>().interactable = false;
            PlayerModel.Instance.AddRegisterDay(1);
            UIPackageModel.Instance.InItem(UIPackageModel.Instance.GetSomeItemByName("精炼强化石"));
            DegisterDay();
            AllTools.Instance.Alert("签到获得精炼强化石x1");
        }
  

    }

    public void Sky_7_SS()
    {
        if (characterInfo.registerDay < 7)
        {

            AllTools.Instance.Alert("连续签到未满7天");

        }
        else
        {
            sky_7_SS.interactable = false;
            AllTools.Instance.Load(AllPaths.Instance.lotteryPrefabs + "Lottery");
            DegisterDay();
            D_RegisterDay(characterInfo.registerDay);
        }


    }


    public void DegisterDay()  //记录该账号下的签到天数
    {
        characterInfo = PlayerDataInfoView.Instance.DisplayInfo();//拿到当前登录的账号数据
        registerDay.text = characterInfo.registerDay.ToString();
    }


    public void D_RegisterDay(int registerDay)
    {
        if (registerDay == -1) return;
        for (int i = 0; i < 7; i++)
        {
            string s = "d" + (1 + i);
            dTf.Add(AllTools.Instance.FindChild(transform, s).gameObject);
        }

        for (int i = 0; i < registerDay; i++)
        {
            //dTf[i].GetComponentInParent<Button>().interactable = false;
            dTf[i].GetComponentInParent<Button>().interactable = false;  //获得  i(该物体)  的父物体里button的组件 然后失活
            dTf[i].SetActive(true);
        }
    }

    public void But()
    {
        buttons = transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            switch (buttons[i].name)
            {
                case "sky_1":
                    {
                        sky_1 = buttons[i];
                        break;
                    }

                case "sky_2":
                    {
                        sky_2 = buttons[i];
                        break;
                    }
                case "sky_3":
                    {
                        sky_3 = buttons[i];
                        break;
                    }
                case "sky_4":
                    {
                        sky_4 = buttons[i];
                        break;
                    }
                case "sky_5":
                    {
                        sky_5 = buttons[i];
                        break;
                    }
                case "sky_6":
                    {
                        sky_6 = buttons[i];
                        break;
                    }
                case "sky_7":
                    {
                        sky_7 = buttons[i];
                        break;
                    }

                case "Sky_7_SS":
                    {
                        sky_7_SS = buttons[i];
                        break;
                    }
            }



        }

    }



    //public void Img()
    //{
    //    images = transform.GetComponentsInChildren<Image>();

    //    for (int i = 0; i < images.Length; i++)
    //    {

    //        switch (images[i].name)
    //        {

    //            case "d1":
    //                {
    //                    d1 = images[i];
    //                    d1.gameObject.SetActive(false);
    //                    break;
    //                }

    //            case "d2":
    //                {
    //                    d2 = images[i];
    //                    d2.gameObject.SetActive(false);
    //                    break;
    //                }

    //            case "d3":
    //                {
    //                    d3 = images[i];
    //                    d3.gameObject.SetActive(false);
    //                    break;
    //                }

    //            case "d4":
    //                {
    //                    d4 = images[i];
    //                    d4.gameObject.SetActive(false);
    //                    break;
    //                }

    //            case "d5":
    //                {
    //                    d5 = images[i];
    //                    d5.gameObject.SetActive(false);
    //                    break;
    //                }

    //            case "d6":
    //                {
    //                    d6 = images[i];
    //                    d6.gameObject.SetActive(false);
    //                    break;
    //                }

    //            case "d7":
    //                {
    //                    d7 = images[i];
    //                    d7.gameObject.SetActive(false);
    //                    break;
    //                }

    //        }

    //   }

    // }
}


