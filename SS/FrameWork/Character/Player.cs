using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AnimalBase
{
    public PlayerDataInfo playerData; 

    private static Player _instance;  

    public static Player Instance
    {
        get
        {
            return _instance;

        }

    }
    public void Awake()
    {

        _instance = this;
    }
  //  public void Start() { }
}
