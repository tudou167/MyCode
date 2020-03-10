using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FightStopController : MonoBehaviour {

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
        tips.text = "是否中止战斗";
        yesBtnText.text = "是";
        noBtnText.text = "否";

        Time.timeScale = 0;
    }
    void Start()
    {
        data = FightTmpManager.Instance.GetData();
    }
    void Yes()
    {
        Time.timeScale = 1;
        FightChange.Instance.callback = DestroySelf;
        data.isBack = true;
        StartCoroutine(FightChange.Instance.LoadScene());
    }
    void No()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
