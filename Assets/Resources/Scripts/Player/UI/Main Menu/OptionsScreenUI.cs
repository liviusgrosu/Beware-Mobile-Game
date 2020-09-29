using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreenUI : MonoBehaviour, IUIGenericElement
{
    private MainMenuMaster menuMaster;
    private MenuSoundController soundController;
    private bool isUIActive;

    private bool settingChanges;
    private float currMusicVal, currSFXVal;

    public Slider musicSlider, sfxSlider;

    private void Start()
    {
        soundController = transform.parent.GetComponent<MenuSoundController>();
        menuMaster = GameObject.Find("Main Menu Master").GetComponent<MainMenuMaster>();
    }

    private void SaveSettings()
    {
        SaveSystem.SaveSettings(sfxSlider.value, musicSlider.value);
    }

    public void LoadSettings()
    {
        SettingData currSettingsData = SaveSystem.LoadSettings();
        if (currSettingsData == null)
        {
            musicSlider.value = 0.2f;
            sfxSlider.value = 0.5f;
        }
        else
        {
            musicSlider.value = currSettingsData.musicLevel;
            sfxSlider.value = currSettingsData.sfxLevel;
        }
    }

    public void BackButtonPress()
    {
        SaveSettings();
        ToggleUI(false);
        soundController.PlayButtonPress();
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Title);
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }
        settingChanges = false;

        if (isUIActive)
        {
            LoadSettings();
        }
    }
}
