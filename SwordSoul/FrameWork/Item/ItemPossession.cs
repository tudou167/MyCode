using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPossession:ItemInterface{
	public int id{get{return item.id;}}
    public int num{get{return item.id;}}
    public string name{get{return item.name;}}
    public Enumclass.ItemType itemType {get{return item.itemType;}}// -物品类型
    public string description{get{return item.description;}}// -描述
    public Enumclass.Quality quality{get{return item.quality;}}// -品质
    public Item item;
}
