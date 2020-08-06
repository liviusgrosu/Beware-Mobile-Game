using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour, IUIElement
{
    private bool isUIActive;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    //Used by the pause button
    public void TogglePause(bool state)
    {
        isUIActive = state;

        foreach (Transform child in transform)
        {
            if(child.gameObject.name == "Pause Button") 
                child.gameObject.SetActive(!isUIActive);
            else 
                child.gameObject.SetActive(isUIActive);
        }

        if (isUIActive) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    //Used by the game manager
    //The reason for this is because the pause button needs to toggle opposite from the menu
    //If this was both used by the menu and pause button then the game win screen would include a pause button
    public void ToggleUI(bool state)
    {
        isUIActive = state;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }
    }

    public void MenuButtonPress()
    {
        Time.timeScale = 1f;
        gameManager.GoBackToMainMenu();
    }

    public void ReplayButtonPress()
    {
        Time.timeScale = 1f;
        gameManager.RestartLevel();
    }
}
