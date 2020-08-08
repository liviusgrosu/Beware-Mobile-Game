using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreenUI : MonoBehaviour, IUIGenericElement
{
    private MainMenuMaster menuMaster;
    private MenuSoundController soundController;
    private bool isUIActive;

    private void Start()
    {
        soundController = transform.parent.GetComponent<MenuSoundController>();
        menuMaster = GameObject.Find("Main Menu Master").GetComponent<MainMenuMaster>();
    }

    public void BackButtonPress()
    {
        ToggleUI(false);
        soundController.PlayButtonPress();
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Title);
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
            child.gameObject.SetActive(isUIActive);
    }
}
