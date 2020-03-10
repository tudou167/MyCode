using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//删除所有子物体，是应该扩展哪个对象？
//扩展能够找到子物体的对象，Transform

public static class ClassTrait
{
    public static void DeleteAllChild(this Transform trf)
    {
        //拿到子元素数量
        int count = trf.childCount;
        //获得所有子物体
        for (int i = count - 1; i > -1; i--)
        {
            //获得子元素
            //Destroy不会在本行代码就会删除GameObject，在本帧流程的最后，删除GameObject
            //DestroyImmediate会立即删除游戏对象，尽量不使用
            Object.Destroy(trf.GetChild(i).gameObject);
        }
    }
}
