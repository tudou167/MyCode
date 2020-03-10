using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class playGame : MonoBehaviour
{
    Transform initialize, load, ll, LoadText;
    bool f = false;
    Text t, g, ge, me;
    float o = 0;
    GameObject Init, voice;
    private string ExitAnimationTitleOne = "罗德地牢";
    private string ExitAnimationTitleTow = "罗德地牢";
    AudioClip Timi_Logo;
    AudioSource sound;
    private void Awake()
    {
        initialize = transform.Find("initialize");
        load = transform.Find("load");
        transform.Find("initialize/Image").gameObject.SetActive(true);
        StartCoroutine("play");
        ll = load.transform.Find("load/load");
        LoadText = load.Find("LoadText");
        t = LoadText.GetComponent<Text>();
        sp = Resources.LoadAll<Sprite>(AllPath.Instance.loadImg);
        g = transform.Find("load/Text").GetComponent<Text>();
        ge = transform.Find("initialize/Text").GetComponent<Text>();
        me = transform.Find("initialize/me").GetComponent<Text>();
        voice = GameObject.Find("voice");
        sound = voice.AddComponent<AudioSource>();
        Timi_Logo = Resources.Load<AudioClip>("Voice/Logo/Timi_Logo");
        sound.clip = Timi_Logo;

    }
    Sprite[] sp;
    void Start()
    {
        Init = Tool.Instance.InstantiateObj("Prefabs/Init", transform.root);

    }
    IEnumerator play()
    {
        yield return new WaitForSeconds(0.2f);
        sound.Play();
        ge.DOText(ExitAnimationTitleOne, 1.5f);
        me.DOText("用心创造快乐", 1f);
        yield return new WaitForSeconds(1.5f);
        Play();
        g.DOText(ExitAnimationTitleTow, 2f);
        //Sequence q = DOTween.Sequence();
        //q.Append(g.DOColor(new Color(255, 210, 0), 0.5f));
        //q.Append(g.DOColor(new Color(255, 0, 0), 0.5f));
        //q.Append(g.DOColor(new Color(255, 0, 255), 0.5f));
        //q.Append(g.DOColor(new Color(150, 210, 10), 0.5f));
        //q.Append(g.DOColor(new Color(5, 210, 245), 0.5f));
        g.DOColor(new Color(255, 210, 0), 0.5f);
        g.DOColor(new Color(255, 0, 0), 1f);
        yield return new WaitForSeconds(1f);
        initialize.gameObject.SetActive(false);
    }
    void Play()
    {
        load.gameObject.SetActive(true);
        f = true;
        int r = Random.Range(0, sp.Length);
        load.transform.GetComponent<Image>().sprite = sp[r];
        //Text k = load.transform.Find("Text").GetComponent<Text>();
        // k. DOText("LastProject",3);
    }

    void Update()
    {
        if (!f)
        {
            return;

        }
        o += 0.7f;
        if (o < 99)
        {
            ll.transform.Translate(1.5f, 0, 0);
            int u = (int)o;
            t.text = "<color=green>" + u.ToString() + "</color>%";
        }
        else
        {
            t.text = "<color=green>100</color>%";
        }

        if (o > 99)
        {
            Init.SetActive(true);
            Destroy(gameObject, 0.2f);
            f = true;
        }


    }
}
