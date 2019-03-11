using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Minion
{
    // Start is called before the first frame update
    void Start()
    {
        startHealthvalue = 100;
        attackValue = 30;
        speedValue = 5;
        isAlive = true;
    }

    public override void Attack()
    {
        //base.Attack();
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
