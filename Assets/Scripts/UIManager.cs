using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Skoru ekrana yazdýrýr ve eðer highscore geçilmiþse skor rengini deðiþtirir.
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PlayerCollision playerCollision;
    private int highScore;

    private void Start()
    {

        scoreManager.OnScoreChange += ScoreManager_OnScoreChange;
    }

    private void ScoreManager_OnScoreChange(object sender, int e)
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScore < (int)e)
        {
            currentScoreText.color = Color.yellow;
        }
        currentScoreText.text =  (int)e +":Score ";
    }
}
