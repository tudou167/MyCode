using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Module : MonoBehaviour
{
    /// <summary>
    /// Forgeshop<铁匠铺>,warehouse<仓库>,DivinationHouse<占卜屋>,SkillsHouse<技能屋>,shop<商店>
    /// </summary>
    public enum MyEnum
    {
        Forgeshop,
        warehouse,
        DivinationHouse,
        SkillsHouse,
        shop,
        Counterpart
    }

    public UnityAction Func;
    string[] actor;
    public AudioClip musicShop;
    AudioSource MainCamera;
    void Start()
    {
        Func = Method;

        actor = new string[5];
        actor[0] = "有朋自远方来，不亦说乎！";
        actor[1] = "啊！有妖气！";
        actor[2] = "即使我们无能为力，也不好被打败！";
        actor[3] = "刀锋所到之处，便是僵土！";
        actor[4] = "理解世界，而非享受！";
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
       
       
    }

    public MyEnum AtPresent;
    public void Method()
    {

        // Forgeshop<铁匠铺>
        if (AtPresent == MyEnum.Forgeshop)
        {
            Transform go = "Prefabs/UI/ZQS/Forgeshop".toLad("Canvas/Screen");
            // go.DOScale(new Vector3(1f,1f,1f), 0.3f);

            Sequence sq = DOTween.Sequence();
            sq.Append(go.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.0f));
            sq.Append(go.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.6f));
            sq.Append(go.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
            //go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            musicShop = Resources.Load<AudioClip>("Voice/BackgroundMusic/HOYO-MiX - Flyby");
            MainCamera.clip = musicShop;
            MainCamera.Play();
            MoveCamera.isCanRun = false;
        }
        //warehouse<仓库>
        else if (AtPresent == MyEnum.warehouse)
        {
            Transform go = "Prefabs/UI/ZQS/warehouse".toLad("Canvas/Screen");
            Sequence sq = DOTween.Sequence();

            sq.Append(go.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.5f));
            sq.Append(go.DOScale(new Vector3(0.9f, 0.9f, 1f), 0.4f));
            //  sq.Append(go.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.2f));
            //go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            musicShop = Resources.Load<AudioClip>("Voice/BackgroundMusic/HOYO-MiX - ACE");
            MainCamera.clip = musicShop;
            MainCamera.Play();
            MoveCamera.isCanRun = false;
        }
        //DivinationHouse<占卜屋>
        else if (AtPresent == MyEnum.DivinationHouse)
        {
            Transform go = "Prefabs/UI/ZQS/AtPresent".toLad("Canvas/Screen");
            Sequence sq = DOTween.Sequence();
            sq.Append(go.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.0f));
            sq.Append(go.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.6f));
            sq.Append(go.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
            musicShop = Resources.Load<AudioClip>("Voice/BackgroundMusic/HOYO-MiX - Gion");
            MainCamera.clip = musicShop;
            MainCamera.Play();
            MoveCamera.isCanRun = false;
        }
        //SkillsHouse < 技能屋 >
        else if (AtPresent == MyEnum.SkillsHouse)
        {
            Transform go = "Prefabs/UI/ZQS/SkillsHouse".toLad("Canvas/Screen");
            // go.DOScale(new Vector3(1f,1f,1f), 0.3f);

            Sequence sq = DOTween.Sequence();
            sq.Append(go.DOLocalMoveY(1287, 0.0f));
            sq.Append(go.DOLocalMoveY(0, 0.6f));
            sq.Append(go.DOLocalMoveY(50, 0.4f));
            sq.Append(go.DOLocalMoveY(0, 0.2f));
            musicShop = Resources.Load<AudioClip>("Voice/BackgroundMusic/HOYO-MiX - Startover");
            MainCamera.clip = musicShop;
            MainCamera.Play();
            // sq.Append(go.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.6f));
            //sq.Append(go.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
            // go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            MoveCamera.isCanRun = false;
        }
        // shop < 商店 >
        else if (AtPresent == MyEnum.shop)
        {
            Transform go = "Prefabs/UI/ZQS/Shop".toLad("Canvas/Screen");
            // go.DOScale(new Vector3(1f,1f,1f), 0.3f);

            Sequence sq = DOTween.Sequence();
            sq.Append(go.DOLocalMoveY(1287, 0.0f));
            sq.Append(go.DOLocalMoveY(0, 0.6f));
            sq.Append(go.DOLocalMoveY(50, 0.4f));
            sq.Append(go.DOLocalMoveY(0, 0.2f));
            musicShop = Resources.Load<AudioClip>("Voice/BackgroundMusic/Zoe _ JODODO - Pre-Pro");
            MainCamera.clip = musicShop;
            MainCamera.Play();
            // sq.Append(go.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.6f));
            //sq.Append(go.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
            // go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            MoveCamera.isCanRun = false;
        }
        //进入副本
        else if (AtPresent == MyEnum.Counterpart)
        {
            Transform go = "Prefabs/UI/ZQS/BeInCommonUse/Hint".toLad("Canvas/Screen");
            go.Find("window/Image/Text").GetComponent<Text>().text = "<color=red>温馨提示</color>";
            Text te = go.Find("window/text/TextContent").GetComponent<Text>();
            go.Find("window/text/login").GetComponent<Button>().onClick.AddListener(Counterpart);
            Transform back = go.Find("window/text/back");
            te.text = "是否进入副本，进入副本之后如果<color=red>死亡会掉落所有物品</color>(装备除外)，建议你把部分物品放入仓库在进入";
            Back BA = back.transform.gameObject.AddComponent<Back>();
            BA.Game = go.gameObject;
            back.transform.GetComponent<Button>().onClick.AddListener(BA.back);
            back.Find("Text").GetComponent<Text>().text = "否";
            back.transform.parent.Find("login/Text").GetComponent<Text>().text = "是";
            go.transform.gameObject.AddComponent<BoxCollider>().size = new Vector3(2500, 1500, 0);
            MoveCamera.isCanRun = false;
        }

        if (AtPresent != MyEnum.Counterpart)
        {
            MainCamera.volume = 0.3f;
        }
    }
    bool kk = false;
    /// <summary>
    /// 进入副本函数
    /// </summary>
    public void Counterpart()
    {

        StartCoroutine(FightChange.Instance.LoadScene(GameObject.Find("/Canvas").transform));
        return;
    }
    Image Load;
    Sprite[] sp;
    int r, s;
    Transform load;
    AsyncOperation async;
    IEnumerator counterpart()
    {
        kk = true;
        //async = SceneManager.LoadSceneAsync("Fight");
        yield return async;
        async = null;
    }

    private void Update()
    {
        //if (!kk && async == null)
        //{
        //    return;
       // }
        if (async != null)
        {
            Load.fillAmount = async.progress;
            Load.transform.parent.parent.Find("BarText").GetComponent<Text>().text = (async.progress * 100).ToString();
        }
       


    }
}
