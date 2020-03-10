using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using live2d;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
[ExecuteInEditMode]
public class L2D : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum MyEnum
    {
        Idle,
        Attack,
        Hit
    }
    public enum Figure
    {
        YoungerSister,
        Sorceress,
        Catwoman,
        Recyclebin,
    }
    /// <summary>
    /// 是否开启多图模式
    /// </summary>
    public bool multiple = true;
    public MyEnum pattern = MyEnum.Attack;
    public Figure figure = Figure.YoungerSister;
    /// <summary>
    /// 用于处理加载模型文件
    /// </summary>
    Live2DModelUnity modelUnity;

    Live2DMotion dMotion;

    MotionQueueManager queueManager;

    Matrix4x4 matrix4X4;

    public TextAsset textAsset;

    /// <summary>
    ///图片
    /// </summary>
    public Texture2D[] texture2Ds;
    public TextAsset[] textAssets;

    public AudioClip[] Lines;
    AudioSource III;
    void Start()
    {
        Live2D.init();
        if (figure == Figure.Catwoman)
        {
            Lines = new AudioClip[12];
            Lines = Resources.LoadAll<AudioClip>("Voice/L2D");


            III = transform.gameObject.AddComponent<AudioSource>();
            III.playOnAwake = false;
        }

        modelUnity = Live2DModelUnity.loadModel(textAsset.bytes);
        for (int i = 0; i < texture2Ds.Length; i++)
        {
            modelUnity.setTexture(i, texture2Ds[i]);
        }
        float mo = modelUnity.getCanvasWidth();
        matrix4X4 = Matrix4x4.Ortho(0, mo, mo, 0, -50.0f, 50.0f);

        queueManager = new MotionQueueManager();
        dMotion = Live2DMotion.loadMotion(textAssets[0].bytes);
        content = new string[12];
        if (figure == Figure.YoungerSister)
        {
            if (!File.Exists(Application.dataPath + "Lottery"))
            {
                content[0] = "第一次免费帮你占卜吧";
                content[1] = "今天运气非常好，消灭敌人有概率获得极品道具哦";
                content[2] = "商城今天会刷新一大批道具快去购买吧";
                content[3] = "我们主城堡隐藏着某个神秘的道具";
                content[4] = "<color=red>今天真糟糕，把道具都放在箱子里面吧</color>";
                content[5] = "<color=red>仍有风云，小心谨慎</color>";
                content[6] = "心情不好不要哭，让我来陪陪你吧";
                content[7] = "<color=blue>每次进入副本建议别重要的装备放仓库里，不然死亡之后什么都没有啦</color>";
                content[8] = "<color=red>一闪一闪亮晶晶，每天都是小星星</color>";
                content[9] = "<color=blue>太累了吧，休息一下吧，我在这等你哦</color>";
                content[10] = "<color=red>他们说游戏里其实是另一个世界</color>";
                content[11] = "<color=blue>好好珍惜你身边的所有人，也许有那么一天就····</color>";
            }

        }
        else if (figure == Figure.Sorceress)
        {
            if (!File.Exists(Application.dataPath + "Lottery"))
            {
                content[0] = "给老娘1000金币老娘帮你占卜一次";
                content[1] = "今天运气一般，杀敌也不会获得极品道具哪";
                content[2] = "存钱干嘛，商城每天都会刷新一大批道具快去买那";
                content[3] = "别看了，天上不会掉馅饼那";
                content[4] = "<color=red>呵呵，把道具都带在身上，不会死的那</color>";
                content[5] = "<color=red>这么担心干嘛那，没了就充钱买那</color>";
                content[6] = "<color=blue>管他三七二十一，别影响打游戏</color>";
                content[7] = "<color=blue>去去去，哪凉快哪呆着去</color>";
                content[8] = "<color=blue>你个沙雕，没事就知道调戏老娘</color>";
                content[9] = "1 2 3画个圈圈诅咒你";
                content[10] = "<color=red>今天在主城堡找神秘宝箱，谁趁老娘不注意，在地上扔香蕉皮，害得我屁股现在都痛</color>";
                content[11] = "<color=red>往地上扔香蕉皮的就是你吧，你完蛋了</color>";
            }
        }
        else if (figure == Figure.Catwoman)
        {
            if (!File.Exists(Application.dataPath + "Lottery"))
            {
                content[0] = "欢迎来到《LastProject》";
                content[1] = "没有账号可以去注册一个新的账号哦";
                content[2] = "登录失败不要慌，仔细想想应该可以想起来";
                content[3] = "一个账号可以注册多个角色呢";
                content[4] = "<color=red>不要盲目的删除角色，因为删除之后就永远不存在了</color>";
                content[5] = "<color=red>如果你非常无聊不妨来试试这款游戏吧</color>";
                content[6] = "<color=blue>适当游戏益脑，沉迷游戏伤身</color>";
                content[7] = "<color=blue>用心创造快乐</color>";
                content[8] = "<color=blue>人是铁饭是钢，吃饱了才有力气玩游戏</color>";
                content[9] = "副本难度不要太高，小心打不过哦";
                content[10] = "<color=red>有朋自远方来，交个朋友吧</color>";
                content[11] = "<color=red>我们这个世界需要你来守护</color>";
            }
        }
        else if (figure == Figure.Recyclebin)
        {
            if (!File.Exists(Application.dataPath + "Lottery"))
            {
                content[0] = "这里是柚子回收站，你想出售什么道具吗";
                content[1] = "高价回收、电冰箱、旧电脑、旧手机、旧电视·····";
                content[2] = "想好了在出售哦我可是不给你退货的";
                content[3] = "今天又想出售什么东西呢";
                content[4] = "<color=red>没道具了，快去杀怪掉落吧</color>";
                content[5] = "<color=red>柚子回收站24小时开放哦</color>";
                content[6] = "<color=blue>你的旧装备都可以在这里出售哦</color>";
                content[7] = "<color=blue>没钱了，不妨出售装备吧</color>";
                content[8] = "<color=blue>人是铁饭是钢，吃饱了才有力气卖装备</color>";
                content[9] = "装备全靠打，一键回收9999";
                content[10] = "<color=red>今天你肝了吗，不肝没装备卖了</color>";
                content[11] = "<color=red>以前这里有多个回收站的，后来就····</color>";
            }
        }


        his = transform.Find("ChatBubble").GetComponent<FalseHis>();
    }

    public FalseHis his;
    void Update()
    {
        if (modelUnity == null)
        {
            return;
        }
        modelUnity.setMatrix(transform.localToWorldMatrix * matrix4X4);

        if (queueManager.isFinished())
        {
            queueManager.startMotion(dMotion);
        }
        queueManager.updateParam(modelUnity);
        modelUnity.update();
    }
    private void OnRenderObject()
    {
        if (modelUnity == null)
        {
            return;
        }
        modelUnity.draw();
    }
    public string[] content;
    public void OnPointerClick(PointerEventData eventData)
    {

        //Debug.Log("你点击了按钮");


        his.yes();
        transform.Find("ChatBubble").GetComponent<FalseHis>().f = 0;
        if (pattern == MyEnum.Attack && multiple)
        {

            dMotion = Live2DMotion.loadMotion(textAssets[1].bytes);
            queueManager.startMotion(dMotion);
            pattern = MyEnum.Hit;
            int a = Random.Range(0, content.Length);
            // Actor(a);
            transform.Find("ChatBubble/Text").GetComponent<Text>().text = content[a];
        }
        else if (pattern == MyEnum.Hit && multiple)
        {
            dMotion = Live2DMotion.loadMotion(textAssets[2].bytes);
            queueManager.startMotion(dMotion);
            pattern = MyEnum.Idle;
            int a = Random.Range(0, content.Length);
            //Actor(a);
            transform.Find("ChatBubble/Text").GetComponent<Text>().text = content[a];
        }
        else if (pattern == MyEnum.Idle && multiple)
        {
            dMotion = Live2DMotion.loadMotion(textAssets[0].bytes);
            queueManager.startMotion(dMotion);
            pattern = MyEnum.Attack;
            int a = Random.Range(0, content.Length);
            //Actor(a);
            transform.Find("ChatBubble/Text").GetComponent<Text>().text = content[a];
        }
        if (figure == Figure.Catwoman)
        {
            // 点击的声音
            //int a = Random.Range(0, content.Length);
            //Actor(a);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    public void Actor(int st)
    {

        switch (st)
        {
            case 0:
                III.clip = Lines[0];
                break;
            case 1:
                III.clip = Lines[1];
                break;
            case 2:
                III.clip = Lines[2];
                break;
            case 3:
                III.clip = Lines[3];
                break;
            case 4:
                III.clip = Lines[4];
                break;
            case 5:
                III.clip = Lines[5];
                break;
            case 6:
                III.clip = Lines[6];
                break;
            case 7:
                III.clip = Lines[7];
                break;
            case 8:
                III.clip = Lines[8];
                break;
            case 9:
                III.clip = Lines[9];
                break;
            case 10:
                III.clip = Lines[10];
                break;
            case 11:
                III.clip = Lines[11];
                break;

        }
        III.Play();
    }
}
