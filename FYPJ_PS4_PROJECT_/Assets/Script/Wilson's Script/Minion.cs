using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//base class for enemies
public class Minion : MonoBehaviour
{
    //base attributes of the enemies
    protected float startHealthvalue;
    public float healthValue;
    public float attackValue;
    public float defenceValue;
    public float speedValue;
    public float rangeValue;
    public bool isAlive;
    public GameObject[] targetList; //enemy or ally also can
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        startHealthvalue = healthValue;
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

    public void TakeDamage(float dmgAmount)
    {
        healthValue -= dmgAmount;
        

        //Have to multiply by defence value to reduce the damage taken
        if (healthValue <= 0)
        {
            SetIsAlive(false);
        }
    }

    void UpdateHealth()
    {
        healthBar.fillAmount = healthValue / startHealthvalue;
        Debug.Log("Update Health : " + healthValue + " / " + startHealthvalue);
    }

    public virtual void Attack()
    {

    }

    public virtual void Defend()
    {

    }

    public void MeleeAttack()
    {

    }

    public virtual void RangedAttack()
    {

    }

    public GameObject FindNearestUnit(Vector3 unit_position)
    {
        float nearest = float.MaxValue;
        GameObject targetGameObject = null;

        Ray CastToGround = new Ray(unit_position, Vector3.down);
        RaycastHit hit;
        RaycastHit Target_hit;
        Vector3 PlayerPos;
        Physics.Raycast(CastToGround, out hit);//Ray casting the player position

        if (Physics.Raycast(CastToGround, out hit))
        {
            PlayerPos = hit.point;
            

            if (this.tag == "Ally_Unit")//Checking for Ally faction
            {
                targetList = GameObject.FindGameObjectsWithTag("Enemy_Unit");
                
                foreach (GameObject GO in targetList)
                {
                    //Debug.Log(tag);

                    CastToGround = new Ray(GO.transform.position, Vector3.down);
                    Physics.Raycast(CastToGround, out Target_hit);
                    Vector3 waypointPos = Target_hit.point;

                    float dist = (PlayerPos - waypointPos).magnitude;

                    if (dist < nearest)
                    {
                        nearest = dist;
                        targetGameObject = GO;
                        //Debug.Log("Ally found");
                    }
                }

            }
            else
            {
                targetList = GameObject.FindGameObjectsWithTag("Ally_Unit");

                foreach (GameObject GO in targetList)
                {
                    //Debug.Log(tag);

                    CastToGround = new Ray(GO.transform.position, Vector3.down);
                    Physics.Raycast(CastToGround, out Target_hit);
                    Vector3 waypointPos = Target_hit.point;

                    float dist = (PlayerPos - waypointPos).magnitude;

                    if (dist < nearest)
                    {
                        nearest = dist;
                        targetGameObject = GO;
                        //Debug.Log("Enemy found");
                    }
                }
            }
        }

        //Debug.Log(targetGameObject.name);
        return targetGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Attack();
        Debug.Log("Update Health : " + healthValue + " / " + startHealthvalue);
        UpdateHealth();

        if (healthValue <= 0)
        {
            //Debug.Log("He dead boy");
            Destroy(this.gameObject);
        }
    }
}
