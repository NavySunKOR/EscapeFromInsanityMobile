using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCanvasSetter : MonoBehaviour {

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }

}
