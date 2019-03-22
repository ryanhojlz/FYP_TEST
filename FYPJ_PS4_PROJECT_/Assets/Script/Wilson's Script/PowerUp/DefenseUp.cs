using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUp : PowerUp
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
        minion_unit.defenceValue += 50f;
    }
}
