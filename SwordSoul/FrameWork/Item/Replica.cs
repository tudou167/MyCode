using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replica : CSharpSingletion<Replica>
{
    // {-关卡ID
    // -关卡名称
    // -关卡描述
    // -关卡级数
    // -怪物ID列表
    // -怪物ID等级列表
    public int id { get; set; }
    public int name { get; set; }
    public int description { get; set; }
    public int level { get; set; }
    public List<int> EnemyId { get; set; }
    public List<int> EnemyLevel { get; set; }
}
