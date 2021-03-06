﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    Attack_Unit unit;
    List<GameObject> targetList;
    string Enemy_Tag;

    public AttackState(Attack_Unit _unit, List<GameObject> _Enemy, string _Enemy_Tag)//assume that passing of values is needed
    {
        unit = _unit;
        //_unit.target = Enemy;
        //target = _target;
        targetList = _Enemy;
        Enemy_Tag = _Enemy_Tag;
    }

    private float CheckDist(Vector3 pos1, Vector3 pos2)//Does not metter which is first
    {
        float dist = float.MaxValue;

        dist = (pos1 - pos2).magnitude;

        return dist;
    }

    public void Enter()//Assign A unitt
    {
        float distNearest = float.MaxValue;
        float tempDst;
        //spawn melee projectile
        if (targetList.Count > 0)
        {
            for (int i = 0; i < targetList.Count; ++i)
            {
                if (!targetList[i])//If is null skip this one
                    continue;

                if (targetList[i].tag != Enemy_Tag)//If is not enemy skip as well
                    continue;

                if (!unit.CheckWithinRange(targetList[i].transform))//If not within attack range, skip
                    continue;

                if (targetList[i].GetComponent<BasicGameOBJ>().OBJ_TYPE == BasicGameOBJ.OBJType.MINION)
                {
                    bool targetable = false;

                    for (int j = 0; j < unit.targetableType.Count; ++j)
                    {
                        if (targetList[i].GetComponent<Minion>().MinionType == unit.targetableType[j])
                        {
                            targetable = true;
                            break;//Once found, no point carry on the loop
                        }

                    }

                    if (!targetable)
                        continue;
                }

                tempDst = unit.CheckDist(targetList[i].transform);

                if (tempDst >= distNearest)//If is longer then what is already targeted
                    continue;
                distNearest = tempDst;
                unit.SetTarget(targetList[i]);// = targetList[i];
            }
        }
    }

    public void Execute()
    {
       

        if (!unit.GetTarget())
        {
            return;
        }

        unit.gameObject.transform.LookAt(unit.GetTarget().transform.position);

        NavMeshAgent agent = unit.gameObject.GetComponent<NavMeshAgent>();

        if (!unit.CheckWithinRange(unit.GetTarget().transform))//If not within attack range
        {
            agent.isStopped = false;
            agent.SetDestination(unit.GetTarget().gameObject.transform.position);

            //if (unit.tag == "Ally_Unit" && unit.GetTarget())
            //    Debug.Log(unit.tag + " : " + unit.GetTarget().name);
            //Debug.Log(unit.tag + " : " + Enemy_Tag);
            //agent.speed = unit.moveSpeedValue;
            //return;
        }
        else
        {
            agent.isStopped = true;
            if (unit.GetTarget() != null)
            {
                unit.Attack();
            }
        }
            //unit.SetTarget(null);
    }

    public void Exit()
    {

    }

}
