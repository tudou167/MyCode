using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{

    public void atk() //测试方法
    { 
        PlayerModel.Instance.AddAtk(100);
        PlayerModel.Instance.AddHp(100);
        PlayerModel.Instance.AddExp(100);
      //  UIMainMenRoleCountView.Instance.Display();  
      //  PlayerDataInfoView.Instance.Display_PlayerDataInfo();
       // PlayerDataInfoView.Instance.Display();
    }

}
