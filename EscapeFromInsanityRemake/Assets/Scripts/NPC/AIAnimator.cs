using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour {

    private Animator animator;
    private AIStatus status;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        status = GetComponent<AIController>().status;
	}
	
	// Update is called once per frame
	void Update () {
        SetActionProperty();
	}

    private void SetActionProperty()
    {
        animator.SetBool("IsWalking", (status.actionStatus == ActionStatus.wandering || status.actionStatus == ActionStatus.tracking) ? true : false);
        animator.SetBool("IsEating", (status.actionStatus == ActionStatus.eating ) ? true : false);
    }

    public void Attack()
    {
        animator.SetTrigger("AttackTrigger");
    }

    public void TookHit()
    {
        animator.SetTrigger("HitTrigger");
    }

    public void Dead()
    {
        animator.SetTrigger("DeathTrigger");
    }
}
