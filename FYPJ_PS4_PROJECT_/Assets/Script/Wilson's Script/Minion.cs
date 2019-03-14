using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//base class for enemies
public class Minion : MonoBehaviour
{
    //base attributes of the enemies
    protected float startHealthvalue;
    public float healthValue;
    public float attackValue;
    public float defenceValue;
    public float attackSpeedValue;
    public float moveSpeedValue;
    public float rangeValue;
    public bool isAlive;
    public GameObject[] targetList; //enemy or ally also can
    public Image healthBar;
    public GameObject meleeProjectile;

    public List<GameObject> minionWithinRange;//Keep track of which unit is within range

    public SphereCollider TriggerRange;
    //public CylinderCollider TriggerRange;

    protected float CountDownTimer;
    protected float OriginalTimer;

    protected StateMachine stateMachine = new StateMachine();

    public string Enemy_Tag;
    public string Ally_Tag;

    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "Ally_Unit")
        {
            Enemy_Tag = "Enemy_Unit";
            Ally_Tag = "Ally_Unit";
        }
        else if (this.tag == "Enemy_Unit")
        {
            Enemy_Tag = "Ally_Unit";
            Ally_Tag = "Enemy_Unit";
        }

        isAlive = true;
        startHealthvalue = healthValue;
        TriggerRange.radius = rangeValue;

        if (attackSpeedValue > 0)
        {
            CountDownTimer = 1 / attackSpeedValue;
            //Debug.Log("CountDownTimer : " + CountDownTimer);
        }
        else
        {
            //If is less then 0, it should never attack
            CountDownTimer = float.MaxValue;
        }

        OriginalTimer = CountDownTimer;

        ChangeToMoveState();

        //Debug.Log("Start_Minion");
    }

    protected void ChangeToMoveState()
    {
        this.stateMachine.ChangeState(new MovingState(this.GetComponent<NavMeshAgent>(), moveSpeedValue));//state machine
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

    public float GetAttackSpeed()
    {
        return attackSpeedValue;
    }

    public void SetAttackSpeed(float _speed)
    {
        attackSpeedValue = _speed;
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
        //Debug.Log("Update Health : " + healthValue + " / " + startHealthvalue);
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

        stateMachine.ExecuteStateUpdate();

        //Debug.Log("Countdown : " + CountDownTimer);

        UpdateHealth();
        Unit_Self_Update();

        if (healthValue <= 0)
        {
            minionWithinRange.Remove(this.gameObject);
            
            //Destroy(this.gameObject);
        }
        //Debug.Log(this.name + "How many in list : " + minionWithinRange.Count);

    }

    public virtual void Unit_Self_Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //For Making sure if the object is already added, don't add again
        for (int i = 0; i < minionWithinRange.Count; ++i)
        {
            if (other.gameObject == minionWithinRange[i])
            {
                return;
            }
        }

        if (other.tag == "Ally_Unit" || other.tag == "Enemy_Unit")
        {
            minionWithinRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {

        for (int i = 0; i < minionWithinRange.Count; ++i)
        {
            if (minionWithinRange[i] == other.gameObject)
                minionWithinRange.Remove(minionWithinRange[i]);
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, rangeValue);
    //}
}
