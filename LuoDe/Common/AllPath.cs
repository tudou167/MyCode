using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPath : Singleton<AllPath>
{
    public string jsonPath = "Json/";
    public string savePath = Application.persistentDataPath;
    public string userPath = Application.persistentDataPath + "/User.json";
    public string userDirectoryPath = Application.persistentDataPath + "/UserData";
    public string prefabsPath = "Prefabs/";
    public string rolePrefabsPath = "Prefabs/Game/Role/";
    public string weaponPrefabsPath = "Prefabs/Game/Weapons/";
    public string uiPrefabsPath = "Prefabs/UI/";
    public string effectPrefabsPath = "Prefabs/Game/Effect/";
    public string dropPrefabsPath = "Prefabs/Game/Drop/";
    public string accountDirectoryPath = Application.persistentDataPath + "/UserData/";

    public string accountPackageDirectoryPath = "/Package/";
    public string accountWarehouseDirectoryPath = "/Warehouse/";
    public string accountSkillDirectoryPath = "/Skill/";
    public string accountRoleDirectoryPath = "/Role/";

    public string animatorPath = "Animations/_MY/";

    public string loadImg = "UI/ToLoadBackdrop";

}
