using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour, IUIGenericElement
{
    private bool isUIActive;
    private GameManager gameManager;
    public MenuSoundController menuSoundController;
    public Image adBanner;
    public Slider soundVolumeSlider;

    private void LoadAd()
    {
        adBanner.sprite = SaveSystem.LoadAd(EnumDefinitions.AdSizes.Banner);
    }

    private void Awake()
    {
        LoadSettings();
    }

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    //Used by the pause button
    public void TogglePause(bool state)
    {
        menuSoundController.PlayButtonPress();

        isUIActive = state;

        foreach (Transform child in transform)
        {
            if(child.gameObject.name == "Pause Button") 
                child.gameObject.SetActive(!isUIActive);
            else 
                child.gameObject.SetActive(isUIActive);
        }

        if (isUIActive) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        gameManager.ToggleInGameUI(!state);
        LoadAd();
    }

    //Used by the game manager
    //The reason for this is because the pause button needs to toggle opposite from the menu
    //If this was both used by the menu and pause button then the game win screen would include a pause button
    public void ToggleUI(bool state)
    {
        isUIActive = state;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }
    }

    public void MenuButtonPress()
    {
        menuSoundController.PlayButtonPress();
        Time.timeScale = 1f;
        gameManager.GoBackToMainMenu();
    }

    public void ReplayButtonPress()
    {
        menuSoundController.PlayButtonPress();
        Time.timeScale = 1f;
        gameManager.RestartLevel();
    }

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(soundVolumeSlider.value);
        menuSoundController.ChangeAudioSrcVolume(soundVolumeSlider.value);
        menuSoundController.PlayButtonPress();
    }

    public void LoadSettings()
    {
        SettingData currSettingsData = SaveSystem.LoadSettings();
        if (currSettingsData == null)
        {
            soundVolumeSlider.value = 1f;
        }
        else
        {
            soundVolumeSlider.value = currSettingsData.soundLevel;
        }
    }
}
