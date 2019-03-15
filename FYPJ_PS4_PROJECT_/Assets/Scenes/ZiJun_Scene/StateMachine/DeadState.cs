using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    Minion unit;
    List<GameObject> targetList;
    string Minion_Tag;

    //need to pass values for the dead state?
    public DeadState(Minion _unit, List<GameObject> minion, string _Minion_Tag)
    {
        unit = _unit;
        //_unit.target = Enemy;
        //target = _target;
        targetList = minion;
        Minion_Tag = _Minion_Tag;
    }

    public void Enter()
    {
        unit.SetIsAlive(false);
    }

    public void Execute()
    {
        unit.Die();
    }

    public void Exit()
    {
        
    }
}
