using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUI : MonoBehaviour, IUIGenericElement
{
    private MainMenuMaster menuMaster;
    private MenuSoundController soundController;
    private bool isUIActive;

    private void Start()
    {
        soundController = transform.parent.GetComponent<MenuSoundController>();
        menuMaster = GameObject.Find("Main Menu Master").GetComponent<MainMenuMaster>();
    }

    public void LevelsButtonPress()
    {
        ToggleUI(false);
        soundController.PlayButtonPress();
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Levels);
    }

    public void OptionsButtonPress()
    {
        ToggleUI(false);
        soundController.PlayButtonPress();
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Options);
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
            child.gameObject.SetActive(isUIActive);
    }
}
