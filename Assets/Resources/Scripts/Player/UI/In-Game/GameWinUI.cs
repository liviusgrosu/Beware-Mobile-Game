﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinUI : MonoBehaviour, IUIGenericElement
{
    private bool isUIActive;

    private GameManager gameManager;
    public SoundController soundController;

    private ScoreManager scoreManager;
    private List<RectTransform> starUI;

    public Sprite emptyStar, fullStar;

    public ParticleSystem starBurstEffect;

    [SerializeField] private Transform advanceLevelButton;

    private bool displayStarScore;

    [SerializeField] private ParticleSystem starBurstParticles;

    public Image adBanner;

    private void Awake()
    {
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();

        starUI = new List<RectTransform>();
        foreach (RectTransform star in transform.Find("Scoring Star").GetComponentsInChildren<RectTransform>())
            starUI.Add(star);

        starUI.RemoveAt(0);
    }

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void ReplayButtonPress()
    {
        soundController.PlayMenuSound(EnumDefinitions.MenuSoundClip.ButtonPress);
        gameManager.RestartLevel();
    }

    public void NextLevelButtonPress()
    {
        soundController.PlayMenuSound(EnumDefinitions.MenuSoundClip.ButtonPress);
        gameManager.AdvanceLevel();
    }

    public void MenuButtonPress()
    {
        soundController.PlayMenuSound(EnumDefinitions.MenuSoundClip.ButtonPress);
        gameManager.GoBackToMainMenu();
    }

    public void ToggleUI(bool state)
    {
        //Rework this, this will always be the case that this will be turned on never off
        //Only way that it considers both is the pause menu
        isUIActive = state;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }

        if(isUIActive)
        {
            LoadAd();
            StartCoroutine(UnlockStarScore());

            SaveSystem.SaveLevel(SceneManager.GetActiveScene().name, true, scoreManager.starTotal);

            if (!gameManager.IsAnotherLevelAvailable())
            {
                advanceLevelButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void LoadAd()
    {
        adBanner.sprite = SaveSystem.LoadAd(EnumDefinitions.AdSizes.Banner);
    }

    IEnumerator UnlockStarScore()
    {
        if (!displayStarScore) displayStarScore = true;
        else yield break;

        for (int starIdx = 0; starIdx < scoreManager.starTotal; starIdx++)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 starPos = starUI.ElementAt(starIdx).transform.position;
            starBurstParticles.transform.position = new Vector3(starPos.x, starPos.y, starBurstParticles.transform.position.z);
            starBurstParticles.Play();
            starUI.ElementAt(starIdx).GetComponent<Image>().sprite = fullStar;
            soundController.PlayMenuSound(EnumDefinitions.MenuSoundClip.StarCollect);
        }
    }
}
