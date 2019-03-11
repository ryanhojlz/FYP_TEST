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

    private GameObject tempPathManager;
    private void Start()
    {
        agent.updateRotation = false;
        tempPathManager = GameObject.FindGameObjectWithTag("PathManager");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        else
        {

            //GameObject tempWaypointmanager;// = GameObject.FindGameObjectWithTag("WaypointManager");

            //if (this.tag == "Enemy_Unit")
            //{
            //    tempWaypointmanager = GameObject.FindGameObjectWithTag("Enemy_WaypointManager");
            //    //Debug.Log("Assigning Enemy Waypoint");
            //}
            //else
            //{
            //    tempWaypointmanager = GameObject.FindGameObjectWithTag("WaypointManager");
            //    //Debug.Log("Assigning Ally Waypoint");
            //}

            //var accessingVar = tempWaypointmanager.GetComponent<WaypointManager>();

            //agent.SetDestination(accessingVar.GetNearestWaypoint(transform.position));

            pathIndex = tempPathManager.GetComponent<PathManager>().AssignPath(transform.position, this.tag);

            agent.SetDestination(tempPathManager.GetComponent<PathManager>().GetNextWaypoint(pathIndex, waypointIndex));
        }
        //tempPathManager.GetComponent<PathManager>().TestValue(pathIndex, waypointIndex, transform.position);
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            //After reaching, Increment to next waypoint
            //++waypointIndex;
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {


            //if (tempPathManager.GetComponent<PathManager>().ReachDestination(pathIndex, waypointIndex, agent.stoppingDistance, transform.position) //Check if the unit have reached the waypoint
            //    && waypointIndex < tempPathManager.GetComponent<PathManager>().GetPathWaypointCount(pathIndex) // Check if it is the final point
            //    )
            //{
        
            //}

            character.Move(Vector3.zero, false, false);
        }

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
                    }
                }
            }
        }
       
    }

    //void UpdateWaypointList()
    //{
    //    //Getting the variables
    //    var tempWaypointmanager = GameObject.FindGameObjectWithTag("WaypointManager");
    //    var accessingVar = tempWaypointmanager.GetComponent<WaypointManager>();

    //    var tempWaypointList = accessingVar.GetWaypointList();

    //    PlayerWaypointList.RemoveRange(0 , PlayerWaypointList.Count);

    //    for (int i = 0; i < tempWaypointList.Count; ++i)
    //    {
    //        WaypointClass AddtoList = new WaypointClass();
    //        AddtoList.SetPos(tempWaypointList[i].GetPos());
    //        AddtoList.SetRayCast(tempWaypointList[i].GetRayCast());
    //        PlayerWaypointList.Add(AddtoList);
    //    }
    //}
}
