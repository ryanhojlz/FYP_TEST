﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Unit : Minion
{
    protected GameObject target;

    public virtual void Attack()
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
}