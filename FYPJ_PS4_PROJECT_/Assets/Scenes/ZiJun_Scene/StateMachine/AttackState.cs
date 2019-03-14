using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    NavMeshAgent agent;
    float attackValue;

    public AttackState(NavMeshAgent _agent, float _attackValue)//assume that passing of values is needed
    {
        this.agent = _agent;
        this.attackValue = _attackValue;
    }

    public void Enter()
    {
        //spawn melee projectile
    }

    public void Execute()
    {
        //throw melee projectile
        //give timer for projectile befoe the projectile gets destoryed
        //load animation of the melee attack
    }

    public void Exit()
    {
        
    }    
}
