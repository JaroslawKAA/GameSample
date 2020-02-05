using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour
{
    [Header("Setting's")]
    public Transform startWaypoint;
    public Transform endWaypoint;
    public float speed = 1;

    float distanceToPlayer;

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(startWaypoint.position, GameObject.FindWithTag("Player").transform.position);

        if (distanceToPlayer < 1.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endWaypoint.position, speed * 2 * Time.deltaTime);
        }
        else if (distanceToPlayer > 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, startWaypoint.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startWaypoint.position, speed * Time.deltaTime);
        }
    }
}
