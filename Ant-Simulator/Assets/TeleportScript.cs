using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportScript : MonoBehaviour
{
    public Vector3 OverWorldPosition;
    public Vector3 UnderWorldPosition;
    public bool IsOverworldCollider;
    private Vector3 temp;

    private void Start()
    {
        OverWorldPosition = transform.GetChild(0).position;
        UnderWorldPosition = transform.GetChild(1).position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ant"))
        {
            temp = other.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination;
            if (other.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination.y > 29 && !IsOverworldCollider)
            {
                other.GetComponent<NavMeshAgent>().Warp(OverWorldPosition);
                other.GetComponent<NavMeshAgent>().SetDestination(temp);
            }
            if (other.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination.y < 29 && IsOverworldCollider)
            {
                other.GetComponent<NavMeshAgent>().Warp(UnderWorldPosition);
                other.GetComponent<NavMeshAgent>().SetDestination(temp);
            }
        }
    }
}