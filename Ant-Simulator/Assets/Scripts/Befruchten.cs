using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Befruchten : StateMachineBehaviour
{
    private NavMeshAgent ant;
    public Vector3 Targetposition;
    private KI_Rigina_formica queen;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        queen = new KI_Rigina_formica();
        ant = animator.GetComponent<NavMeshAgent>();
        ant.SetDestination(Targetposition);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(Targetposition, ant.transform.position) < 1)
        {
            queen.SpawnLarva();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}