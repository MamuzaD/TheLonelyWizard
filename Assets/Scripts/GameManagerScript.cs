using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject deathObj;
    private TextMeshProUGUI deathText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject scoreObj;
    private TextMeshProUGUI scoreText;

    public Color deathColor;

    public void GameOver()
    {
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        deathText = deathObj.GetComponent<TextMeshProUGUI>();
        deathObj.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);
        scoreObj.SetActive(true);

        for (float t = 0; t < 1.0f; t += Time.deltaTime / 5f)
        {
            deathText.color = Color.Lerp(deathText.color, Color.red, 1f);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathObj.SetActive(false);
        restartButton.SetActive(false);
        scoreObj.SetActive(false);
    }

    public void Score(int score)
    {
        scoreText.text = "Score\n" + score;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); //main menu
    }
}
