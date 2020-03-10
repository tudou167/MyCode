using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    // -id
    public int id{get;set;}
    // -名称
    public string name{get;set;}
    // -初始强化等级
    public int InitialLevel{get;set;}
    // -攻击力
    public int atk{get;set;}
    // -防御力
    public int def{get;set;}
    // -暴击
    public int idcrit{get;set;}
    // -品质
    public Enumclass.Quality quality;
    // -最大血量
    public int hp{get;set;}
    //购入价格
    public int buy{get;set;}
    // -装备类型
    public Enumclass.ItemType equipmentType{get;set;}
    // -描述
    public string description{get;set;}
}
