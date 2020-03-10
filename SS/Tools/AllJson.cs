using UnityEngine;
using System.IO;
public class AllJson : CSharpSingletion<AllJson>
{
    public string characterInfojson() { return Resources.Load<TextAsset>(AllPaths.Instance.CharacterInfoPath).text; }
    public string userPackageStaticJson() { return Resources.Load<TextAsset>(AllPaths.Instance.getUserPackageStaticPath).text; }
    public string userPackageJson()
    {
        if (!File.Exists(AllPaths.Instance.userPackagePath))
        {
            File.WriteAllText(AllPaths.Instance.userPackagePath, "{\"User\": []}");
        }
        return File.ReadAllText(AllPaths.Instance.userPackagePath);
           
    }
    public string PlayerDataInfoJson() { return Resources.Load<TextAsset>(AllPaths.Instance.PlayerDataInfoPath).text; }
    public string UserInfoJson() { return File.ReadAllText(AllPaths.Instance.AccountJsonPath); }

    public string ItemListJson() { return Resources.Load<TextAsset>(AllPaths.Instance.itemList).text; }
    public string shopListJson() { return Resources.Load<TextAsset>(AllPaths.Instance.shopList).text; }
}

