using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bla : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
}
