using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadState : IState
{
    Minion unit;
    NavMeshAgent agent;

    //need to pass values for the dead state?
    public DeadState(NavMeshAgent _agent, Minion _unit)
    {
        unit = _unit;
        agent = _agent;
    }

    public void Enter()
    {
        unit.SetIsActive(false);
    }

    public void Execute()
    {
        //Debug.Log("Destroying");
        unit.Die();
    }

    public void Exit()
    {
        
    }
}
