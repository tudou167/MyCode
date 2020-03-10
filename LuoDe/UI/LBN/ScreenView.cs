using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenView : MonoBehaviour
{

    void Start()
    {
        RoleData cur = HeroManager.Instance.GetCurHeroData();
        int[] arr = Tool.Instance.LvCheck(cur.BaseExp, cur.Exp, cur.LV);
        transform.Find("PlayerInformation/ID/id").GetComponent<Text>().text = "昵称：<color=yellow>" + cur.Name + "</color>";
        transform.Find("PlayerInformation/ID/lv").GetComponent<Text>().text = "等级：<color=red>   " + arr[3] + "</color>";

    }
}
