using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public int score = 0;
    public GameObject titleScreen;

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore(int points)
    {
        score += points;

        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        scoreText.text = "Score: ";
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
    }
}
