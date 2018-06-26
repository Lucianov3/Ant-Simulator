using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hungry : StateMachineBehaviour {
    private NavMeshAgent ant;
    public GameObject EatArea;
    private Vector3 targetpositoin;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        targetpositoin = EatArea.transform.position;
        ant = animator.GetComponent<NavMeshAgent>();
        ant.SetDestination(targetpositoin);
        


        

	    
	}



	


}
