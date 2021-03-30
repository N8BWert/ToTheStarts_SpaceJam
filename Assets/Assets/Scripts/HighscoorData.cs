using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoorData
{
    public int highscore;

    public HighscoorData () {
        highscore = Score.highScore;
    }
}
