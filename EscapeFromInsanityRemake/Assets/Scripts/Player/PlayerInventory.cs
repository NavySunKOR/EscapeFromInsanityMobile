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

    public bool PickupItem(Transform tr)
    {
        //check item
        Pickable pck = tr.GetComponent<Pickable>();
        switch (pck.item.type)
        {
            case ItemType.Gun:return PickupOne(pck);
            case ItemType.Ammo: return PickupAmount(pck);
            case ItemType.Key: return PickupOne(pck);
            case ItemType.Health: return PickupAmount(pck);
            default: return false; 

        }

    }

    private bool PickupOne(Pickable pck)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i].type == ItemType.None)
            {
                AddInventoryItem(pck, i);
                return true;
            }
        }
        return false;
    }

    private bool PickupAmount(Pickable pck)
    {
        int fillable = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].type == ItemType.None)
            {
                AddInventoryItem(pck, i);
                return true;
            }
            else
            {
                if(inventory[i].name == pck.item.name)
                {
                    if(inventory[i].amount + pck.item.amount > inventory[i].maxAmount)
                    {
                        fillable = inventory[i].maxAmount - inventory[i].amount;
                        inventory[i].amount += fillable;
                        pck.item.amount -= fillable;
                    }
                    else
                    {
                        inventory[i].amount += pck.item.amount;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void AddInventoryItem(Pickable pck,int idx)
    {
        switch (pck.item.type)
        {
            case ItemType.Gun: AddWeapon(pck, idx); break;
            case ItemType.Ammo: AddAmmo(pck, idx); break;
            case ItemType.Health: AddHealth(pck, idx); break;
            case ItemType.Key: AddKey(pck, idx);break;
            default: break;
        }
        
    }

    private void AddWeapon(Pickable pck,int idx)
    {
        inventory[idx] = new Weapon();
        inventory[idx].amount = pck.item.amount;
        inventory[idx].maxAmount = pck.item.maxAmount;
        inventory[idx].type = pck.item.type;

        switch(pck.item.name)
        {
            case ItemName.Handgun: weapons[1].GetComponent<WeaponController>().isHaving = true; break;
            case ItemName.Shotgun: weapons[2].GetComponent<WeaponController>().isHaving = true; break;
            case ItemName.AssaultRifle: weapons[3].GetComponent<WeaponController>().isHaving = true; break;
            default: break;

        }
    }

    private void AddAmmo(Pickable pck, int idx)
    {
        inventory[idx] = new Ammo();
        inventory[idx].amount = pck.item.amount;
        inventory[idx].maxAmount = pck.item.maxAmount;
        inventory[idx].type = pck.item.type;
        inventory[idx].name = pck.item.name;
    }

    private void AddHealth(Pickable pck, int idx)
    {
        inventory[idx] = new Health();
        inventory[idx].amount = pck.item.amount;
        inventory[idx].maxAmount = pck.item.maxAmount;
        inventory[idx].type = pck.item.type;
        inventory[idx].name = pck.item.name;
    }

    private void AddKey(Pickable pck, int idx)
    {
        inventory[idx] = new Key();
        inventory[idx].amount = pck.item.amount;
        inventory[idx].maxAmount = pck.item.maxAmount;
        inventory[idx].type = pck.item.type;
        inventory[idx].name = pck.item.name;
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
            case ItemName.AssaultRifle: ammo = GetItem(ItemName.AssaultRifle, out idx); break;
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

    private Item GetItem(ItemName lookable,out int refer)
    {
        Item returns = new Item();
        refer = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            //TODO : Check less amount of ammo.
            if(inventory[i].name == lookable)
            {
                if(returns.type == ItemType.None)
                {
                    returns = inventory[i];
                    refer = i;
                }
                else if(inventory[i].amount <= returns.amount)
                {
                    //allocate new
                    returns = inventory[i];
                    refer = i;
                }
            }
        }
        return returns;
    }

    //TODO: Redefine Weapon Equip
        
    private void EquipWeapon(int index)
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