using System.Collections.Generic;
using UnityEngine.Events;

public class ItemData
{
    public int ID;
    public string Name;
    public string Icon;
    public string Prefab;
    public int Price;
    public string Detail;
    public ItemQuality Quality;
    public ItemType Type;
    public int ATK;
    public int DEF;
    public int ATKSpeed;
    public int Crit;
    //默认进退/硬直(Repel)
    public int Repel;
    //物品效果委托
    public UnityAction<UnityEngine.Transform,string> DelegateFunc;
    //物品效果委托名称
    public string DelegateName;
    //物品效果执行参数
    public string DelegateParameter;
}
