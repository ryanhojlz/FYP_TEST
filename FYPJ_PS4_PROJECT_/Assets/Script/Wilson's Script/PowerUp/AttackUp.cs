﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : PowerUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        minion_unit.attackValue += 50f;
    }
}
