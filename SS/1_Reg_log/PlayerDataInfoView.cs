    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine.UI;
    using UnityEngine;

    public class PlayerDataInfoView : MonoSingletion<PlayerDataInfoView> {
        public Text[] texts;

        public Text name1;
        public Text lv;
        public Text hp;
        public Text maxHp;
        public Text exp;
        public Text maxExp;
        public Text gold;
        public Text diamond;

        public UserInfoList info;
        public string userName;

        public Image[] images;
        public Image hpImage;
        public Image expImage;
        public Image bossImage;

        //怪物UI
        public Text bossname;
        public Text bossmaxHp;
        public Text bossHp;
        public Text boss_LV;

        public string lv_1;
        public override void Awake () {
            base.Awake ();
            userName = Log_in.Instance._userName;
            GetImage ();
            GetTexts ();
        }

        public void Start () {
            Display_PlayerDataInfo ();
            Display_BossDataInfo ();

        }

        //遍历玩家账号与登录的账号相对应,获得账号下的数据
        public PlayerDataInfo DisplayInfo () {
            userName = Log_in.Instance._userName;
            info = AllToObject.Instance.GetUserInfo ();
            for (int i = 0; i < info.User.Count; i++) {
                if (info.User[i].playerDataInfo.userName == userName) {
                    if (info.User[i].characterInfo.characterInfos[0].name == null) {
                        info.User[i].characterInfo.characterInfos[0].name = userName;
                    }
                    return info.User[i].playerDataInfo;
                }
            }
            return null;
        }

        //boss UI刷新

        public void Display_BossDataInfo () {

            if (bossname != null) bossname.text = PlayerModel.Instance._bossInfo.name;
            if (bossmaxHp != null) bossmaxHp.text = PlayerModel.Instance._bossInfo.maxHp.ToString ();
            if (bossHp != null) bossHp.text = PlayerModel.Instance._bossInfo.hp.ToString ();
            if (boss_LV != null) boss_LV.text = PlayerModel.Instance._bossInfo.lv.ToString ();
            if (bossHp != null&&bossmaxHp != null) bossImage.fillAmount = float.Parse (bossHp.text) / float.Parse (bossmaxHp.text);
        }

        //刷新左上角玩家UI数据方法
        public void Display_PlayerDataInfo () {
            PlayerDataInfo characterInfo = DisplayInfo ();

            exp.text = AllFormula.Instance.GetExpInfo (characterInfo.exp) [0].ToString ();; //经验
            maxExp.text = AllFormula.Instance.GetExpInfo (characterInfo.exp) [3].ToString ();
            gold.text = characterInfo.gold.ToString ();
            diamond.text = characterInfo.diamond.ToString ();

            //玩家经验条UI变动
            float exp_1 = AllFormula.Instance.GetExpInfo (characterInfo.exp) [0];
            float maxExp_1 = AllFormula.Instance.GetExpInfo (characterInfo.exp) [3];
            expImage.fillAmount = exp_1 / maxExp_1;

            userName = PlayerModel.Instance.userName;
            info = AllToObject.Instance.GetUserInfo ();
            for (int i = 0; i < info.User.Count; i++) {
                if (info.User[i].playerDataInfo.userName == userName) {

                    name1.text = info.User[i].characterInfo.characterInfos[0].name;
                    lv.text = AllFormula.Instance.GetExpInfo (info.User[i].playerDataInfo.exp) [2].ToString ();
                    hp.text = info.User[i].characterInfo.characterInfos[0].hp.ToString ();
                    maxHp.text = info.User[i].characterInfo.characterInfos[0].maxHp.ToString ();
                    exp.text = info.User[i].playerDataInfo.exp.ToString ();

                    //玩家血条UI变动
                    float hp_1 = info.User[i].characterInfo.characterInfos[0].hp;
                    float maxhp_1 = info.User[i].characterInfo.characterInfos[0].maxHp;
                    hpImage.fillAmount = hp_1 / maxhp_1;

                }
            }

        }
        public string Lv_1 () {
            lv_1 = lv.text;
            return lv_1;
        }

        public string GetId () { return DisplayInfo ().gold.ToString (); }
        /// <summary>
        /// 遍历该物体下的 Image 名字对应 switch里的字符串名字
        /// </summary>
        public void GetImage () {
            images = transform.GetComponentsInChildren<Image> ();

            for (int i = 0; i < images.Length; i++) {
                switch (images[i].name) {
                    case "hpImage":
                        {
                            hpImage = images[i];
                            break;

                        }
                    case "expImage":
                        {
                            expImage = images[i];
                            break;

                        }
                    case "bossImage":
                        {
                            bossImage = images[i];
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 遍历 该物体下的 Text 名字对应  switch里的字符串名字
        /// </summary>
        public void GetTexts () {
            texts = transform.GetComponentsInChildren<Text> ();
            PlayerDataInfo characterInfo = DisplayInfo ();
            for (int i = 0; i < texts.Length; i++) {
                switch (texts[i].name) {

                    case "name1":
                        {
                            name1 = texts[i];
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

                    case "exp":
                        {
                            exp = texts[i];
                            break;
                        }
                    case "maxExp":
                        {
                            maxExp = texts[i];
                            break;
                        }
                    case "gold":
                        {
                            gold = texts[i];
                            break;
                        }
                    case "diamond":
                        {
                            diamond = texts[i];
                            break;
                        }

                    case "bossname":
                        {
                            bossname = texts[i];
                            break;
                        }
                    case "bossmaxHp":
                        {
                            bossmaxHp = texts[i];
                            break;
                        }
                    case "bossHp":
                        {
                            bossHp = texts[i];
                            break;
                        }
                    case "boss_LV":
                        {
                            boss_LV = texts[i];
                            break;

                        }
                }
            }

        }
    }