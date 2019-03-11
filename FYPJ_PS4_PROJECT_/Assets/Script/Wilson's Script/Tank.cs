using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Minion
{
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
        GameObject target = FindNearestUnit(transform.position);
        //base.Attack();
        target.GetComponent<Minion>().TakeDamage(attackValue);
        MeleeAttack();
    }

    public override void Defend()
    {
        //base.Defend();
    }

    public override void TakeDamage(float dmgAmount)
    {
        healthValue -= dmgAmount;
        healthBar.fillAmount = healthValue / startHealthvalue;

        //Have to multiply by defence value to reduce the damage taken
        if (healthValue <= 0)
        {
            SetIsAlive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
