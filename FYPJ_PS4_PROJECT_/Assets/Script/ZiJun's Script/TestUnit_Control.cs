using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

public class TestUnit_Control : MonoBehaviour
{
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    private static List<WaypointClass> PlayerWaypointList = new List<WaypointClass>();
    private int pathIndex = 0;
    private int waypointIndex = 0;

    //private StateMachine sm = new StateMachine();
    private GameObject tempPathManager;
    private void Start()
    {
        //this.sm.ChangeState(new State_Moving(agent, pathIndex));
        agent.updateRotation = false;
        tempPathManager = GameObject.FindGameObjectWithTag("PathManager");
        agent.enabled = false;
        this.GetComponent<Rigidbody>().freezeRotation = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }

    public int GetpathIndex()
    {
        return pathIndex;
    }

    // Update is called once per frame
    void Update()
    {
        agent.enabled = true;
        //this.sm.ExecuteStateUpdate();
        //Debug.Log(pathIndex);
        if (this.gameObject.GetComponent<Minion>().GetTarget())
            return;

        //Debug.Log(waypointIndex);

        if (!GetComponent<Minion>().GetIsActive())
        {
            agent.isStopped = true;
            return;
        }

        if(waypointIndex <= 0)
        pathIndex = tempPathManager.GetComponent<PathManager>().AssignPath(transform.position, this.tag);

        if (pathIndex <= -1)//If there is no path, dont continue
        {
            return;
        }

        //GetComponent<NavMeshAgent>().baseOffset = 0f;

        if (agent.isOnNavMesh && agent.isActiveAndEnabled)
        agent.SetDestination(tempPathManager.GetComponent<PathManager>().GetNextWaypoint(pathIndex, waypointIndex));

        if (waypointIndex < tempPathManager.GetComponent<PathManager>().GetPathWaypointCount(pathIndex) - 1)
        {
            //If waypoint has reached, goes to the next path
            if (!agent.pathPending && agent.isOnNavMesh && agent.isActiveAndEnabled)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        int currentWaypoint = waypointIndex;
                       
                        ++waypointIndex;

                        //Debug.Log("Current : " + currentWaypoint + ", " + waypointIndex);
                        //agent.SetDestination(tempPathManager.GetComponent<PathManager>().GetNextWaypoint(pathIndex, waypointIndex));
                    }
                }
            }
        }

        if (character != null)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }
    }
}
