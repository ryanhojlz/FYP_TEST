using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

public class TestUnit_Control : MonoBehaviour
{
    public Camera cam;
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
    }
    // Update is called once per frame
    void Update()
    {
        //this.sm.ExecuteStateUpdate();
        //Debug.Log(pathIndex);

        pathIndex = tempPathManager.GetComponent<PathManager>().AssignPath(transform.position, this.tag);
        agent.SetDestination(tempPathManager.GetComponent<PathManager>().GetNextWaypoint(pathIndex, waypointIndex));

        if (waypointIndex < tempPathManager.GetComponent<PathManager>().GetPathWaypointCount(pathIndex))
        {
            //If waypoint has reached, goes to the next path
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        ++waypointIndex;
                        agent.SetDestination(tempPathManager.GetComponent<PathManager>().GetNextWaypoint(pathIndex, waypointIndex));
                    }
                }
            }
        }

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
