using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("Targets")]
    public Transform ball;
    public Transform cam;
    private int activeSceneIndex;
    void Start()
    {
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        checkBallPos();
    }

    private void checkBallPos() {
        if(ball.position.y <= cam.position.y - 7f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void afterScene() {
        if(Block_Controller.blockNumbers <= 0) {
            SceneManager.LoadScene(activeSceneIndex + 1);
        }   
    }
}
