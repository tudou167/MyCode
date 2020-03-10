using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : EffectController
{
    private int triggerTime;
    private float curTime;
    protected override void OnEnable()
    {
        base.OnEnable();   
        triggerTime = 1;
        curTime = 0;
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime>triggerTime)
        {
            curTime = 0;
            RangeCheck();
        }
    }
}
