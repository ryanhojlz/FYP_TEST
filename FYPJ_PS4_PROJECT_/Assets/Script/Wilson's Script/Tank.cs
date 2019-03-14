using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : Attack_Unit
{

    public override void Attack()
    {
        //target = FindNearestUnit(transform.position);
        //base.Attack();
        if (target == null)
            return;

        //Debug.Log("Who is attacking? : " + this.name);
        if (CountDownTimer <= 0)
        {
            CountDownTimer = OriginalTimer;
            target.GetComponent<Minion>().TakeDamage(attackValue);
        }
        else
        {
            CountDownTimer -= Time.deltaTime;
        }

        
        //MeleeAttack();
    }

    public override void Unit_Self_Update()
    {
        if (minionWithinRange.Count > 0)
        {
            this.stateMachine.ChangeState(new AttackState(this, minionWithinRange, Enemy_Tag));
        }

        if (!target)
        {
            ChangeToMoveState();
        }
        else if (target.GetComponent<Minion>().healthValue <= 0)
        {
            ChangeToMoveState();
        }
    }

}
