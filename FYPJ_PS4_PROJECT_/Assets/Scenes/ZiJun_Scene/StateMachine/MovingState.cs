using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovingState : IState
{
    NavMeshAgent agent;
    float movespeed;

    public MovingState(NavMeshAgent _agent, float _movespeed)
    {
        this.agent = _agent;
        this.movespeed = _movespeed;
    }

    public void Enter()
    {
        agent.speed = movespeed;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
       
    }
}
