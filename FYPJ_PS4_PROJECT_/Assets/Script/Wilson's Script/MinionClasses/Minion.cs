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
    public bool isActive;
    public GameObject[] targetList; //enemy or ally also can
    public Image healthBar;
    public GameObject meleeProjectile;

    public List<GameObject> minionWithinRange;//Keep track of which unit is within range

    public SphereCollider TriggerRange;
    //public CylinderCollider TriggerRange;

    protected float CountDownTimer;
    protected float OriginalTimer;
    protected GameObject target;

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

        isActive = true;
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

    public float GetCountDownTimer()
    {
        return CountDownTimer;
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

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive(bool toggle)
    {
        isActive = toggle;
    }

    public void Die()
    {
        //Can add timer and other stuff
        if (isActive == false) 
        {
            //Debug.Log("Destroying");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmgAmount)
    {
        healthValue -= dmgAmount;
        

        //Have to multiply by defence value to reduce the damage taken
        if (healthValue <= 0)
        {
            SetIsActive(false);
        }
    }

    void UpdateHealth()
    {
        healthBar.fillAmount = healthValue / startHealthvalue;
        //Debug.Log("Update Health : " + healthValue + " / " + startHealthvalue);
    }

    //public GameObject FindNearestUnit(Vector3 unit_position)
    //{
    //    float nearest = float.MaxValue;
    //    GameObject targetGameObject = null;

    //    Ray CastToGround = new Ray(unit_position, Vector3.down);
    //    RaycastHit hit;
    //    RaycastHit Target_hit;
    //    Vector3 PlayerPos;
    //    Physics.Raycast(CastToGround, out hit);//Ray casting the player position

    //    if (Physics.Raycast(CastToGround, out hit))
    //    {
    //        PlayerPos = hit.point;
            

    //        //if (this.tag == "Ally_Unit")//Checking for Ally faction
    //        //{
    //        //    targetList = GameObject.FindGameObjectsWithTag("Enemy_Unit");
                
    //        //    foreach (GameObject GO in targetList)
    //        //    {
    //        //        //Debug.Log(tag);

    //        //        CastToGround = new Ray(GO.transform.position, Vector3.down);
    //        //        Physics.Raycast(CastToGround, out Target_hit);
    //        //        Vector3 waypointPos = Target_hit.point;

    //        //        float dist = (PlayerPos - waypointPos).magnitude;

    //        //        if (dist < nearest)
    //        //        {
    //        //            nearest = dist;
    //        //            targetGameObject = GO;
    //        //            //Debug.Log("Ally found");
    //        //        }
    //        //    }

    //        //}
    //        //else
    //        //{
    //        //    targetList = GameObject.FindGameObjectsWithTag("Ally_Unit");

    //        //    foreach (GameObject GO in targetList)
    //        //    {
    //        //        //Debug.Log(tag);

    //        //        CastToGround = new Ray(GO.transform.position, Vector3.down);
    //        //        Physics.Raycast(CastToGround, out Target_hit);
    //        //        Vector3 waypointPos = Target_hit.point;

    //        //        float dist = (PlayerPos - waypointPos).magnitude;

    //        //        if (dist < nearest)
    //        //        {
    //        //            nearest = dist;
    //        //            targetGameObject = GO;
    //        //            //Debug.Log("Enemy found");
    //        //        }
    //        //    }
    //        //}
    //    }

    //    //Debug.Log(targetGameObject.name);
    //    return targetGameObject;
    //}

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();

        stateMachine.ExecuteStateUpdate();

        //Debug.Log("Countdown : " + CountDownTimer);

       
        Unit_Self_Update();//Update for indivisual units (unique to 1 unit) 
        UpdateCheckList();//Checking for unit in the list. If it is not active, remove it
        CheckTargetActive();//Check if your target is active. If not active target becomes null


        if (!target)
        {
            ChangeToMoveState();
        }

        if (healthValue <= 0)
        {
            //target = null;
            //minionWithinRange.Remove(this.gameObject);
            //Destroy(this.gameObject);
            //Destroy(gameObject.GetComponent<CapsuleCollider>());
            //Destroy(gameObject.GetComponent<Rigidbody>());
            //Destroy(gameObject.GetComponent<NavMeshAgent>());

            target = null;
            //this.SetIsActive(false);

            this.stateMachine.ChangeState(new DeadState(this.GetComponent<NavMeshAgent>(), this));//state machine

            //Physics.IgnoreCollision(this.GetComponent<Collider>());
        }
        //Debug.Log(this.name + "How many in list : " + minionWithinRange.Count);
        //if (!isActive)
        //{
        //    Destroy(this.gameObject);
        //}
    }

    void UpdateCheckList()
    {
        for (int i = 0; i < minionWithinRange.Count; ++i)
        {
            if (!minionWithinRange[i])
            {
                RemoveUnitFromList(minionWithinRange[i]);
                //RemoveUnitFromList(minionWithinRange[i].GetComponent<Minion>());
                continue;
            }

            if (!minionWithinRange[i].GetComponent<Minion>().GetIsActive())
            {
                RemoveUnitFromList(minionWithinRange[i]);
            }

        }
    }

    void CheckTargetActive()
    {
        if (!target)
            return;

        if (!target.GetComponent<Minion>().GetIsActive())
        {
            target = null;
        }
    }

    public void RemoveUnitFromList(GameObject unit)
    {
        minionWithinRange.Remove(unit);
    }

    public bool CheckMinionWithinRange(Minion unit)
    {
        if (this.gameObject == unit)//Unlikely happen but just in-case it detect itself in list somehow
            return false;

        Ray ThisToGround = new Ray(this.gameObject.transform.position, Vector3.down);
        Ray TargetToGround = new Ray(unit.gameObject.transform.position, Vector3.down);

        RaycastHit ThisPos;
        RaycastHit TargetPos;

        Physics.Raycast(ThisToGround, out ThisPos);
        Physics.Raycast(TargetToGround, out TargetPos);

        if ((ThisPos.point - TargetPos.point).magnitude <= rangeValue)
        {
            return true;
        }

        return false;
    }

    public float CheckDist(Minion unit)//Does not matter which is first
    {
        Vector3 pos1 = this.gameObject.transform.position;
        Vector3 pos2 = unit.gameObject.transform.position;

        Ray ThisToGround = new Ray(pos1, Vector3.down);
        Ray TargetToGround = new Ray(pos2, Vector3.down);

        RaycastHit ThisPos;
        RaycastHit TargetPos;

        Physics.Raycast(ThisToGround, out ThisPos);
        Physics.Raycast(TargetToGround, out TargetPos);

        return (ThisPos.point - TargetPos.point).magnitude;
    }

    public virtual void Unit_Self_Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>())
            return;

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

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>())
            return;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeValue);
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
