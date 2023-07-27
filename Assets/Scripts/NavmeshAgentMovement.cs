using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAgentMovement : MonoBehaviour
{
    FieldOfView fieldOfView;
    public Transform playerTransform;

    Animator animator;
    NavMeshAgent agent;

    public bool agentMove = true;

    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();

        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(agentMove) 
        {
            if (fieldOfView.canSeePlayer)
            {
                agent.SetDestination(playerTransform.position); // Move toward player
            }
            else if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
            else 
            {
                UpdateDestination();
            }
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) 
        {
            waypointIndex = 0;
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }


}
