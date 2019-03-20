using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealState : IState
{
    Healer_Unit unit;
    List<GameObject> targetList;
    string Ally_Tag;

    NavMeshAgent agent;// = unit.gameObject.GetComponent<NavMeshAgent>();

    public HealState(Healer_Unit _unit, List<GameObject> _Ally, string _Ally_Tag)
    {
        unit = _unit;
        targetList = _Ally;
        Ally_Tag = _Ally_Tag;
        agent = unit.gameObject.GetComponent<NavMeshAgent>();
    }

    void FindAllyWithLowestHP()
    {
        float LowestHP = float.MaxValue;//Percentage
        float tempHealth;//Percentage
        //spawn melee projectile
        if (targetList.Count > 0)
        {
            for (int i = 0; i < targetList.Count; ++i)
            {
                if (!targetList[i])//If is null skip this one
                    continue;

                if (targetList[i].tag != Ally_Tag)//If is not enemy skip as well
                    continue;

                bool targetable = false;
                for (int j = 0; j < unit.targetableType.Count; ++j)
                {
                    if (targetList[i].GetComponent<Minion>().MinionType == unit.targetableType[j])
                    {
                        targetable = true;
                        break;//Once found, no point carry on the loop
                    }
                   
                }

                if (!targetable)
                    continue;

                var temp1 = targetList[i].GetComponent<Minion>().healthValue;
                var temp2 = targetList[i].GetComponent<Minion>().startHealthvalue;
                tempHealth = temp1 / temp2;

                if (tempHealth >= LowestHP)//If is longer then what is already targeted
                    continue;
                LowestHP = tempHealth;
                unit.SetTarget(targetList[i]);// = targetList[i];
            }
        }
    }

    public void Enter()
    {
        //On enter find the lowest HP unit first
        FindAllyWithLowestHP();
        this.agent.speed = unit.moveSpeedValue;
    }

    public void Execute()
    {
        FindAllyWithLowestHP();//Has to find constantly

        //if (unit.GetTarget() != null)
        //{
        //    unit.FindAllyToHeal();
        //}
        if (!unit.GetTarget())
        {
            return;
        }

        //unit.gameObject.transform.LookAt(unit.GetTarget().transform.position);

        

        //if (unit.GetTarget().GetComponent<Minion>().healthValue >= unit.GetTarget().GetComponent<Minion>().startHealthvalue)
        //{
        //    agent.isStopped = false;
        //    agent.SetDestination(unit.GetTarget().gameObject.transform.position);

        //    //agent.speed = unit.moveSpeedValue;

        //    unit.SetTarget(null);
        //    return;
        //}


        if (!unit.CheckMinionWithinRange(unit.GetTarget().GetComponent<Minion>()))//If not within attack range
        {
            agent.isStopped = false;
            agent.SetDestination(unit.GetTarget().gameObject.transform.position);

            //if (unit.tag == "Ally_Unit" && unit.GetTarget())
            //    Debug.Log(unit.tag + " : " + unit.GetTarget().name);
            //Debug.Log(unit.tag + " : " + Enemy_Tag);
            //agent.speed = unit.moveSpeedValue;
            //return;
        }
        else
        {
            agent.isStopped = true;
            if (unit.GetTarget() != null)
            {
                unit.Healing();
            }
        }

        //if (unit.GetTarget().GetComponent<Minion>().healthValue >= unit.GetTarget().GetComponent<Minion>().startHealthvalue)
        //{
        //    agent.isStopped = false;
        //    agent.SetDestination(unit.GetTarget().gameObject.transform.position);

        //    agent.speed = unit.moveSpeedValue;
        //}
    }

    public void Exit()
    {
       
    }
}
