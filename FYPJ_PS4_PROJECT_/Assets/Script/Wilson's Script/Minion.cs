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
        healthBar.fillAmount = healthValue / startHealthvalue;

        //Have to multiply by defence value to reduce the damage taken
        if (healthValue <= 0)
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

    public void MeleeAttack()
    {

    }

    public virtual void RangedAttack()
    {

    }

    public GameObject FindNearestUnit(Vector3 unit_position)
    {
        float nearest = float.MaxValue;
        GameObject targetGameObject = new GameObject();

        Ray CastToGround = new Ray(unit_position, Vector3.down);
        RaycastHit hit;
        Vector3 PlayerPos;
        //nearestpoint = unit_position;//Initing the pos for nearest point to be it's own point

        if (this.tag == "Ally_Unit")
        {
            if (Physics.Raycast(CastToGround, out hit))
            {
                //Playerpos is raycasted to floor value
                PlayerPos = hit.point;
                targetList = GameObject.FindGameObjectsWithTag("Enemy_Unit");

                foreach (GameObject TargetObject in targetList)
                {
                    Vector3 waypointPos = TargetObject.GetComponent<WaypointClass>().GetRayCast();
                    float dist = (PlayerPos - waypointPos).magnitude;

                    if (dist < nearest)
                    {
                        nearest = dist;
                        targetGameObject = TargetObject;
                    }
                }
            }
        }
        else if (this.tag == "Enemy_Unit")
        {
            if (Physics.Raycast(CastToGround, out hit))
            {
                //Playerpos is raycasted to floor value
                PlayerPos = hit.point;
                targetList = GameObject.FindGameObjectsWithTag("Ally_Unit");

                foreach (GameObject TargetObject in targetList)
                {
                    Vector3 waypointPos = TargetObject.GetComponent<WaypointClass>().GetRayCast();
                    float dist = (PlayerPos - waypointPos).magnitude;

                    if (dist < nearest)
                    {
                        nearest = dist;
                        targetGameObject = TargetObject;
                    }
                }
            }
        }

        return targetGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
