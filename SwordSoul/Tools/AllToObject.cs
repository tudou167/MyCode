using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class AllToObject : CSharpSingletion<AllToObject>
{
    public T GetAllUserPackge<T>()
    {
        return JsonMapper.ToObject<T>(AllJson.Instance.userPackageJson());
    }
    public UserInfoList GetUserInfo()
    {
        return JsonMapper.ToObject<UserInfoList>(AllJson.Instance.UserInfoJson());
    }
    public CharacterInfoList GetCharacterInfo()
    {
        return JsonMapper.ToObject<CharacterInfoList>(AllJson.Instance.characterInfojson());
    }
    public PlayerDataInfo GetPlayDataInfo()
    {
        return JsonMapper.ToObject<PlayerDataInfo>(AllJson.Instance.PlayerDataInfoJson());
    }
    public List<EquipmentTemp.Row.Row2> GetItemList()
    {
        return JsonMapper.ToObject<List<EquipmentTemp.Row.Row2>>(AllJson.Instance.ItemListJson());
    }
    public GroceryStore GetShopList()
    {
        return JsonMapper.ToObject<GroceryStore>(AllJson.Instance.shopListJson());
    }

}
