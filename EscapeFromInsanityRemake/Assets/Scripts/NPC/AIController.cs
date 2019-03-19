using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public AIStatus status;

    private AIAnimator animator;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {

        animator = GetComponent<AIAnimator>();
        agent = GetComponent<NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DeathCheck()
    {
        if (status.health <= 0)
        {
            Dead();
        }
    }

    private void Wander()
    {
        //random spot point

        //NavMesh.SamplePosition check
            //nav.set destination.
    }

    private void DetectingPlayer()
    {
        //using field of view and sound colision

        //if enemey detect player, then follow player

        //else do nothing.

    }

    private void Tracking()
    {
        status.actionStatus = ActionStatus.tracking;

    }


    public void TookHit(RaycastHit hit,int damage)
    {
        animator.TookHit();
        //check damage calculation.

        //activate blood prefab on hitPoint.
        DeathCheck();
    }

    private void Dead()
    {
        animator.Dead();
        status.actionStatus = ActionStatus.over;
    }
}
