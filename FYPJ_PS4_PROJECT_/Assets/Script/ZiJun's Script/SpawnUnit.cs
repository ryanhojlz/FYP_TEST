﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public GameObject UnitToSpawn;
    bool hasSpawn = false;

    //static bool unitspawned = false;
   
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(landingRay, out hit, 1f))
        {
            if (!hasSpawn)
            {
                hasSpawn = true;
                SpawningUnit();

                Destroy(this.gameObject);
            }
        }

        
    }

    void SpawningUnit()
    {
        GameObject thisnew = Instantiate(UnitToSpawn, this.transform);
        thisnew.transform.parent = null;
        //Destroy(this.gameObject);
    }
}