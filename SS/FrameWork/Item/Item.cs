using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // -id
    public int id { get; set; }
    // -名称
    public string name { get; set; }
    // -物品类型 : 物品类型
    public Enumclass.ItemType itemType { get; set; }
    // -品质
    public Enumclass.Quality quality { get; set; }
    // -描述
    public string description { get; set; }
    // -血量
    public int hp { get; set; }
    // -攻击力
    public int atk { get; set; }
    // -防御力
    public int def { get; set; }
    // -暴击率
    public int crit { get; set; }
    // -持续时间
    public float continuedTime { get; set; }
    // -执行委托名称
    public string delegaName { get; set; }
    // -执行委托
    //TODO
    // -间隔时间
    public float intervals { get; set; }
    //购入价格
    public int buy { get; set; }

}
