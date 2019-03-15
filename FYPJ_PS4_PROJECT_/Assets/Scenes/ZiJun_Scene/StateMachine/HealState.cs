using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : IState
{
    Healer_Unit unit;
    List<GameObject> targetList;
    string Ally_Tag;

    public HealState(Healer_Unit _unit, List<GameObject> _Ally, string _Ally_Tag)
    {
        unit = _unit;
        //_unit.target = Enemy;
        //target = _target;
        targetList = _Ally;
        Ally_Tag = _Ally_Tag;
    }



    public void Enter()
    {
        
    }

    public void Execute()
    {
        if (unit.GetTarget() != null)
        {
            unit.FindAllyToHeal();
        }
    }

    public void Exit()
    {
       
    }
}
