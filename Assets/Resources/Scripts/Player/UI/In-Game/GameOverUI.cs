using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour, IUIGenericElement
{
    private bool isUIActive;

    private GameManager gameManager;
    public MenuSoundController soundController;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void ReplayButtonPress()
    {
        soundController.PlayButtonPress();
        gameManager.RestartLevel();
    }

    public void MenuButtonPress()
    {
        soundController.PlayButtonPress();
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
