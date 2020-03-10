using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenRoleCountView:MonoSingletion<UIMainMenRoleCountView>
{
    public Text[] texts;
    public Text name1;
    public Text atk;
    public Text def;
    public Text crit;
    public Text lv;
    public Text hp;
    public Text maxHp;
    public UserInfoList info;
    public string userName;
    public CharacterInfo characterInfo;

    public override void Awake()
    {
        base.Awake();
        userName = Log_in.Instance._userName; 
        texts = transform.GetComponentsInChildren<Text>();
        GetTexts();
    }

   //遍历玩家动态表里的账号,获得这个账号就会获得这个账号下的关联数据
    public CharacterInfo DisplayCharacterInfo()
    {
        userName = Log_in.Instance._userName;  //登录的账号传给  userName
        info = AllToObject.Instance.GetUserInfo();
        for (int i = 0; i < info.User.Count; i++)
        {
            if (info.User[i].playerDataInfo.userName == userName)
            {
                if (info.User[i].characterInfo.characterInfos[0].name==null)   //判断名字是否为空,如果是,则把账号赋值给 name
                {
                    info.User[i].characterInfo.characterInfos[0].name = userName;
                  
                }
                return info.User[i].characterInfo.characterInfos[0];
            }
        }
        return null;
    }
    public void Display()  //把动态数据传递给UI  初始化数据
    {
        characterInfo = DisplayCharacterInfo();
        name1.text = characterInfo.name.ToString();     
        atk.text = characterInfo.atk.ToString(); 
        def.text = characterInfo.def.ToString();
        crit.text = characterInfo.crit.ToString();
        lv.text = characterInfo.lv.ToString();
        hp.text = characterInfo.hp.ToString();
        maxHp.text = characterInfo.maxHp.ToString();
        lv.text = PlayerDataInfoView.Instance.Lv_1();
       
    }

    //public void Display1(CharacterInfo da)  //把动态数据传递给UI
    //{
    //    name1.text = da.name.ToString();    
    //    atk.text = da.atk.ToString();
    //    def.text = da.def.ToString();
    //    crit.text = da.crit.ToString();
    //    lv.text = da.lv.ToString();
    //    hp.text = da.hp.ToString();
    //    maxHp.text = da.maxHp.ToString();
    //}

    public void GetTexts()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            switch (texts[i].name)
            {
                case "name1":
                    {
                        name1 = texts[i];
                        break;
                    }
                case "atk":
                    {
                        atk = texts[i];
                        break;
                    }
                case "def":
                    {
                        def = texts[i];
                        break;
                    }
                case "crit":
                    {
                        crit = texts[i];
                        break;
                    }
                case "lv":
                    {
                        lv = texts[i];
                        break;
                    }
                case "hp":
                    {
                        hp = texts[i];
                        break;
                    }
                case "maxHp":
                    {
                        maxHp = texts[i];
                        break;
                    }
            }
        }

    }
  

}

