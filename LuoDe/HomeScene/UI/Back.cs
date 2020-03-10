using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour {
    public enum MyEnum
    {
        off,
        no
    }
    public MyEnum kkk = MyEnum.off; 
    public GameObject Game;
    public float sj = 0.3f;
    public void back()
    {
        Destroy(Game.gameObject,sj);
        
        MoveCamera.isCanRun = true;
        if (kkk == MyEnum.no)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowing>().Music();
        }
    }
}
