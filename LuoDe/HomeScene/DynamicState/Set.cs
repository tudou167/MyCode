using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Set : MonoBehaviour
{

    Slider MusicSlider, FunctionSlider, ImageQuality;
    Voice voice;
    Button ll;
    public Transform setWindos;
    RectTransform setWindosRect;
    RectTransform backRect;
    void Start()
    {
        MusicSlider = transform.parent.Find("SetWindos/MusicSlider").GetComponent<Slider>();
        FunctionSlider = MusicSlider.transform.parent.Find("FunctionSlider").GetComponent<Slider>();
        ImageQuality = FunctionSlider.transform.parent.Find("SkillSlider").GetComponent<Slider>();
        voice = GameObject.Find("voice").AddComponent<Voice>();
        ll = FunctionSlider.transform.parent.Find("back").GetComponent<Button>();
        ll.onClick.AddListener(nnnnn);
        MusicSlider.transform.parent.Find("ESC").GetComponent<Button>().onClick.AddListener(hhhh);
        setWindosRect = setWindos as RectTransform;
        backRect = FunctionSlider.transform.parent.Find("back") as RectTransform;
    }
    bool o, oo = false;
    void Update()
    {
        voice.MusicSlider = MusicSlider.value;
        voice.FunctionSlider = FunctionSlider.value;
        voice.ImageQuality = ImageQuality.value;
        if (o && oo)
        {

            StartCoroutine(enumerator());
            oo = false;
        }

    }
    Transform hint;
    /// <summary>
    /// 退出游戏提示窗口
    /// </summary>
    public void hhhh()
    {
        hint = transform.parent.Find("Hint");
        if (hint == null)
        {
            hint = "Prefabs/UI/ZQS/BeInCommonUse/Hint".toLad("Canvas/Screen");
        }
        hint.gameObject.SetActive(true);
        Text t = hint.Find("window/text/TextContent").GetComponent<Text>();
        t.alignment = TextAnchor.MiddleCenter;
        hint.gameObject.AddComponent<BoxCollider>().size = new Vector3(2500, 1500, 0);
        t.text = "确定退出游戏吗，<color=red>退出游戏未保存的游戏数据会丢失</color>";
        t.transform.parent.Find("login").GetComponent<Button>().onClick.AddListener(ESC);
        t.transform.parent.Find("back").GetComponent<Button>().onClick.AddListener(back);
        t.transform.parent.Find("login/Text").GetComponent<Text>().text = "<color=red>是</color>";
        t.transform.parent.Find("back/Text").GetComponent<Text>().text = "否";
    }
    IEnumerator enumerator()
    {
        Sequence q = DOTween.Sequence();
        q.Append(ll.transform.DOLocalMoveX(ll.transform.localPosition.x + backRect.rect.width, 0.4f));
        q.Append(ll.transform.DOLocalMoveX(ll.transform.localPosition.x, 0.4f));
        yield return new WaitForSeconds(2.5f);
        oo = true;
    }

    public void back()
    {
        hint.gameObject.SetActive(false);
        //Destroy(hint);

    }
    public void ESC()
    {
        Application.Quit();
    }
    /// <summary>
    /// 弹出设置窗
    /// </summary>
    public void uuuuu()
    {
        setWindos.DOLocalMoveX(setWindos.localPosition.x - setWindosRect.rect.width, 1);
        o = true; oo = true;
    }
    /// <summary>
    /// 收起设置窗
    /// </summary>
    public void nnnnn()
    {
        setWindos.DOLocalMoveX(setWindos.localPosition.x + setWindosRect.rect.width, 1);
        o = false; oo = false;
    }
}
