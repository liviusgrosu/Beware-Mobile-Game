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
            for(int i = 0; i < levelButtons.Count; i++)
            {
                if(i == 0)
                {
                    LevelData currLevelData = SaveSystem.LoadLevel(levelButtons.ElementAt(i).levelName);
                    levelButtons.ElementAt(i).ToggleStarImages((currLevelData != null) ? currLevelData.levelStarScore : 0);
                    levelButtons.ElementAt(i).ToggleButton(true);
                }
                else
                {
                    LevelData currLevelData = SaveSystem.LoadLevel(levelButtons.ElementAt(i).levelName);
                    LevelData prevLevelData = SaveSystem.LoadLevel(levelButtons.ElementAt(i - 1).levelName);

                    if (prevLevelData != null)
                        levelButtons.ElementAt(i).ToggleButton(prevLevelData.levelFinished);
                    else
                        levelButtons.ElementAt(i).ToggleButton(false);

                    levelButtons.ElementAt(i).ToggleStarImages((currLevelData != null) ? currLevelData.levelStarScore : 0);
                }
            }
        }
    }
}
