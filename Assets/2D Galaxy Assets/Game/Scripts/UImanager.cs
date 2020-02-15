using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{

    public Sprite[] lives;

    public Image livesImageDisplay;

    public int score;
    public Text scoreText;

    public GameObject title;

    // update display
    public void UpdateLives( int currentLives)
    {
        Debug.Log("Lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
        
    }
    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
       
    }

    public void DisableTitleScreen()
    {
        title.SetActive(false);
                        
    }

    public void ShowTitleScreen()
    {
        title.SetActive(true);
       

    }
}
