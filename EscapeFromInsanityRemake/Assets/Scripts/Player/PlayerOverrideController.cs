using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overrided action control class. It also controls status.
public class PlayerOverrideController : MonoBehaviour {
    public PlayerStatus playerStatus;
    private PlayerAnimatorController animatorController;
    private MobileInputManager inputManager;
    private ThirdPersonController thirdPersonController;
    private PlayerUIController playerUIController;
    private PlayerInventory playerInventory;

    private void Start()
    {
        animatorController = GetComponent<PlayerAnimatorController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        inputManager = GetComponentInChildren<MobileInputManager>();
        playerUIController = GetComponent<PlayerUIController>();
        playerInventory = GetComponent<PlayerInventory>();
        

        //TODO: Check this when save/load function implementing.
        SetDefaultReference(); 
    }

    private void FixedUpdate()
    {
        RefreshStatus();
    }


    private void RefreshStatus()
    {
        playerStatus.IsAim = inputManager.isAim;
        playerStatus.Horizontal = thirdPersonController.horizontal;
        playerStatus.Vertical = thirdPersonController.vertical;
        
        //Set aim movement speed
        if(playerStatus.IsAim && playerStatus.Vertical > playerStatus.MAX_RUN_VERTICAL)
        {
            playerStatus.Vertical = playerStatus.MAX_RUN_VERTICAL;
        }
    }

    private void SetDefaultReference()
    {
        playerStatus.maxHealth = 100;
        playerStatus.Health = playerStatus.maxHealth;
    }

    public void Shoot()
    {
        //TODO: Tell weapon to shoot 
        if (playerStatus.Vertical <= playerStatus.MAX_RUN_VERTICAL && playerStatus.IsAim)
            animatorController.ShootWeapon();
    }
    
    public void Reload()
    {
        //TODO: Tell weapon to reload
        if(playerStatus.Vertical <= playerStatus.MAX_RUN_VERTICAL)
            animatorController.ReloadWeapon();
    }

    public void PauseMenu()
    {
        playerUIController.OpenClosePauseMenu();
    }

    public void InventoryMenu()
    {
        playerUIController.OpenCloseInventory();
    }

    public void AddItem(Pickable item)
    {
        if(playerInventory.AddItem(item.info))
        {
            item.PickupItem();
        }
    }

    public void UseItem(int index)
    {
        playerInventory.
    }
}
