using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//list all status.

[System.Serializable]
public class PlayerStatus{
    
    public readonly float MAX_RUN_VERTICAL = 0.6f;

    //status properties.
    public int maxHealth;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    private int health;

    public bool HGWield
    {
        get
        {
            return hgWield;
        }
        set
        {
            hgWield = value;
        }
    }

    public bool SGWield
    {
        get
        {
            return sgWield;
        }
        set
        {
            sgWield = value;
        }
    }

    public bool ARWield
    {
        get
        {
            return arWield;
        }
        set
        {
            arWield = value;
        }
    }

    private bool hgWield = false;
    private bool sgWield = false;
    private bool arWield = false;

    //movement properties

    public bool IsAim
    {
        get
        {
            return isAim;
        }
        set
        {
            isAim = value;
        }
    }
    
    public float Horizontal
    {
        get
        {
            return horizontal;
        }
        set
        {
            horizontal = value;
        }
    }

    public float Vertical
    {
        get
        {
            return vertical;
        }
        set
        {
            vertical = value;
        }
    }
    
    private bool isAim = false;
    private float horizontal = 0f;
    private float vertical = 0f;

    //interact
    public float DetectRadius
    {
        get
        {
           return detectRadius;
        }
    }

    private float detectRadius = 5f;

    public LayerMask detectLayer;

    //UI
    public bool isPaused;


    //Inventory and weapon

    public int selectedWeapon;

}
