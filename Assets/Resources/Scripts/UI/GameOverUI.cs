using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour, IUIElement
{
    private bool isUIActive;

    public void ReplayButtonPress()
    {
        // Restart Level
    }

    public void MenuButtonPress()
    {
        // Go back to menu
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
