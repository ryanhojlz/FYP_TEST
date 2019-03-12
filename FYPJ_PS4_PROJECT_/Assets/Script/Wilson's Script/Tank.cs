using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Minion
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        //healthValue = 100;
        //attackValue = 30;
        //speedValue = 5;
        isAlive = true;
        startHealthvalue = healthValue;
        
    }

    public override void Attack()
    {
        target = FindNearestUnit(transform.position);
        //base.Attack();
        if (target == null)
            return;

        if(target)
        target.GetComponent<Minion>().TakeDamage(attackValue);
        //MeleeAttack();
    }

    public override void Defend()
    {
        //base.Defend();
    }

    // Update is called once per frame
    //void  Update()
    //{
        //Attack();
    //}
}
