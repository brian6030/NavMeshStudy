using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0f, 360f)] public float angle;

    public GameObject player;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    NavMeshAgent agent;
    public bool agentMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(FOVRoutine());
    }
    
    IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck() 
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length > 0) 
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle * 0.5f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) 
                {
                    canSeePlayer = true;

                    if(agentMove)
                        agent.SetDestination(player.transform.position); // Move toward player
                }
                else canSeePlayer = false;
            }
            else canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            // Reset the flag if far away from player
            canSeePlayer = false;
        }
    }
}
