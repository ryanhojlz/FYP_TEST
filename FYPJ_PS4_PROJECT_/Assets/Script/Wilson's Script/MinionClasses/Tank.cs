﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : Attack_Unit
{
    public GameObject meleeProjectile;
    public AudioSource attackSound;

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
            Shoot();
            //target.GetComponent<Minion>().TakeDamage(attackValue);
        }
        else
        {
            CountDownTimer -= Time.deltaTime;
        }

    }

    public override void Unit_Self_Update()
    {
        if (minionWithinRange.Count > 0)
        {
            this.stateMachine.ChangeState(new AttackState(this, minionWithinRange, Enemy_Tag));
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(meleeProjectile, this.transform.position, this.transform.rotation);
        MeleeProjectile bullet = bulletGO.GetComponent<MeleeProjectile>();
        //attackSound.Play();

        if (bullet != null)
        {
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }

}
