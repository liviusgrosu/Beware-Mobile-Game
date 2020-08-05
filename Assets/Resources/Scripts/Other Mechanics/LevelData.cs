using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public bool levelFinished;
    public int levelStarScore;

    public LevelData(string levelName, bool levelFinished, int levelStarScore)
    {
        this.levelName = levelName;
        this.levelFinished = levelFinished;
        this.levelStarScore = levelStarScore;
    }
}