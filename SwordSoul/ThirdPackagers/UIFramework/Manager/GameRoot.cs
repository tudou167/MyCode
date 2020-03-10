//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using TMPro;
//using System;

//public class GameRoot : MonoBehaviour
//{
//    //public TMP_InputField GM;
//    private void Awake()
//    {
//        //UIManager.Instance.Test();
//        //UIManager.Instance.PushPanel(UIPanelType.StartMain);
//        //BaseManager<UIMainMenuController>.Instance.Register(new UIMainMenuController());

//        UIManager.Instance.PushPanel(UIPanelType.MainMenuMVC);

//    }
//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.BackQuote))
//        {
//            GM.gameObject.SetActive(true);
//        }
//    }
//}
////     public void GMinput()
////     {
////         GM.text="";
////         UIMainMenuController mainC = BaseManager<UIMainMenuController>.Instance.Get(typeof(UIMainMenuController));
////         //Add Gold 1000
////         string[] temp = GM.text.Split(' ');
////         switch (temp[0])
////         {
////             case "Add":
////                 switch (temp[1])
////                 {
////                     case "Gold":
////                         RoleCountDataModel.Instance.AddGoldValue(int.Parse(temp[2]));
////                         mainC.RoleCountView.Display();
////                         break;
////                     case "Diamond":
////                         RoleCountDataModel.Instance.AddDiamondValue(int.Parse(temp[2]));
////                         mainC.RoleCountView.Display();
////                         break;
////                     case "Power":
////                         RoleCountDataModel.Instance.AddPowerValue(int.Parse(temp[2]));
////                         mainC.RoleCountView.Display();
////                         break;
////                 }
////                 break;
////         }
////         GM.gameObject.SetActive(false);
////     }
//// }
