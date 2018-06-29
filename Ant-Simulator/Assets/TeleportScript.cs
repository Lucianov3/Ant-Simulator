using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportScript : MonoBehaviour
{
    public Vector3 OverWorldPosition;
    public Vector3 UnderWorldPosition;
    public bool IsOverworldCollider;
    Vector3 temp;

    private void Start()
    {
        OverWorldPosition = transform.GetChild(0).position;
        UnderWorldPosition = transform.GetChild(1).position;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ant") )
        {
            if(other.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination.y > 29)
            {
                temp = other.GetComponent<NavMeshAgent>().destination;
                other.GetComponent<NavMeshAgent>().Warp(OverWorldPosition);
                other.GetComponent<NavMeshAgent>().SetDestination(temp);
            }
            temp = other.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination;
            if (other.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination.y < 29 )
            {
                other.GetComponent<NavMeshAgent>().Warp(UnderWorldPosition);
                other.GetComponent<NavMeshAgent>().SetDestination(temp);
            }
        }
    }
}
