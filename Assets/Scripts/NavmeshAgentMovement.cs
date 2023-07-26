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

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();

        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.canSeePlayer && agentMove)
        {
            agent.SetDestination(playerTransform.position); // Move toward player
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
