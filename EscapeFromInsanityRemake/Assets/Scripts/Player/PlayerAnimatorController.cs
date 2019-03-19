using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {
    
    private Animator animator;
    private PlayerStatus playerStatus;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        playerStatus = GetComponent<PlayerOverrideController>().playerStatus;
    }
	
	// Update is called once per frame
	void Update () {
        SetMovementProperties();
        SetStatusProperties();
    }

    private void SetMovementProperties()
    {
        animator.SetFloat("Horizontal", playerStatus.Horizontal);
        animator.SetFloat("Vertical", playerStatus.Vertical);
        animator.SetBool("IsAim", playerStatus.IsAim);
    }

    private void SetStatusProperties()
    {
        animator.SetBool("HGWield", playerStatus.HGWield);
        animator.SetBool("SGWield", playerStatus.SGWield);
        animator.SetBool("ARWield", playerStatus.ARWield);
    }

    public void ReloadWeapon()
    {
        animator.SetTrigger("ReloadTrigger");
    }

    public void ShootWeapon()
    {
        animator.SetTrigger("ShootTrigger");
    }

    public void TookHit()
    {
        animator.SetTrigger("TookHitTrigger");
    }

    public void Dead()
    {
        animator.SetTrigger("DeathTrigger");
    }
}
