﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMaster : MonoBehaviour
{
    [HideInInspector]
    public enum MenuPage
    {
        Title,
        Options,
        Levels
    }

    [HideInInspector]
    public MenuPage currentPage;

    private Transform titlePage, optionsPage, levelsPage;

    private void Start()
    {
        titlePage = GameObject.Find("Title Page").transform;
        optionsPage = GameObject.Find("Options Page").transform;
        levelsPage = GameObject.Find("Levels Page").transform;

        currentPage = MenuPage.Levels;
    }

    public void ChangeToPage(MenuPage nextPage)
    {
        currentPage = nextPage;

        switch (currentPage)
        {
            case MenuPage.Levels:
                levelsPage.GetComponent<IUIElement>().ToggleUI(true);
                break;
            case MenuPage.Title:
                titlePage.GetComponent<IUIElement>().ToggleUI(true);
                break;
            case MenuPage.Options:
                optionsPage.GetComponent<IUIElement>().ToggleUI(true);
                break;
        }
    }
}
