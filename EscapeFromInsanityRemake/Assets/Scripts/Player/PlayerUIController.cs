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

    private void Start()
    {
        //OnOFFCrossHair();
        status = GetComponent<PlayerOverrideController>().playerStatus;
        //crossHair.SetActive(true);
    }

    private void Update()
    {

    }
    
   

    private void OpenCloseInventory()
    {
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

    public void AddItem()
    {

    }

    public void DiscardItem()
    {

    }

    public void ShiftItem()
    {

    }
}