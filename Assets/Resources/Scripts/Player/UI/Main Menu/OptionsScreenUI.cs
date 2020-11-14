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
    private float currSoundVal;

    public Slider soundVolumeSlider;

    public AudioSource mainMenuAudioSrc;

    private void Start()
    {
        soundController = transform.parent.GetComponent<MenuSoundController>();
        menuMaster = GameObject.Find("Main Menu Master").GetComponent<MainMenuMaster>();
    }

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(soundVolumeSlider.value);
        soundController.ChangeAudioSrcVolume(soundVolumeSlider.value);
        soundController.PlayButtonPress();
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
