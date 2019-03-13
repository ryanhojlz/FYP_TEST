using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Minion
{
    Gun gun = new Gun(); //Sniper has a gun
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        healthValue = 100;
        attackValue = 30;
        speedValue = 5;
        isAlive = true;
    }

    public override void Attack()
    {
        //base.Attack();
        target = FindNearestUnit(transform.position);
        //base.Attack();
        if (target == null)
            return;

        if (target)
        {
            target.GetComponent<Minion>().TakeDamage(attackValue);
            gun.Shoot();
        }
    }

    public override void Defend()
    {
        //base.Defend();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
