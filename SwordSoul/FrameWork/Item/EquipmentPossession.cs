using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPossession : ItemInterface
{
    public int id { get { return equipment.id; } }
    public string name { get { return equipment.name; } }
    public Enumclass.ItemType equipmentType { get { return equipment.equipmentType; } }
    public string description { get { return equipment.description; } }
    public Enumclass.Quality quality { get { return equipment.quality; } }
    public Equipment equipment;
}
