using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;


public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject pauseMenu;


    [SerializeField] private GameObject deathObj;
    private TextMeshProUGUI deathText;
    [SerializeField] private GameObject scoreObj;
    private TextMeshProUGUI scoreText;

    [SerializeField] private PlayerStatsScript playerStatsScript;
    public Color deathColor;
    public static bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1f; //start instead of restart bc it wont work idk
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerStatsScript.playerHealth > 0) {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        deathText = deathObj.GetComponent<TextMeshProUGUI>();
        deathMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathMenu.SetActive(false);
    }

    public void Score(int score)
    {
        scoreText.text = "Score\n" + score;
    }

    public void MainMenu()
    {   
        if (isPaused)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene(0); //main menu
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
