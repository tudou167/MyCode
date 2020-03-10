
using System;
using UnityEngine;

public class AllFormula : CSharpSingletion<AllFormula>
{
    public int SellPrice(int buy, int level, int count = 1)
    {
        return ((buy * level) / 2) * count;
    }
    public int[] GetExpInfo(int exp = 0, int curLv = 1, int needExp = 50)
    {

        int temp = exp - needExp;
        if (temp >= 0)
        {
            curLv++;
         
            return GetExpInfo(temp,curLv, needExp *= 2);
        }
        else
        {

            //Debug.Log("现在持有的经验值" + (needExp - Math.Abs(temp)));
            //Debug.Log("升级所需经验" + Math.Abs(temp));
            //Debug.Log("当前等级" + curLv);
            //Debug.Log("下一级需要的经验" + needExp);

            int[] tempArr = { (needExp - Math.Abs(temp)), Math.Abs(temp), curLv, needExp };
            return tempArr;
        }
        //int[] tempArr = { (needExp - Math.Abs(temp)), Math.Abs(temp), curLv, needExp };
        //return tempArr;
        //Debug.Log("现在持有的经验值" + (needExp - Math.Abs(temp)));
        //Debug.Log("升级所需经验" + Math.Abs(temp));
        //Debug.Log("当前等级" + curLv);
        //Debug.Log("下一级需要的经验" + needExp);
    }

    public int GetEquipProperty(int property, int lv)
    {
        return (int)(property * lv * 0.6f);
    }
}
