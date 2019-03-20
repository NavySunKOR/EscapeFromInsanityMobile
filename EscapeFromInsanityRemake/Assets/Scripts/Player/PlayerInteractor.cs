using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour {

    private PlayerStatus status;

	// Use this for initialization
	void Start ()
    {
        status = GetComponent<PlayerOverrideController>().playerStatus;
    }

    private void FixedUpdate()
    {
        DetectItem();
    }

    private void DetectItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, status.DetectRadius, status.detectLayer);
        foreach(Collider coll in colliders)
        {
            //active item buttons.
        }
    }

}
