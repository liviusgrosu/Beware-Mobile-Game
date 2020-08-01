using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameWinUI : MonoBehaviour, IUIElement
{
    private bool isUIActive;

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

    public void ReplayButtonPress()
    {
        // Restart Level
    }

    public void NextLevelButtonPress()
    {

    }

    public void MenuButtonPress()
    {
        // Go back to menu
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
        }
    }
}
