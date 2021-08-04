using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Targets")]
    public Transform ball;
    public Transform cam;
    private int activeSceneIndex;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        CheckBallPosition();
    }

    private void CheckBallPosition() {
        if(ball.position.y <= cam.position.y - 7f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AfterScene() {
        if(BrickController.BlockNumbers <= 0) {
            SceneManager.LoadScene(activeSceneIndex + 1);
        }   
    }
}
