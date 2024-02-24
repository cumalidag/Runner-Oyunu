using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Skorun artmasýný saðlar ve skoru hafýzada tutar.
    private int currentScore = 0;
    private int highScore = 0;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private PlayerMovement playerMovement;
    public event EventHandler<int> OnScoreChange;
    private int remainResetScore = 0;
    private int maxResetScore = 10000;
    private int someScore = 0;


    private void Start()
    {
        playerCollision.OnCoinCollide += PlayerCollision_OnScoreChange;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void PlayerCollision_OnScoreChange(object sender, EventArgs e)
    {
        currentScore += 100;
        someScore += 100;
    }

    private void Update()
    {
        if(!playerCollision.IsDead)
        {
            IncreaseScore();
            remainResetScore = maxResetScore - someScore;

            if(remainResetScore < 0)
            {
                someScore = 0;
            }

            if(currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }
        }
    }

    private void IncreaseScore()
    {
        currentScore = currentScore + (int)(550 * Time.deltaTime);
        someScore = someScore + (int)(550 * Time.deltaTime);
        OnScoreChange?.Invoke(this, currentScore);
    }
}
