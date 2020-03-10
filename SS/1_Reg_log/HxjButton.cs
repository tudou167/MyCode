using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HxjButton : BaseButton
{
   
  
    public override void HandlerNotification(Transform Target)
    {
       
        base.HandlerNotification(Target);
       
        switch (Target.name)
        {    
            case "RoleAllStatus#Button":  //  玩家属性
                {
                    //Transform temp = LoadPrefabs(AllPaths.Instance.Reg_logPrefabs, Target.name);
                    Transform temp = LoadPrefabs(AllPaths.Instance.roleaAllStatusPrefabs, Target.name);

                    temp.DOScale(1, 0.3f);
                    //UIMainMenRoleCountView.Instance.Display();  //打开属性UI 就会刷新动态数据

                    break;
                }
            case "Log_in#Button":   //  加载登录
                {
                    Transform temp = LoadPrefabs(AllPaths.Instance.Reg_logPrefabs, Target.name);
                    temp.DOScale(1, 0.3f);
            
                    break;
                }
            case "Panel_UI#Button":  //  加载主UI
                {
             
                    Transform temp = Log_in.Instance.LoginClick();
                    temp.DOScale(1, 0.3f);
                  
                    break;
                }
            case "Sign_In#Button":  // 加载签到
                {
                    Transform temp = LoadPrefabs(AllPaths.Instance.Reg_logPrefabs, Target.name);
         
                    temp.DOScale(1, 0.3f);             
                    break;
                }
            case "sky_1":  // 第一天签到按键
                {
                  
                    
                        Sign_In.Instance.Sky_1();
       
                    break;
                }
            case "sky_2":  // 第二天签到按键
                {

                    
                        Sign_In.Instance.Sky_2();
                     
              
                    break;
                }
            case "sky_3":  // 第三天签到按键
                {
                   
                    
                        Sign_In.Instance.Sky_3();
                  

                    break;
                }
            case "sky_4":  // 第四天签到按键
                {
                
                    
                        Sign_In.Instance.Sky_4();
  
              
                    break;

                }
            case "sky_5":  // 第五天签到按键
                {
                  
                    
                        Sign_In.Instance.Sky_5();
  
                    break;
                }
            case "sky_6":  // 第六天签到按键
                {
               
                    
                        Sign_In.Instance.Sky_6();
  
                    break;
                }
            case "sky_7":  // 第七天签到按键
                {
                 
                    
                        Sign_In.Instance.Sky_7();
      
                    break;
                }
            case "Sky_7_SS":  // 大礼包按键
                {

                    Sign_In.Instance.Sky_7_SS();

                    break;
                }
        }
    }
}
