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
    
    public AdInit AdInit;
    
    private void Start()
    {
        titlePage = GameObject.Find("Title Page").transform;
        optionsPage = GameObject.Find("Options Page").transform;
        levelsPage = GameObject.Find("Levels Page").transform;

        currentPage = MenuPage.Levels;

        // Make sure the ads have a directory to store into
        AdInit.VerifyAdFolder();
        AdInit.RefreshLatestAds();
    }

    public void ChangeToPage(MenuPage nextPage)
    {
        currentPage = nextPage;

        switch (currentPage)
        {
            case MenuPage.Levels:
                levelsPage.GetComponent<IUIGenericElement>().ToggleUI(true);
                break;
            case MenuPage.Title:
                titlePage.GetComponent<IUIGenericElement>().ToggleUI(true);
                break;
            case MenuPage.Options:
                optionsPage.GetComponent<IUIGenericElement>().ToggleUI(true);
                break;
        }
    }
}

