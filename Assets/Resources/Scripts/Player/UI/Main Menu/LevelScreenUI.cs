using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreenUI : MonoBehaviour, IUIGenericElement
{
    private MainMenuMaster menuMaster;
    private MenuSoundController soundController;
    private bool isUIActive;

    public List<LevelButton> levelButtons;

    [SerializeField]
    private string levelNamePrefix;

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

    public void LevelSelectButtonPress(int levelId)
    {
        soundController.PlayButtonPress();
        SceneManager.LoadScene($"{levelNamePrefix} {levelId}");
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
            child.gameObject.SetActive(isUIActive);

        if(isUIActive)
        {
            LevelData currLevelData, prevLevelData;
            LevelButton currLevel;
            for (int i = 0; i < levelButtons.Count; i++)
            {
                currLevel = levelButtons.ElementAt(i);
                currLevelData = SaveSystem.LoadLevel(currLevel.levelName);
                levelButtons.ElementAt(i).ToggleStarImages((currLevelData != null) ? currLevelData.levelStarScore : 0);

                if (i == 0) currLevel.ToggleButton(true);
                else
                {
                    prevLevelData = SaveSystem.LoadLevel(levelButtons.ElementAt(i - 1).levelName);
                    if (prevLevelData != null) currLevel.ToggleButton(prevLevelData.levelFinished);
                    else currLevel.ToggleButton(false);
                }
            }
        }
    }
}
