﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    [SerializeField]
    private int coinValue = 20;

    [SerializeField]
    private int _maxScore = 0;
    [SerializeField]
    private int _curScore = 0;

    [SerializeField]
    private Text scoreText;

    private int startTotal = 0;

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

    private void CalculateStarScore()
    {
        startTotal = 3 * (curScore / maxScore);
    }
}