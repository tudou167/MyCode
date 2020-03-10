using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState//状态
{
    //1.为什么要用uint,有什么区别?
    public abstract uint GetStateID { get; }
    protected Dictionary<uint, FSMTransition> dicTranstion;//传递ID做key,存这个ID的过度状态
    protected Dictionary<uint, uint> mapTranstionState;//传递ID做key,存状态ID
    protected FSM fsm;
    public FSMState()
    {
        dicTranstion = new Dictionary<uint, FSMTransition>();
        mapTranstionState = new Dictionary<uint, uint>();
    }
    
    public void AddTransition(uint transitionID, Func<bool> transitionCondition, uint stateID)
    {
        if (!dicTranstion.ContainsKey(transitionID))
        {
            mapTranstionState.Add(transitionID, stateID);
            dicTranstion.Add(transitionID, new FSMTransition(transitionID, transitionCondition));
        }
        else
        {
            mapTranstionState[transitionID] = stateID;
        }
    }
    public void ReMoveTransition(uint transitionID)
    {
        if (mapTranstionState.ContainsKey(transitionID))
        {
            mapTranstionState.Remove(transitionID);
            dicTranstion.Remove(transitionID);
        }
    }
    //检查转换
    public void CheckTransition(FSM fSM,params object[] _params){
        foreach (FSMTransition transition in dicTranstion.Values)
        {
            if (transition.DoCehckTransition())
            {
                fsm.SwitchState(transition.TransitionID,_params);
            }
        }
    }
    public int GetNextState(uint transitionID)
    {
        if (mapTranstionState.ContainsKey(transitionID))
            return (int)mapTranstionState[transitionID];
        else
            return -1;
    }
    public virtual void EnterState(FSM fSM, uint lastStateID, params object[] _params)
    {
        this.fsm = fSM;
    }
    public virtual void ExitState(uint nextState, params object[] _params)
    {

    }
    public virtual void OnUpdate(params object[] _params)
    {

    }
    public virtual void OnFixedUpdate(params object[] _params)
    {

    }
    public virtual void OnLateUpdate(params object[] _params)
    {

    }
}
