using System.Collections;
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

    public void Enter()//Assign A unitt
    {
        //spawn melee projectile
        if (targetList.Count > 0)
        {
            for (int i = 0; i < targetList.Count; ++i)
            {
                if (!targetList[i])
                    continue;

                if (targetList[i].tag == Enemy_Tag)
                {
                    unit.SetTarget(targetList[i]);// = targetList[i];
                }
            }
        }
    }

    public void Execute()
    {
        if (unit.GetTarget() != null)
        {
            unit.Attack();
        }
    }

    public void Exit()
    {
        
    }

}
