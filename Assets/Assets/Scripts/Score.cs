using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int highScore = 0;

    public static void SaveScore () {
        SaveSystem.SaveScore();
    }
    public void LoadScore() {
        HighscoorData data = SaveSystem.LoadScore();

        highScore = data.highscore;
    }
}
