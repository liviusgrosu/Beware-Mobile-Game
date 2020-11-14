using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenUI : MonoBehaviour
{
    public Text presentedByText, companyNameText;
    public float fadingIncrement = 0.1f;

    public MainMenuMaster menuMaster;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("initial_app_open", 1) == 0)
        {
            menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Title);
            return;
        }

        StartCoroutine(FadeInText(presentedByText));
        StartCoroutine(FadeInText(companyNameText));
    }

    private IEnumerator FadeInText(Text currText)
    {
        while(currText.color.a < 1f)
        {
            currText.color = new Color(currText.color.r, currText.color.g, currText.color.b, currText.color.a + fadingIncrement);
            yield return null;
        }
        StartCoroutine(WaitAndShowScreen(currText));
    }

    private IEnumerator WaitAndShowScreen(Text currText)
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FadeOutText(currText));
    }

    private IEnumerator FadeOutText(Text currText)
    {
        while (currText.color.a > 0f)
        {
            currText.color = new Color(currText.color.r, currText.color.g, currText.color.b, currText.color.a - fadingIncrement);
            yield return null;
        }
        menuMaster.ChangeToPage(MainMenuMaster.MenuPage.Title);
        PlayerPrefs.SetInt("initial_app_open", 0);
    }
}
