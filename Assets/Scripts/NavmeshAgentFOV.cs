using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAgentFOV : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    public float fovDegree;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;

        if(Mathf.Abs(Vector3.Angle(transform.forward, dir)) < fovDegree) 
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
