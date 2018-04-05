using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//public class PatrolScript : MonoBehaviour
//{
//    public List<GameObject> PatrolPoints;
//    NavMeshAgent navmeshagent;

//	void Start ()
//    {
//        navmeshagent = GetComponent<NavMeshAgent>();
//        navmeshagent.SetDestination(PatrolPoints[0].transform.position);
//	}


//	void Update ()
//    {
//        for (int i = 0; i < PatrolPoints.Count; i++)
//        {
//            if (Vector3.Distance(transform.position, PatrolPoints[i].transform.position) < 1 && i < PatrolPoints.Count - 1)
//            {
//                navmeshagent.SetDestination(PatrolPoints[i + 1].transform.position);
//            }
//            else if(Vector3.Distance(transform.position, PatrolPoints[i].transform.position) < 1 && i == PatrolPoints.Count - 1)
//            {
//                navmeshagent.SetDestination(PatrolPoints[0].transform.position);
//            }
//        }
//    }
//}
public class PatrolScript : MonoBehaviour
{
    public List<GameObject> PatrolPoints;
    NavMeshAgent navmeshagent;

    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        navmeshagent.SetDestination(PatrolPoints[Random.Range(0, PatrolPoints.Count - 1)].transform.position);
    }


    void Update()
    {
            if (navmeshagent.remainingDistance < 0.5f)
            {
                navmeshagent.SetDestination(PatrolPoints[Random.Range(0, PatrolPoints.Count - 1)].transform.position);
            }
    }
}