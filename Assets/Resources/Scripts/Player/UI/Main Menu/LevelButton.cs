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

    void Awake()
    {
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
}
