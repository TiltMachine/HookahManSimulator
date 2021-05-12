using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //public Text scoreVal;
    public Text endScoresVal;
   // public Text incScoreText;
   // public Text gameOverText;

    public int scores;
    public int endScores;
    public int scoreValue;
    public int growthRate;

    public bool gameOver;

    public void Update()
    {
        //scoreVal.text = scores.ToString("0");
        endScoresVal.text = endScores.ToString("0");


        if(gameOver == true)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if(endScores != scores && scores > endScores)
        {
            endScores += growthRate;
        }
    }
}
