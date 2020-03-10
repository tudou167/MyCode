using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdate : MonoBehaviour
{
    private Text TimeUpdateText;
    void Awake()
    {
        TimeUpdateText = transform.Find("TimeTips/Time").GetComponent<Text>();
    }

    void Start()
    {
        StartCoroutine(GetTime());//时刻更新时间
    }
    private int hour = 0;
    private int minute = 0;
    private int second = 0;
    IEnumerator GetTime()
    {
        WWW www = new WWW("http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=1");
        yield return www;

        if (www.text == "" || www.text.Trim() == "")//如果获取网络时间失败,改为获取系统时间
        {
            TimeUpdateText.text = System.DateTime.Now.Month + "_" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "_" +System.DateTime.Now.Minute + "_" + System.DateTime.Now.Second;
            hour = System.DateTime.Now.Hour;
            minute = System.DateTime.Now.Minute;
            second = System.DateTime.Now.Second;
        }
        else//成功获取网络时间
        {
            string timeStr = www.text.Substring(2);
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds(Convert.ToDouble(timeStr));
            timeStr = time.ToString();
            TimeUpdateText.text = time.Month + "_" + time.Day + "_" + time.Hour +"_" + time.Minute + "_" + time.Second;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
        }
        InvokeRepeating("Subtract",0, 1);
    }
    private void Subtract()
    {
        second++;
        if (second == 60)
        {
            second = 0;
            minute++;
        }

        if (minute == 60)
        {
            minute = 0;
            hour++;
        }
        if (hour == 24)
        {
            hour = 0;
        }

        TimeUpdateText.text = "剩：" + (23 - hour) + "时 " + (59 - minute) + "分 " + (60 - second) + "秒";

        //TimeUpdateText.text = hour + "_" + minute + "_" + second;

    }

}