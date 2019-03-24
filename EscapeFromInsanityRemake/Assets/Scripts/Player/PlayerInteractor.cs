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

    //TODO : Delete This code.

}
