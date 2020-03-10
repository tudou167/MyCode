using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appCombat : Hero
{

  public  Button attack;
  public  Hero player;
    private new void Awake()
    {
        base.Awake();
       
    }

    void Start () {
        Debug.Log("11111111");
        player = GameObject.FindGameObjectWithTag("hero").GetComponent<Hero>();
        attack = transform.Find("attack/Image").GetComponent<Button>();
       // attack.onClick.AddListener(player.Attk);
    }
	

	void Update () {
		
	}

   
}
