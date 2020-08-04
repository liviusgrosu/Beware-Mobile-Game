using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinUI : MonoBehaviour, IUIElement
{
    private bool isUIActive;

    private GameManager gameManager;

    private ScoreManager scoreManager;
    private List<RectTransform> starUI;

    public Sprite emptyStar, fullStar;

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
        gameManager.RestartLevel();
    }

    public void NextLevelButtonPress()
    {
        gameManager.AdvanceLevel();
    }

    public void MenuButtonPress()
    {
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

            SaveSystem.SaveLevel(SceneManager.GetActiveScene().name, scoreManager.starTotal);
        }
    }
}
