using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text highScoreText;

    private void Awake()
    {
        // play menuMusic
        FindObjectOfType<AudioManager>().Play("menuMusic");
    }

    private void Start()
    {
        highScoreText.text = "HighScore \n" + PlayerPrefs.GetInt("Highscore");
    }

    // Quit Button
    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("<color=red> Game Closed </color>");
    }

    // Start Button
    public void StartGame()
    {
        // Uncomment the line below to load the game
        SceneManager.LoadScene("MainGame"); 

        Debug.Log("<color=green> Game Started </color>");
    }

    // Add More Code Here
}
