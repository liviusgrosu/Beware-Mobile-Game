using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreenUI : MonoBehaviour, IUIElement
{
    private MainMenuMaster menuMaster;
    private bool isUIActive;

    private void Start()
    {
        menuMaster = GameObject.Find("Main Menu Master").GetComponent<MainMenuMaster>();
    }

    public void BackButtonPress()
    {
        ToggleUI(false);
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Title);
    }

    public void LevelSelectButtonPress(int levelId)
    {

    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
            child.gameObject.SetActive(isUIActive);
    }
}
