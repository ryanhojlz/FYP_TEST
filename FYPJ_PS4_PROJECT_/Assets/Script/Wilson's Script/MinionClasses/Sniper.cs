using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Attack_Unit
{
    public GameObject bulletPrefab;
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

        if (!target)
        {
            ChangeToMoveState();
        }
        else if (target.GetComponent<Minion>().healthValue <= 0)
        {
            ChangeToMoveState();
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        attackSound.Play();

        if (bullet != null)
            bullet.Seek(target.transform);
    }
}
