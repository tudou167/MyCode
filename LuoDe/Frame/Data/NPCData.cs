
public class NPCData
{
    public int ID;
    public string Name;
    public string Icon;
    public string Prefab;
    //出售的物品列表id 如 10001#20001 # 分割
    public string ItemList;

    public NPCData() { }
    public NPCData(int id,string name,string icon,string prefab,string itemList)
    {
        ID = id;
        Name = name;
        Icon = icon;
        Prefab = prefab;
        ItemList = itemList;
    }
}
