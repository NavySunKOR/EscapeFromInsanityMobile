using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public GameObject icon;
    public Item info;
    private Transform playerTr;

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().transform;
    }

    private void FixedUpdate()
    {
        if (icon.activeSelf)
            icon.transform.LookAt(playerTr);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            icon.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            icon.SetActive(false);
    }

    public void PickupItem()
    {
        Destroy(gameObject);
    }
}
