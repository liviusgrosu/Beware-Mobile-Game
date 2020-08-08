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
    private MenuSoundController soundController;

    private ScoreManager scoreManager;
    private List<RectTransform> starUI;

    public Sprite emptyStar, fullStar;

    [SerializeField] private Sprite lockedBackground;
    [SerializeField] private Transform advanceLevelButton;

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
        soundController = transform.parent.GetComponent<MenuSoundController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void ReplayButtonPress()
    {
        soundController.PlayButtonPress();
        gameManager.RestartLevel();
    }

    public void NextLevelButtonPress()
    {
        soundController.PlayButtonPress();
        gameManager.AdvanceLevel();
    }

    public void MenuButtonPress()
    {
        soundController.PlayButtonPress();
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
            for(int i = 0; i < starUI.Count; i++)
                starUI.ElementAt(i).GetComponent<Image>().sprite = (i <= scoreManager.starTotal - 1) ? fullStar : emptyStar;

            SaveSystem.SaveLevel(SceneManager.GetActiveScene().name, true, scoreManager.starTotal);

            if (!gameManager.IsAnotherLevelAvailable())
            {
                advanceLevelButton.GetComponent<Button>().enabled = false;
                advanceLevelButton.GetComponent<Image>().sprite = lockedBackground;
            }
        }
    }
}
