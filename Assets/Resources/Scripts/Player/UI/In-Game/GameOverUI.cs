using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour, IUIElement
{
    private bool isUIActive;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void ReplayButtonPress()
    {
        gameManager.RestartLevel();
    }

    public void MenuButtonPress()
    {
        gameManager.GoBackToMainMenu();
    }
    public void ToggleUI(bool state)
    {
        isUIActive = state;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }
    }
}
