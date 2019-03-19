using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActionStatus { eating = 0, wandering, tracking , grabbing , over};
[System.Serializable]
public class AIStatus {
    public int health;
    public ActionStatus actionStatus;
    public float GrabRange
    {
        get
        {
            return grabRange;
        }

        set
        {
            grabRange = value;
        }

    }

    public float HearRange
    {
        get
        {
            return hearRange;
        }

        set
        {
            hearRange = value;
        }

    }

    public float FieldOfView
    {
        get
        {
            return fieldOfView;
        }

        set
        {
            fieldOfView = value;
        }

    }

    public float WanderWaitInterval
    {
        get
        {
            return wanderWaitInterval;
        }

        set
        {
            wanderWaitInterval = value;
        }

    }

    public float GrabInterval
    {
        get
        {
            return grabInterval;
        }

        set
        {
            grabInterval = value;
        }
    }


    private float grabRange;
    private float hearRange;
    private float fieldOfView;
    private float wanderWaitInterval;
    private float grabInterval;


}
