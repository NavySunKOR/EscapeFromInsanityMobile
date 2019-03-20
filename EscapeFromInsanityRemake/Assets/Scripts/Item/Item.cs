using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,Health,Ammo,Gun,Key
}

public enum ItemName
{

    None,Handgun,Shotgun,AssaultRifle
    ,FirstAidKit,Bandage,Alchol
    ,HandgunAmmo,ShotgunAmmo,AssaultRifleAmmo
}

[System.Serializable]
public class Item {
    public int amount;
    public int maxAmount;
    public ItemType type;
    public ItemName name;

    public virtual void Use(int useAmount)
    {

    }
}
