using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public int levelStarScore;

    public LevelData(string levelName, int levelStarScore)
    {
        this.levelName = levelName;
        this.levelStarScore = levelStarScore;
    }
}