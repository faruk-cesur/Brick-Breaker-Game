using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Camera cam;
    [HideInInspector] public int score;
    [HideInInspector]public int brickNumbers;
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


    private void Start()
    {
        gameOverUI.SetActive(false);
        mainGameUI.SetActive(true);
        winGameUI.SetActive(false);
    }

    private void Update()
    {
        SetPlayerPrefs();
        scoreText.text = score.ToString();
        gameOverScoreText.text = scoreText.text;
        gameOverBestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
    

    public IEnumerator NewBricksSpawnedText()
    {
        newBricksSpawnedText.enabled = true;
        yield return new WaitForSeconds(3f);
        newBricksSpawnedText.enabled = false;
    }

    public IEnumerator BallIsDeadText()
    {
        ballIsDeadText.enabled = true;
        yield return new WaitForSeconds(3f);
        ballIsDeadText.enabled = false;
    }

    public IEnumerator RoundCompleteText()
    {
        roundCompleteText.enabled = true;
        yield return new WaitForSeconds(3f);
        roundCompleteText.enabled = false;
    }

    public void PlayAgainButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

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