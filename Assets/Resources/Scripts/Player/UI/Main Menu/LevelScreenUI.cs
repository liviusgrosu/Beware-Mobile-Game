using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenUI : MonoBehaviour, IUIElement
{
    private MainMenuMaster menuMaster;
    private bool isUIActive;

    public List<LevelButton> levelButtons;

    [SerializeField]
    private string levelNamePrefix;

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
        SceneManager.LoadScene($"{levelNamePrefix} {levelId}");
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
            child.gameObject.SetActive(isUIActive);

        if(isUIActive)
        {
            foreach (LevelButton button in levelButtons)
            {
                LevelData data = SaveSystem.LoadLevel(button.levelName);
                button.ToggleStarImages((data != null) ? data.levelStarScore : 0);
            }
        }
    }
}
