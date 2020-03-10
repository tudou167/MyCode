using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM//状态的列表
{
    private Dictionary<uint, FSMState> dicState;//用状态ID存状态
    private bool pause = false;

    public bool Pause
    {
        get
        {
            return pause;
        }

        set
        {
            pause = value;
        }
    }
    private FSMState defaultState;
    private FSMState currentState;
    public FSMState DefaultState
    {
        get
        {
            return defaultState;
        }

        set
        {
            defaultState = value;
        }
    }

    public FSMState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }
    public FSM(FSMState _dafaultState)
    {
        dicState = new Dictionary<uint, FSMState>();
        defaultState = _dafaultState;
        currentState = defaultState;
        pause = false;
    }
    public void AddState(FSMState state)
    {
        if (!dicState.ContainsKey(state.GetStateID))
            dicState.Add(state.GetStateID, state);
    }
    public void ReMoveState(FSMState state){
        if(dicState.ContainsKey(state.GetStateID))
            dicState.Remove(state.GetStateID);
    }
    public FSMState GetState(uint stateID){
        if(dicState.ContainsKey(stateID))
            return dicState[stateID];
        else
            return null;
    }
    //开关状态
    public void SwitchState(uint transitionID,params object[] _params)
    {
        if (currentState==null)return;
        uint lastStateID=currentState.GetStateID;
        int stateID=currentState.GetNextState(transitionID);
        if (stateID==-1)
        {
            return;
        }
        uint nextStateID=(uint)stateID;
        this.currentState.ExitState(nextStateID,_params);
        if (nextStateID==defaultState.GetStateID)
        {
            this.currentState=this.defaultState;
        }else
        {
            this.currentState=this.dicState[nextStateID];
        }
        this.currentState.EnterState(this,lastStateID,_params);

    }
    public void ForceSwitchState(uint newStateID,params object[] _params){
        uint lastStateID=currentState.GetStateID;
        uint nextStateID=(uint) newStateID;
        this.currentState.ExitState(nextStateID,_params);
        if (nextStateID==defaultState.GetStateID)
            this.currentState=this.defaultState;
        else
            this.currentState=this.dicState[nextStateID];

        this.currentState.EnterState(this,lastStateID,_params);
    }
    public void OnUpdate(params object[] _params){
        if (currentState!=null&&!pause)
        {
            currentState.OnUpdate(_params);
            this.currentState.CheckTransition(this,_params);
        }
    }
    public void OnFixedUpdate(params object[] _params){
        if (currentState!=null&&!pause)
        {
            currentState.OnFixedUpdate(_params);
        }
    }
    public void OnLateUpdate(params object[] _params){
        if (currentState!=null&&!pause)
        {
            currentState.OnLateUpdate(_params);
        }
    }

}
