using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill
{

    private int id;
    private string name;
    private float cd;
    private int level;

    private string skillDelegateName;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public float Cd
    {
        get
        {
            return cd;
        }

        set
        {
            cd = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public string SkillDelegateName
    {
        get
        {
            return skillDelegateName;
        }

        set
        {
            skillDelegateName = value;
        }
    }
}
