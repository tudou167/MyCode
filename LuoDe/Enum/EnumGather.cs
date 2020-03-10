public enum ItemQuality
{
    N = 1,
    R,
    SR,
    SSR
}

public enum SceneName
{
    Login = 1,
    HomeScene,
    Fight
}

public enum RoleName
{
    jyg1 = 1
}

public enum RoleType
{
    hero = 1,
    xiaoguai,
    jyg,
    boss

}
public enum LayerType
{
    UI = 1,
    enemy,
    hero,
    wall,
    drop

}

public enum ItemType
{
    //双手剑
    LongHandle = 1,
    //剑盾
    SwordShield,
    //法杖
    Wand,
    //消耗品
    Consumables,
    //防具
    Armor,
    //武器强化石
    WeaponMaterial,
    //防具强化石
    ArmorMaterial,
    //任务物品
    Task,
    //其他
    Other

}

public enum SkillType
{
    //主动
    Initiative = 1,
    //被动
    Passivity
}