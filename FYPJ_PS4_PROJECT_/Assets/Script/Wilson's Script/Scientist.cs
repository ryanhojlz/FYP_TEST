using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : Minion
{
    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
