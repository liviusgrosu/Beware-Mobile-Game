using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour, IUIElement
{

    private bool isUIActive;

    [SerializeField]
    private int coinValue = 20;

    [SerializeField]
    private int _maxScore = 0;
    [SerializeField]
    private int _curScore = 0;

    [SerializeField]
    private Text scoreText;

    public int starTotal = 0;

    private int maxScore 
    { 
        get { return _maxScore; }
        set { _maxScore = value; }
    }
    private int curScore 
    {
        get { return _curScore; }
        set { 
            _curScore = value;
            scoreText.text = _curScore.ToString();
            starTotal = (int)(3 * ((float)curScore / (float)maxScore));
        }
    }

    public void AddMaxScore()
    {
        maxScore += coinValue;
    }

    public void AddCurrentScore()
    {
        curScore += coinValue;
    }

    public void ToggleUI(bool state)
    {
        isUIActive = state;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isUIActive);
        }
    }
}
