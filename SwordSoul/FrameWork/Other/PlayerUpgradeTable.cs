using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeTable : CSharpSingletion<PlayerUpgradeTable>
{
    public int level { get; set; }
    public int exp { get; set; }
    public int atk { get; set; }
    public int def { get; set; }
    public int hp { get; set; }
    public int crit { get; set; }

}
