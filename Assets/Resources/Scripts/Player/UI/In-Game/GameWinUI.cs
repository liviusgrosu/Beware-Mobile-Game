using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinUI : MonoBehaviour, IUIGenericElement
{
    private bool isUIActive;

    private GameManager gameManager;
    private SoundController soundController;

    private ScoreManager scoreManager;
    private List<RectTransform> starUI;

    public Sprite emptyStar, fullStar;

    [SerializeField] private Sprite lockedBackground;
    [SerializeField] private Transform advanceLevelButton;

    private bool displayStarScore;

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
        soundController = GameObject.Find("Sound Controller").GetComponent<SoundController>();
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
            StartCoroutine(UnlockStarScore());

            SaveSystem.SaveLevel(SceneManager.GetActiveScene().name, true, scoreManager.starTotal);

            if (!gameManager.IsAnotherLevelAvailable())
            {
                advanceLevelButton.GetComponent<Button>().enabled = false;
                advanceLevelButton.GetComponent<Image>().sprite = lockedBackground;
            }
        }
    }
    IEnumerator UnlockStarScore()
    {
        if (!displayStarScore) displayStarScore = true;
        else yield break;

        for (int starIdx = 0; starIdx < scoreManager.starTotal; starIdx++)
        {
            yield return new WaitForSeconds(0.5f);
            starUI.ElementAt(starIdx).GetComponent<Image>().sprite = fullStar;
            soundController.PlayMenuSound(EnumDefinitions.MenuSoundClip.StarCollect);
        }
    }
}
