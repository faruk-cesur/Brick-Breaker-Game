using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Targets")]
    public Transform ball;
    public Transform cam;
    private int _activeSceneIndex;
    public List<GameObject> bricks;
    private int _randomBrick;
    private Vector3 _brickPos;
    private Vector3 _brickPosY;
    private Vector3 _brickPosX;

    private void Awake()
    {
        Instance = this;
        _brickPos = new Vector3(0f,4f,0f);
        _brickPosY = new Vector3(0f,0.5f,0f);
        _brickPosX = new Vector3(1f,0f,0f);
    }

    void Start()
    {
        _randomBrick = Random.Range(0, 5);
        Instantiate(bricks[_randomBrick], _brickPos, Quaternion.identity);
        _randomBrick = Random.Range(0, 5);
        Instantiate(bricks[_randomBrick], _brickPos-_brickPosY, Quaternion.identity);
        _randomBrick = Random.Range(0, 5);
        Instantiate(bricks[_randomBrick], _brickPos-_brickPosX, Quaternion.identity);
        _randomBrick = Random.Range(0, 5);
        Instantiate(bricks[_randomBrick], _brickPos+_brickPosX, Quaternion.identity);
        _randomBrick = Random.Range(0, 5);
        _activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
            SceneManager.LoadScene(_activeSceneIndex + 1);
        }   
    }
}
