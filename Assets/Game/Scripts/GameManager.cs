using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // Variables Defined.
    public static GameManager instance;
    public static Camera cam;
    [HideInInspector] public int score;
    [HideInInspector] public int brickNumbers;
    public TMP_Text scoreText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverBestScoreText;
    public TMP_Text ballIsDeadText;
    public TMP_Text newBricksSpawnedText;
    public TMP_Text roundCompleteText;
    public Slider collisionSlider;
    public GameObject gameOverUI;
    public GameObject mainGameUI;
    public GameObject winGameUI;
    public GameObject bricksUI;
    [HideInInspector] public bool powerForce;


    // GameManager Singleton Design Pattern
    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    
    // Closing Game Over and Win Screens when game starts and open MainGame UI
    private void Start()
    {
        gameOverUI.SetActive(false);
        mainGameUI.SetActive(true);
        winGameUI.SetActive(false);
    }

    // Defining scores to texts on each frame
    private void Update()
    {
        SetPlayerPrefs();
        scoreText.text = score.ToString();
        gameOverScoreText.text = scoreText.text;
        gameOverBestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }


    // It's a text with an animaton that will appear for 3 seconds.
    public IEnumerator NewBricksSpawnedText()
    {
        newBricksSpawnedText.enabled = true;
        yield return new WaitForSeconds(3f);
        newBricksSpawnedText.enabled = false;
    }

    // It's a text with an animaton that will appear for 3 seconds.
    public IEnumerator BallIsDeadText()
    {
        ballIsDeadText.enabled = true;
        yield return new WaitForSeconds(3f);
        ballIsDeadText.enabled = false;
    }

    // It's a text with an animaton that will appear for 3 seconds.
    public IEnumerator RoundCompleteText()
    {
        roundCompleteText.enabled = true;
        yield return new WaitForSeconds(3f);
        roundCompleteText.enabled = false;
    }

    // This is a button we can click when game is over. It restarts the game.
    public void PlayAgainButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    
    // Keeping the BestScore on HDD, It will stays forever even if you close the game.
    public void SetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        if (PlayerPrefs.GetInt("BestScore") < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
}