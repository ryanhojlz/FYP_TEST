using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Minion
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        startHealthvalue = healthValue;

        if (attackSpeedValue > 0)
        {
            CountDownTimer = 1 / attackSpeedValue;
            Debug.Log("CountDownTimer : " + CountDownTimer);
        }
        else
        {
            //If is less then 0, it should never attack
            CountDownTimer = float.MaxValue;
            Debug.Log("CountDownTimer : " + CountDownTimer);
        }

        OriginalTimer = CountDownTimer;
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
