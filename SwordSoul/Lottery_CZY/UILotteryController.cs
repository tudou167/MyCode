using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILotteryController : MonoBehaviour
{
    private int clickNum;
    private int openBox = 10;
    private Animator ani;
    private Text tips;
    private Transform prize;

    void Awake()
    {
        ani = transform.Find("Box").GetComponent<Animator>();
        tips = transform.Find("Tips").GetComponent<Text>();
        prize = transform.Find("Prize").transform;
    }
    void Start()
    {
        InvokeRepeating("Reduce",0, 0.25f);
    }

    int check = 0;
    public void BoxClick()
    {
        ani.transform.DOShakePosition(8,8);
        tips.transform.DOShakePosition(3, 12);

        tips.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        if (clickNum == openBox)
        {
            if (check != 0)
            {
                return;
            }
            check++;
            EquipmentTemp.Row.Row2 temp = UIPackageModel.Instance.GainItem();

            prize.GetComponent<Image>().sprite = Resources.Load<Sprite>(AllPaths.Instance.allIconPath + temp.id);
            UIPackageModel.Instance.InItem(temp);
            prize.Find("Text").GetComponent<Text>().text = temp.name;

            prize.gameObject.SetActive(true);
            ani.SetBool("isPlay",true);
            prize.DOScale(Vector3.one, 0.4f);
            prize.DOLocalMoveY(310, 0.4f);
        }
        else {
            ++clickNum;
        }
    }
    public void BoxAgain()
    {
        clickNum = 0;
        ani.SetBool("isPlay", false);
        prize.DOLocalMoveY(-310, 0);
        prize.DOScale(Vector3.zero, 0);

        prize.gameObject.SetActive(false);
    }
    public void BoxClose()
    {
        Destroy(gameObject);
    }

    private void Reduce()
    {
        if (clickNum > 0 && clickNum != openBox)
        {
            --clickNum;
        }
    }
}
