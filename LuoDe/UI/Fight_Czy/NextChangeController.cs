using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NextChangeController : MonoBehaviour
{
    Text tips;
    Button yesBtn;
    Button noBtn;
    Text yesBtnText;
    Text noBtnText;

    FightTmpData data;

    void Awake()
    {
        tips = transform.Find("Tips").GetComponent<Text>();
        yesBtn = transform.Find("Yes").GetComponent<Button>();
        noBtn = transform.Find("No").GetComponent<Button>();
        yesBtnText = yesBtn.transform.Find("Text").GetComponent<Text>();
        noBtnText = noBtn.transform.Find("Text").GetComponent<Text>();
        yesBtn.onClick.AddListener(Yes);
        noBtn.onClick.AddListener(No);
        tips.text = "是否进入下一层";
        yesBtnText.text = "是";
        noBtnText.text = "否";
    }
    void Start()
    {
        data = FightTmpManager.Instance.GetData();
        if (data.isBack) tips.text = "是否返回城镇 <color=red> （选否会直接进入下一层） </color>";
        if (data.curPlieNum == 15) tips.text = " <color=red> 恭喜通关 </color>";
    }
    void Yes()
    {
        FightChange.Instance.callback = DestroySelf;
        StartCoroutine(FightChange.Instance.LoadScene());
    }
    void No()
    {
        FightTmpData data = FightTmpManager.Instance.GetData();
        if (data.isBack)
        {
            if (data.curPlieNum == 15)
            {
                Destroy(gameObject);
                return;
            }
            data.isBack = false;
            FightChange.Instance.callback = DestroySelf;
            StartCoroutine(FightChange.Instance.LoadScene());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
