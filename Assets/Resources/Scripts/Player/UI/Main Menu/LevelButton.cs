using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string levelName;
    private List<Image> stars;

    [SerializeField]
    private Sprite fullStar, emptyStar;
    [SerializeField]
    private Sprite lockedBackground, unlockedBackground;
    private Transform lockedSymbol;

    void Awake()
    {
        lockedSymbol = transform.Find("Locked Symbol");

        stars = new List<Image>();
        foreach (Image star in GetComponentsInChildren<Image>())
            stars.Add(star);

        stars.RemoveAt(0);
    }

    public void ToggleStarImages(int starScore)
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i].gameObject.SetActive(true);
            stars[i].sprite = (i <= starScore - 1) ? fullStar : emptyStar;
        }
    }

    public void ToggleButton(bool? isUnlocked)
    {
        switch(isUnlocked)
        {
            case true:
                GetComponent<Image>().sprite = unlockedBackground;
                break;
            case false:
                lockedSymbol.gameObject.SetActive(true);
                GetComponent<Button>().enabled = false;
                GetComponent<Image>().sprite = lockedBackground;
                break;
            default:
                return;
        }
    }
}
