using System;
using UnityEngine;

public class FSMTransition//状态过度
{
    private uint transitionID;//传递ID
    private Func<bool> transitionCondition;//过渡条件
    public uint TransitionID { get { return transitionID; } }
    public FSMTransition(uint transitionID, Func<bool> transitionCondition)
    {
        this.transitionID = transitionID;
        this.transitionCondition = transitionCondition;
    }
    public bool DoCehckTransition()
    {
        if (transitionCondition == null)
        {
            Debug.LogWarning("转换条件为Null!");
            return true;
        }
        else
        {
            return transitionCondition();
        }
    }

}
