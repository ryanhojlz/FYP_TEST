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
        //Debug.Log(_movespeed);
        
        this.agent = _agent;
        this.movespeed = _movespeed;
        this.agent.speed = movespeed;
    }

    public void Enter()
    {
        //Debug.Log(agent.tag);
        this.agent.isStopped = false;
    }

    public void Execute()
    {
        //Debug.Log(agent.velocity);

        Vector3 front = new Vector3(agent.velocity.x, agent.velocity.y, agent.velocity.z);
        front.Normalize();
        front += agent.transform.position;
        


        //new Vector3(agent.velocity.x, agent.velocity.y, agent.velocity.z) + agent.transform.position).normalized()
        if(!(agent.velocity.magnitude <= 0))
        agent.transform.LookAt(front);
    }

    public void Exit()
    {
        //Debug.Log("Exited Move State");
        //this.agent.speed = 0;//Stop the agent from moving outside this state
        this.agent.isStopped = true;
    }
}
