using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
    //attributes.
    [Tooltip("maximum 12")]
    public Item[] inventory;
    [Tooltip("maximum 5 , 0 - None ,1 - Handgun ,2 - Shotgun ,3 - AssaultRifle")]
    public GameObject[] weapons;
    private PlayerStatus status;

    private void Start()
    {
        status = GetComponent<PlayerOverrideController>().playerStatus;


    }

    //Global
    public bool ReloadWeapon(int amount,out int available)
    {
        Weapon weapon = weapons[status.selectedWeapon].GetComponent<WeaponController>().weaponInfo;
        return ReloadWeaponWithType(weapon.name, amount, out available);
    }
    

    private bool ReloadWeaponWithType(ItemName itemName, int amount,out int reloadable)
    {
        Item ammo;
        int idx = 0;
        switch(itemName)
        {
            case ItemName.Handgun: ammo = GetItem(ItemName.HandgunAmmo, out idx); break;
            case ItemName.Shotgun: ammo = GetItem(ItemName.ShotgunAmmo, out idx); break;
            case ItemName.AssaultRifle: ammo = GetItem(ItemName.AssaultRifleAmmo, out idx); break;
            default: ammo = new Item(); ammo.type = ItemType.None; ammo.name = ItemName.None; break;
        }
        
        //check exist;
        if (ammo.type != ItemType.None)
        {
            //check ammo fulfill reiqure amount
            if (amount > ammo.amount)
            {
                int ammoFill = 0;
                ammoFill += ammo.amount;
                inventory[idx].type = ItemType.None;

                //to fill amount of reqired ammo.
                if(amount > ammoFill)
                {
                    Item search = GetItem(ammo.name, out idx);
                    //keep search until no ammo left in my inventory. 
                    while(search.type != ammo.type)
                    {
                        if (ammoFill >= amount)
                            break;

                        search = GetItem(ammo.name, out idx);
                        int ammoCount = (ammoFill - ammo.amount);
                        ammoFill += ammoCount;
                        inventory[idx].amount -= ammoCount;
                        if (search.amount == 0)
                        {
                            inventory[idx].type = ItemType.None;
                        }
                    }
                    reloadable = ammoFill;
                }
                else
                {
                    inventory[idx].amount -= ammoFill;
                    reloadable = ammoFill;
                }
            }
            else
            {
                inventory[idx].amount -= amount;
                reloadable = amount;
                if (inventory[idx].amount <= 0)
                    inventory[idx].type = ItemType.None;

            }
            return true;
        }
        reloadable = 0;
        return false;
    }

    

    //TODO: Redefine Weapon Equip
        
    public void EquipWeapon(int index)
    {
        int selected = 0;
        WeaponController weaponController = weapons[index].GetComponent<WeaponController>();
        if (GetItem(weaponController.weaponInfo.name, out selected).type != ItemType.None)
        {
            if (weapons[status.selectedWeapon] != null)
            {
                weapons[status.selectedWeapon].GetComponent<WeaponController>().UnWieldWeapon();
                
            }
            weaponController.WieldWeapon();
            status.selectedWeapon = index;
        }
    }

    //Items

    public bool AddItem(Item item) // this bool determines destroy.
    {

        switch(item.type)
        {
            case ItemType.Ammo: return AddAmmo(item);
            case ItemType.Gun: return AddGun(item); 
            case ItemType.Health: return AddHealth(item); 
            case ItemType.Key: return AddKey(item);
            default: return false;
        }

    }

    private bool AddAmmo(Item item)
    {
        int index;
        int emptySpace = GetEmptySpace();
        Item exist = GetItem(item.name, out index);
        //exceeded max.
        if(exist.type != ItemType.None)
        {
            if (exist.amount + item.amount > exist.maxAmount)
            {
                int addTo = exist.maxAmount - exist.amount;
                exist.amount += addTo;
                item.amount -= addTo;
                if (emptySpace != inventory.Length)
                {
                    inventory[emptySpace] = item;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                exist.amount += item.amount;
                return true;
            }
        }
        else if(emptySpace != inventory.Length)
        {
            inventory[emptySpace] = item;
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private bool AddHealth(Item item)
    {
        int index;
        int emptySpace = GetEmptySpace();
        Item exist = GetItem(item.name, out index);
        //exceeded max.
        if (exist.type != ItemType.None)
        {
            if (exist.amount + item.amount > exist.maxAmount)
            {
                int addTo = exist.maxAmount - exist.amount;
                exist.amount += addTo;
                item.amount -= addTo;
                if (emptySpace != inventory.Length)
                {
                    inventory[emptySpace] = item;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                exist.amount += item.amount;
                return true;
            }
        }
        else if (emptySpace != inventory.Length)
        {
            inventory[emptySpace] = item;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool AddGun(Item item)
    {
        int emptySpace = GetEmptySpace();
        if (emptySpace != inventory.Length)
        {
            inventory[emptySpace] = item;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool AddKey(Item item)
    {
        int emptySpace = GetEmptySpace();
        if (emptySpace != inventory.Length)
        {
            inventory[emptySpace] = item;
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GetEmptySpace()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].type == ItemType.None)
                return i;
        }

        return inventory.Length;
    }

    private Item GetItem(ItemName lookable, out int refer)
    {
        Item returns = new Item();
        refer = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            //TODO : Check less amount of ammo.
            if (inventory[i].name == lookable)
            {
                if (returns.type == ItemType.None)
                {
                    returns = inventory[i];
                    refer = i;
                }
                else if (inventory[i].amount <= returns.amount)
                {
                    //allocate new
                    returns = inventory[i];
                    refer = i;
                }
            }
        }
        return returns;
    }

    public void DiscardItem(int index)
    {
        inventory[index].type = ItemType.None;
        inventory[index].name = ItemName.None;
    }
        
    private void UseItem(int index,int amount)
    {
        inventory[index].Use(amount);
    }
}