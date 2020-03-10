using UnityEngine;
public class  AllPaths : CSharpSingletion<AllPaths>
{
    //json
    //public string AccountJsonPath = Application.persistentDataPath + "/AccountJson.json";
    public string AccountJsonPath { get { return Application.persistentDataPath + "/AccountJson.json"; } }
    //public string itemPath = Application.persistentDataPath + "/json/item.json";
    //public string initPath = "Json/initRoleData";
    //装备图标路径
    public string itemJsonPath { get { return Application.dataPath + "/Resources/Json/"; } }
    public string CharacterInfoPath { get { return "Json/CharacterInfo"; } }

    //装备图标路径
    public string allIconPath { get { return "UI/All/"; } }
    //消耗品图标路径
    public string consumeIconPath { get { return "UI/Consume/"; } }
    //强化材料图标路径
    public string intensifyIconPath { get { return "UI/Intensify/"; } }
    //任务物品图标路径
    public string missionPropsIconPath { get { return "UI/MissionProps/"; } }
    //玩家背包路径 静态 查询
    public string fileUserPackageStaticPath { get { return Application.dataPath + "/Resources/Json/UserPackageInit.json"; } }
    //玩家背包路径 静态 初始化
    public string getUserPackageStaticPath { get { return "Json/UserPackageInit"; } }
    //玩家背包路径 动态
    public string userPackagePath { get { return Application.persistentDataPath + "/UserPackage.json"; } }
    //物品静态表
    public string itemList { get { return "Json/ItemList"; } }
    //玩家背包预制体路径
    public string packagePrefabs { get { return "Prefabs/Package/"; } }
    //公共预制体路径
    public string commonPrefabs { get { return "Prefabs/Common/"; } }
    //用户属性预制体
    public string roleaAllStatusPrefabs { get { return "Prefabs/RoleAllStatus/"; } }
    //商店相关预制体文件夹
    public string shopPrefabs { get { return "Prefabs/Shop/"; } }
    //抽卡相关预制体文件夹
    public string lotteryPrefabs { get { return "Prefabs/Lottery/"; } }
    //商店静态表
    public string shopList { get { return "Json/ShopList"; } }
    //玩家属性预制体路径
    public string Reg_logPrefabs { get { return "Prefabs/Reg_log/"; } }

    //npc 头像图标路径
    public string npc_image { get { return "UI/npc_image"; } }
    
    public string PlayerDataInfoPath { get { return "Json/PlayerDataInfo"; } }


}