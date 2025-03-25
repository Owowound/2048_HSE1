using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool[,] Positions;
    public int[,] values;
    public int MaxScore;

    public GameData() { }
    public GameData(bool[,] positions, int[,] values, int maxScore)
    {
        Positions = positions;
        this.values = values;
        MaxScore = maxScore;
    }
}
