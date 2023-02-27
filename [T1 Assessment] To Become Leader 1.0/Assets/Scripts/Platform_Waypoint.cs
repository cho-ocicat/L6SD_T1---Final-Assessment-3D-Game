using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Waypoint : MonoBehaviour
{
    [SerializeField] GameObject[] wayPoints;
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 5f;

    void Update()
    {
        if(Vector3.Distance(transform.position, wayPoints[currentWaypointIndex].transform.position) < .1f) 
        {
            currentWaypointIndex++;
            
            if(currentWaypointIndex >= wayPoints.Length) {
                currentWaypointIndex = 0;
            }
        }
        //refers to the transform of the platform (the arm)
        //Time*deltaTime value is smaller than the framerates, normalise the speed, not per frames
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}
