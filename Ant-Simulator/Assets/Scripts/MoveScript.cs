using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveScript : MonoBehaviour
{
    NavMeshAgent navmeshAgent;
    public Transform destination;

	void Start ()
    {

	}

    private void Update()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        navmeshAgent.SetDestination(destination.position);
    }
}
