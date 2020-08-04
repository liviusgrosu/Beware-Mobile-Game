using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUI : MonoBehaviour, IUIElement
{
    private MainMenuMaster menuMaster;
    private bool isUIActive;

    private void Start()
    {
        menuMaster = GameObject.Find("Main Menu Master").GetComponent<MainMenuMaster>();
    }

    public void LevelsButtonPress()
    {
        ToggleUI(false);
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Levels);
    }

    public void OptionsButtonPress()
    {
        ToggleUI(false);
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Options);
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
            child.gameObject.SetActive(isUIActive);
    }
}
