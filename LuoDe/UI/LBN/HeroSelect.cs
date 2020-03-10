using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HeroSelect : MonoBehaviour
{
    private List<RoleData> heroList;
    GameObject curCell;
    GameObject go;
    GameObject delete;
    RectTransform ret;
    GameObject HeroCell;
    GameObject cell;
    public UnityAction Func;
    GameObject model;

    public void OnEnable()
    {
        Tool.Instance.DeleteChild(transform.Find("HeroList/Viewport/Content"));
        model = transform.Find("Model").gameObject;
        model.SetActive(true);
        go = transform.Find("Go").gameObject;
        delete = transform.Find("Delete").gameObject;
        heroList = HeroManager.Instance.GetAllHero();
        if (heroList.Count == 0)
        {
            GameObject lastCell = Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/HeroCell", transform.Find("HeroList/Viewport/Content"));
            lastCell.GetComponent<Button>().onClick.AddListener(ClickLast);
            ret = lastCell.transform as RectTransform;
            ret.sizeDelta = new Vector2(512, 228);
            ret.transform.Find("cursor").gameObject.SetActive(false);
        }
        else
        {
            quickSort(heroList);
            for (int i = 0; i < heroList.Count + 1; i++)
            {
                cell = Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/HeroCell", transform.Find("HeroList/Viewport/Content"));

                ret = cell.transform as RectTransform;
                ret.sizeDelta = new Vector2(512, 228);
                ret.transform.Find("cursor").gameObject.SetActive(false);
                // reset();
                if (i == heroList.Count)
                {
                    cell.GetComponent<Button>().onClick.AddListener(ClickLast);
                    continue;
                }
                cell.transform.Find("Text").GetComponent<Text>().text = "Lv：<color=red>" + heroList[i].LV + "</color>";
                cell.transform.Find("id").GetComponent<Text>().text = "昵称：<color=yellow>" + heroList[i].Name + "</color>";
                cell.GetComponent<Button>().onClick.AddListener(ClickOne);

            }
        }
        transform.Find("Back").GetComponent<Button>().onClick.AddListener(GoBackToLogin);
        actor = new string[5];
        actor[0] = "有朋自远方来，不亦说乎！";
        actor[1] = "啊！有妖气！";
        actor[2] = "即使我们无能为力，也不好被打败！";
        actor[3] = "刀锋所到之处，便是僵土！";
        actor[4] = "理解世界，而非享受！";

        HeroCell = transform.Find("HeroList/Viewport/Content").gameObject;
        Func = HeroDelete;
    }
    AudioClip[] actorSlines;
    void Start()
    {
        // 进入选择角色界面时播放的声音

        //actorSlines = new AudioClip[4];
        //actorSlines = Resources.LoadAll<AudioClip>("Voice/actorSlines");
        //int a = Random.Range(0, 5);
        //AudioSource asd = transform.gameObject.AddComponent<AudioSource>();
        //asd.clip = actorSlines[a];
        //asd.playOnAwake = false;
        //asd.Play();

    }
    /// <summary>
    /// 选择角色
    /// </summary>
    void ClickOne()
    {

        model.SetActive(true);
        //reset();
        go.SetActive(true);
        delete.SetActive(true);
        go.transform.Find("Text").GetComponent<Text>().text = "进入游戏";
        Transform tf = transform.Find("HeroList/Viewport/Content");
        curCell = EventSystem.current.currentSelectedGameObject;

        //Debug.Log(curCell.transform.Find("Text").GetComponent<Text>().text);
        for (int i = 0; i < heroList.Count; i++)
        {

            string h = curCell.transform.Find("id").GetComponent<Text>().text;
            if (h == "昵称：<color=yellow>" + heroList[i].Name + "</color>")
            {
                HeroManager.Instance.GetHero(UserManager.Instance.GetCurUser().ID + "_" + heroList[i].RoleID);
                HeroManager.Instance.GetUserHeroID(UserManager.Instance.GetCurUser().ID + "_" + heroList[i].RoleID);
                //Debug.Log(heroList[i].Name + heroList[i].Prefab + heroList[i].Weapon);
                curCell.transform.Find("cursor").gameObject.SetActive(true);
                go.GetComponent<Button>().onClick.RemoveAllListeners();
                go.GetComponent<Button>().onClick.AddListener(GameStart);
                delete.GetComponent<Button>().onClick.RemoveAllListeners();
                //删除角色
                delete.GetComponent<Button>().onClick.AddListener(Hint);
                /// 
                break;
            }
        }

        reset(curCell.transform.GetSiblingIndex());
    }
    /// <summary>
    /// 创建一个新角色
    /// </summary>
    void ClickLast()
    {
        curCell = EventSystem.current.currentSelectedGameObject;
        //reset();
        go.SetActive(true);
        delete.SetActive(false);
        go.transform.Find("Text").GetComponent<Text>().text = "创建角色";
        go.GetComponent<Button>().onClick.RemoveAllListeners();
        go.GetComponent<Button>().onClick.AddListener(GoCreate);
        curCell.transform.Find("cursor").gameObject.SetActive(true);

        reset(curCell.transform.GetSiblingIndex());
    }
    /// <summary>
    /// 去创建
    /// </summary>
    void GoCreate()
    {
        if (go.transform.Find("Text").GetComponent<Text>().text == "创建角色")
        {
            GameObject heroCreate = GameObject.Find("UI/Canvas/HeroCreate");
            if (heroCreate == null)
            {
                heroCreate = Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/HeroCreate", transform.parent);
            }
            if (!heroCreate.activeSelf)
            {
                heroCreate.SetActive(true);
            }
            gameObject.SetActive(false);
        }

    }

    /// 
    // bool jd = true;
    string[] actor;
    /// <summary>
    /// 返回上一个登录界面
    /// </summary>
    void GoBackToLogin()
    {
        Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/Login", transform.parent);
        Destroy(gameObject);
    }
    //删除角色
    void HeroDelete()
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            string h = curCell.transform.Find("id").GetComponent<Text>().text;
            if (h == "昵称：<color=yellow>" + heroList[i].Name + "</color>")
            {
                HeroManager.Instance.SaveData(UserManager.Instance.GetCurUser().ID, heroList[i].RoleID, null);
                Destroy(curCell);
                Debug.Log("删除成功");
                go.SetActive(false);
                delete.SetActive(false);
                model.SetActive(true);
                break;
            }
        }
    }


    /// <summary>
    /// 游戏开始
    /// </summary>
    void GameStart()
    {
        model.SetActive(false);
        GameObject go = transform.Find("Go").gameObject;
        Sprite[] sp = Resources.LoadAll<Sprite>(AllPath.Instance.loadImg);
        int r = Random.Range(0, sp.Length);
        int s = Random.Range(0, 5);
        if (Load == null)
        {
            Transform load = "Prefabs/UI/ZQS/BeInCommonUse/Load".toLad("UI/Canvas");
            Load = load.Find("window/ProgressBar/Image").GetComponent<Image>();
            BarText = load.Find("window/BarText").GetComponent<Text>();
        }
        if (go.transform.Find("Text").GetComponent<Text>().text == "进入游戏")
        {  //jd = false;
            Tool.Instance.InstantiateObj("Prefabs/Loader/HeroSkillLoader", transform.root);
            Tool.Instance.InstantiateObj("Prefabs/Loader/PackageLoader", transform.root);
            Tool.Instance.InstantiateObj("Prefabs/Loader/WarehouseLoader", transform.root);
            RoleData hero = HeroManager.Instance.GetCurHeroData();
            hero.Weapon = PackageManager.Instance.GetItem<Equip>(hero.WeaponId);
            hero.Armor = PackageManager.Instance.GetItem<Equip>(hero.ArmorId);

            StartCoroutine(LoadingMain());
        }
        Load.transform.parent.parent.GetComponent<Image>().sprite = sp[r];
        Load.transform.parent.parent.Find("BarText").GetComponent<Text>().text = actor[s];
    }
    AsyncOperation op;
    /// <summary>
    /// 进度条相关
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingMain()
    {

        op = SceneManager.LoadSceneAsync("HomeScene");
        //op.allowSceneActivation = false;
        yield return op;
        Transform heroCreateTr = transform.parent.Find("HeroCreate");
        if (heroCreateTr != null)
        {
            GameObject heroCreate = heroCreateTr.gameObject;
            Destroy(heroCreate);
        }
        Destroy(gameObject);
        op = null;
        Destroy(Load.transform.parent.parent.parent.parent.parent.gameObject);
    }
    Image Load;
    Text BarText;
    float ll;
    private void Update()
    {
        if (op != null)
        {
            ll = op.progress;
            Load.fillAmount = op.progress;
            BarText.text = "<color=green>" + ll * 100 + "</color>%".ToString();
            //Debug.Log("协同程序的进度"+op.progress);
            //Debug.Log("float的进度"+ll);
        }


    }

    void reset(int index)
    {
        for (int i = 0; i < HeroCell.transform.childCount; i++)
        {
            HeroCell.transform.GetChild(i).transform.Find("cursor").gameObject.SetActive(false);
            if (index == i)
            {
                HeroCell.transform.GetChild(i).transform.Find("cursor").gameObject.SetActive(true);
            }
        }
        //ret.transform.Find("cursor").gameObject.SetActive(false);
    }
    Transform hint;
    void Hint()
    {
        model.SetActive(false);
        hint = "Prefabs/UI/ZQS/BeInCommonUse/Hint".toLad("UI/Canvas");
        Transform texts = hint.Find("window/text");
        texts.parent.Find("Image/Text").GetComponent<Text>().text = "<color=red>温馨提示</color>";
        texts.Find("TextContent").GetComponent<Text>().text = "删除角色，你的角色数据，<color=red>将永远永远消失无法找回</color>,是否删除";
        Button bu = texts.Find("login").GetComponent<Button>();
        bu.onClick.AddListener(HeroDelete);
        bu.transform.Find("Text").GetComponent<Text>().text = "是";

        Back backs = texts.Find("back").gameObject.AddComponent<Back>();
        backs.transform.Find("Text").GetComponent<Text>().text = "否";
        backs.transform.gameObject.GetComponent<Button>().onClick.AddListener(back);
        backs.Game = hint.gameObject;
        backs.sj = 0;
        bu.onClick.AddListener(backs.back);

    }
    void back()
    {
        Destroy(hint.gameObject);
        model.SetActive(true);
        MoveCamera.isCanRun = true;
    }


    public void quickSort(List<RoleData> array)
    {
        quickSort(array, 0, array.Count - 1);
    }

    private void quickSort(List<RoleData> array, int left, int right)
    {
        if (array == null || left >= right || array.Count <= 1)
        {
            return;
        }
        int mid = partition(array, left, right);
        quickSort(array, left, mid);
        quickSort(array, mid + 1, right);
    }

    private int partition(List<RoleData> array, int left, int right)
    {
        RoleData temp = array[left];
        while (right > left)
        {
            // 先判断基准数和后面的数依次比较
            while (temp.RoleID <= array[right].RoleID && left < right)
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
            while (temp.RoleID >= array[left].RoleID && left < right)
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
