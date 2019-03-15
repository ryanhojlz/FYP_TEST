using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_Unit : Minion
{

    public virtual void Attack()
    {

    }

    public virtual void Healing(float HealValue)
    {

    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public virtual void FindAllyToHeal()
    {

    }
}
