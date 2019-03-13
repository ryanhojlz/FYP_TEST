﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : Minion
{
    public GameObject[] healTargetList;
    // Start is called before the first frame update
    void Start()
    {
        healthValue = 100;
        startHealthvalue = healthValue;
        attackValue = 30;
        speedValue = 5;
        isAlive = true;
    }

    public override void Defend()
    {
        //base.Defend();
    }

    public void Heal(float HealValue)
    {
        if (healthValue < startHealthvalue)
        {
            if ((HealValue + healthValue) > startHealthvalue)
            {
                healthValue = startHealthvalue;
                return;
            }
            healthValue += HealValue;
        }
    }

    public override void FindAllyToHeal()
    {
        //base.FindAllyToHeal();
        healTargetList = GameObject.FindGameObjectsWithTag("Ally_Unit");

        foreach (GameObject GO in healTargetList)
        {
            //how to search through the list and find the unit with the lowest hp?
            if (healthValue < 30)//only find units with health value less than 20?
            {
                Heal(60f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
