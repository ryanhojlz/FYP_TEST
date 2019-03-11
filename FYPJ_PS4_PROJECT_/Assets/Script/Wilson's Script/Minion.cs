using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//base class for enemies
public class Minion : MonoBehaviour
{
    //base attributes of the enemies
    public float startHealthvalue;
    public float healthValue;
    public float attackValue;
    public float defenceValue;
    public float speedValue;
    public float rangeValue;
    public bool isAlive;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        healthValue = startHealthvalue;
    }

    public float GetHealth()
    {
        return healthValue;
    }

    public void SetHealth(float _health)
    {
         healthValue = _health;
    }

    public float GetAttack()
    {
        return attackValue;
    }

    public void SetAttack(float _attack)
    {
        attackValue = _attack;
    }

    public float GetSpeed()
    {
        return speedValue;
    }

    public void SetSpeed(float _speed)
    {
        speedValue = _speed;
    }

    public float GetRange()
    {
        return rangeValue;
    }

    public void SetRange(float _range)
    {
        rangeValue = _range;
    }

    public float GetDefence()
    {
        return defenceValue;
    }

    public void SetDefence(float _defence)
    {
        defenceValue = _defence;
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

    public void SetIsAlive(bool toggle)
    {
        isAlive = toggle;
    }

    public void TakeDamage (float dmgAmount)
    {
        healthValue -= dmgAmount;
        healthBar.fillAmount = healthValue / startHealthvalue;

        if(healthValue <=0)
        {
            SetIsAlive(false);
        }
    }

    public virtual void Attack()
    {

    }

    public virtual void Defend()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
