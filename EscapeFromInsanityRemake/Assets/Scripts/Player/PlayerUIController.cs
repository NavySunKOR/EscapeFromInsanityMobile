using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerUIController : MonoBehaviour {
    
    public GameObject inventoryPanel;
    public GameObject inventoryInnerPanel;
    public GameObject pauseMenu;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject hitPanel;
    public GameObject[] buttons;
    public GameObject crossHair;
    public Text frameText;

    private PlayerStatus status;
    private PlayerInventory inventory;

    private void Start()
    {
        //OnOFFCrossHair();
        status = GetComponent<PlayerOverrideController>().playerStatus;
        inventory = GetComponent<PlayerInventory>();
        //crossHair.SetActive(true);
    }

    public void OpenCloseInventory()
    {
        if (!inventoryPanel.activeSelf)
            UpdateInventory();
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        OnOFFCrossHair();
    }

    public void OpenClosePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        OnOFFCrossHair();
    }

    public void OnOFFCrossHair()
    {
        status.isPaused = !status.isPaused;
        crossHair.SetActive((!status.isPaused) ? true : false);
    }
    
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator HitUI()
    {
        hitPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitPanel.SetActive(false);
    }

    //Item Movement inventory update.
    public void UpdateInventory()
    {
        Transform[] transforms = inventoryInnerPanel.GetComponentsInChildren<Transform>();
        foreach (Transform tr in transforms)
        {
            if (tr != inventoryInnerPanel.transform)
                Destroy(tr.gameObject);
        }

        for (int i = 0; i < inventory.inventory.Length; i++)
        {
            if(inventory.inventory[i].name != ItemName.None)
            {
                GameObject obj = Instantiate(buttons[(int)inventory.inventory[i].name], inventoryInnerPanel.transform);
                InventoryButton buttonInfo = obj.GetComponent<InventoryButton>();
                buttonInfo.index = i;

                switch (inventory.inventory[i].type)
                {
                    case ItemType.Gun:
                        buttonInfo.SetAmountText(inventory.weapons[(int)inventory.inventory[i].name - 1].GetComponent<WeaponController>().weaponInfo.currentMagazine);
                        break;
                    case ItemType.Ammo:
                    case ItemType.Health:
                        buttonInfo.SetAmountText(inventory.inventory[i].amount);
                        break;
                    default: break;
                }
            }
        }
    }

    public void DiscardItem()
    {
        UpdateInventory();
    }

    public void ShiftItem()
    {
        UpdateInventory();
    }
}