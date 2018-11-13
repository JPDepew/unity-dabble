using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsAI : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWaypoint = 0;

    public Vector2 dir;
    GameObject currentWaypointTarget = null;

    private EnemyAI enemyAi;

    void Start()
    {
        enemyAi = GetComponent<EnemyAI>();
        dir = Vector2.zero;
        findClosest();
        Debug.Log("In start");
    }

    public Vector3 evaluateWaypoint()
    {

        dir = waypoints[currentWaypoint].transform.position - this.transform.position;
        Debug.Log("Dir " + dir.ToString());
        if (dir.magnitude < 0.01) //enemy has reached the waypoint
        {
            currentWaypoint++;
            currentWaypoint %= waypoints.Length;

        }
        dir.Normalize();
        return dir;
    }

    private void findClosest()
    {
        float shortestDistance = float.MaxValue;
        Vector3 shortestDirection = Vector2.zero;
        Vector3 dirTemp = Vector2.zero;
        foreach (GameObject g in waypoints)
        {
            dirTemp = g.transform.position - this.transform.position;
            dirTemp.z = 0;
            float distance = dirTemp.magnitude;
            if (distance <= 0.6) //skip the one we're at
                continue;
            if (distance < shortestDistance)
            {
                dirTemp.z = 0;
                shortestDirection = dirTemp;
                shortestDistance = distance;
                currentWaypointTarget = g;

            }

        }
        dir = shortestDirection;
    }
    public Vector2 findClosestWaypoint()
    {

        Vector2 foo = this.transform.position - currentWaypointTarget.transform.position;
        float distanceToTarget = foo.magnitude;

        if (distanceToTarget < 0.6)
            findClosest();

        return dir.normalized;


    }
    // Update is called once per frame
    void Update()
    {

    }
}
