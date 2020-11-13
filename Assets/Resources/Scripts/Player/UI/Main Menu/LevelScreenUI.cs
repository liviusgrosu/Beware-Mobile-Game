using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScreenUI : MonoBehaviour, IUIGenericElement
{
    private MainMenuMaster menuMaster;
    private MenuSoundController soundController;
    private bool isUIActive;

    public List<GameObject> worldObjects;
    private List<LevelButton> currentLevelsButtons;
    
    public Text currentWorldText;
    public Button previousWorldBtn, nextWorldBtn;

    public Image adBanner;

    private int lastWorldIdx = 0;
    private int worldNumCap;

    private void Start()
    {
        worldNumCap = worldObjects.Count - 1;

        soundController = transform.parent.GetComponent<MenuSoundController>();
        menuMaster = GameObject.Find("Main Menu Master").GetComponent<MainMenuMaster>();
    }

    public void BackButtonPress()
    {
        ToggleUI(false);
        soundController.PlayButtonPress();
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Title);
    }

    public void LevelSelectButtonPress(string levelName)
    {
        //Split off world and level id here
        List<String> leveParams = levelName.Split(' ').ToList();
        int worldId = int.Parse(leveParams.ElementAt(0));
        int levelId = int.Parse(leveParams.ElementAt(1));
        
        soundController.PlayButtonPress();
        SceneManager.LoadScene($"World {worldId} Level {levelId}");
    }

    public void WorldChangeButton(int direction)
    {
        ToggleLevelButtons(false);
        lastWorldIdx += direction;
        lastWorldIdx = Mathf.Clamp(lastWorldIdx, 0, worldNumCap);
        ToggleLevelButtons(true);
        UpdateWorldSelectorUI();
    }

    private void UpdateWorldSelectorUI()
    {
        currentWorldText.text = $"World {lastWorldIdx + 1}";

        previousWorldBtn.interactable = true;
        nextWorldBtn.interactable = true;

        if (lastWorldIdx == 0)
            previousWorldBtn.interactable = false;
        else if (lastWorldIdx == worldNumCap)
            nextWorldBtn.interactable = false;
    }

    public void ToggleLevelButtons(bool state)
    {
        Transform currentWorldObj = worldObjects.ElementAt(lastWorldIdx).transform;
        LevelData currLevelData, prevLevelData;
        LevelButton currLevel;

        for (int i = 0; i < currentWorldObj.childCount; i++)
        {
            currentWorldObj.GetChild(i).gameObject.SetActive(state);
            if (state)
            {
                currLevel = currentWorldObj.GetChild(i).GetComponent<LevelButton>();
                currLevelData = SaveSystem.LoadLevel(currLevel.levelName);
                currLevel.ToggleStarImages((currLevelData != null) ? currLevelData.levelStarScore : 0);

                if (i == 0 && lastWorldIdx == 0) currLevel.ToggleButton(true);
                else
                {
                    if (i == 0)
                    {
                        prevLevelData = SaveSystem.LoadLevel(worldObjects.ElementAt(lastWorldIdx - 1).transform.GetChild(worldObjects.ElementAt(lastWorldIdx - 1).transform.childCount - 1).GetComponent<LevelButton>().levelName);
                    }
                    else
                        prevLevelData = SaveSystem.LoadLevel(currentWorldObj.GetChild(i - 1).GetComponent<LevelButton>().levelName);

                    if (prevLevelData != null) currLevel.ToggleButton(prevLevelData.levelFinished);
                    else currLevel.ToggleButton(false);
                }
            }
        }
    }

    private void LoadAd()
    {
        adBanner.sprite = SaveSystem.LoadAd(EnumDefinitions.AdSizes.Banner);
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;

        foreach (Transform child in transform)
            child.gameObject.SetActive(isUIActive);

        // TODO: not sure if I need this if statement check...
        if(isUIActive)
        {
            ToggleLevelButtons(state);
            UpdateWorldSelectorUI();
            //LoadAd();
        }
    }
}
