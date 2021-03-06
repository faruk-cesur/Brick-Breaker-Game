using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    
    // Variables Defined.
    public PlayerController playerController;
    public PowerUpSpawner powerUpSpawner;
    public BrickSpawner brickSpawner;
    
    [HideInInspector] public Vector3 ballStartPos;
    
    public GameObject goldenBall;

    private Rigidbody2D _rb;
    
    private bool _isStarted;
    private bool _isTaller;
    
    private int _collisionMeter;
    
    private float _elapsedTime;


    // Ball scale, position and color values resets on start.
    void Start()
    {
        if (_isTaller == false)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        ballStartPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        gameObject.transform.SetParent(playerController.gameObject.transform);
    }

    // Calling StartMove and FixBug Methods On Update
    private void Update()
    {
        StartMove();
        FixBugBallPosition();
    }

    // Ball Start Moving Up When Clicking Left Mouse Button Or Tap
    public void StartMove()
    {
        if (_isStarted == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.transform.SetParent(null);
                _rb.constraints = RigidbodyConstraints2D.None;
                _rb.velocity = Vector2.up * 10f;
                _isStarted = true;
            }
        }
    }

    // If Ball is stuck somewhere, it will be reset in 10 seconds.
    private void FixBugBallPosition()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= 10 && _isStarted == true)
        {
            transform.position = ballStartPos;
            playerController.gameObject.transform.position = new Vector3(0, -4, 0);
            gameObject.transform.SetParent(playerController.gameObject.transform);
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _isStarted = false;
            _elapsedTime = 0;
        }
    }

    
    // Moving down all of the bricks has spawned, and create new ones on top of them.
    public void MoveDownSpawnBricks()
    {
        foreach (var brick in brickSpawner.spawnedBricks)
        {
            brick.transform.position -= new Vector3(0, 0.5f, 0);
        }

        foreach (var powerUp in powerUpSpawner.spawnedPowerUps)
        {
            powerUp.transform.position -= new Vector3(0, 0.5f, 0);
        }

        brickSpawner.brickPos = brickSpawner.tempBrickPos;

        for (int i = 0; i < 5; i++)
        {
            brickSpawner.randomBrick = Random.Range(0, 6);
            brickSpawner.spawnedBrick =
                Instantiate(brickSpawner.bricks[brickSpawner.randomBrick],
                    brickSpawner.brickPos, Quaternion.identity, brickSpawner.parentBrick.transform);
            brickSpawner.brickPos = brickSpawner.brickPos + new Vector3(1, 0, 0);
            brickSpawner.spawnedBricks.Add(brickSpawner.spawnedBrick);
        }

        powerUpSpawner.SpawnPowerUpsEachRound();
        GameManager.instance.brickNumbers += 5;
        transform.position = ballStartPos;
        playerController.gameObject.transform.position = new Vector3(0, -4, 0);
        gameObject.transform.SetParent(playerController.gameObject.transform);
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _isStarted = false;
    }

    // Only Spawn One Line Bricks.
    public void OnlySpawnBricks()
    {
        brickSpawner.brickPos = brickSpawner.tempBrickPos;

        for (int i = 0; i < 5; i++)
        {
            brickSpawner.randomBrick = Random.Range(0, 6);
            brickSpawner.spawnedBrick =
                Instantiate(brickSpawner.bricks[brickSpawner.randomBrick],
                    brickSpawner.brickPos, Quaternion.identity, brickSpawner.parentBrick.transform);
            brickSpawner.brickPos = brickSpawner.brickPos + new Vector3(1, 0, 0);
            brickSpawner.spawnedBricks.Add(brickSpawner.spawnedBrick);
        }

        GameManager.instance.brickNumbers += 5;
        transform.position = ballStartPos;
        playerController.gameObject.transform.position = new Vector3(0, -4, 0);
        gameObject.transform.SetParent(playerController.gameObject.transform);
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _isStarted = false;
    }

    // Win Screen For 3 Seconds
    IEnumerator ShowWinScreen()
    {
        GameManager.instance.winGameUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        GameManager.instance.winGameUI.SetActive(false);
    }

    
    // Ball is scaling up, changing color to red and got a new power that destroys bricks with one shot. For 10 seconds.
    public IEnumerator PowerForceDuration()
    {
        GameManager.instance.powerForce = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0);
        yield return new WaitForSeconds(10f);
        GameManager.instance.powerForce = false;
        gameObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    
    // Player (red line) is getting bigger for 10 seconds.
    public IEnumerator PowerTallerDuration()
    {
        playerController.gameObject.transform.localScale += new Vector3(1, 0.5f, 0);
        playerController.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        _isTaller = true;
        yield return new WaitForSeconds(10f);
        if (gameObject.transform.parent == null)
        {
            playerController.gameObject.transform.localScale -= new Vector3(1, 0.5f, 0);
        }
        else
        {
            gameObject.transform.SetParent(null);
            playerController.gameObject.transform.localScale -= new Vector3(1, 0.5f, 0);
            gameObject.transform.SetParent(playerController.gameObject.transform);
        }

        playerController.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _isTaller = false;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Ball resets its position on death. And spawn new bricks for punishing.
        DeathArea deathArea = other.gameObject.GetComponentInParent<DeathArea>();

        if (deathArea)
        {
            MoveDownSpawnBricks();
            _collisionMeter = 0;
            GameManager.instance.collisionSlider.value = 0;
            StartCoroutine(GameManager.instance.NewBricksSpawnedText());
            StartCoroutine(GameManager.instance.BallIsDeadText());
        }

        
        // Spawning a golden ball, so we will have one more ball to destroy bricks until it falls 
        PowerGoldenBall powerGoldenBall = other.gameObject.GetComponentInParent<PowerGoldenBall>();

        if (powerGoldenBall)
        {
            var newBall = Instantiate(goldenBall, transform.position, Quaternion.identity, null);
            newBall.GetComponent<SpriteRenderer>().color = Color.yellow;
            other.gameObject.SetActive(false);
        }

        
        // Ball is scaling up, changing color to red and got a new power that destroys bricks with one shot. For 10 seconds.
        PowerForce powerForce = other.gameObject.GetComponentInParent<PowerForce>();

        if (powerForce)
        {
            StartCoroutine(PowerForceDuration());
            other.gameObject.SetActive(false);
        }

        
        // Player (red line) is getting bigger for 10 seconds.
        PowerTallerPlayer powerTallerPlayer = other.gameObject.GetComponentInParent<PowerTallerPlayer>();

        if (powerTallerPlayer)
        {
            StartCoroutine(PowerTallerDuration());
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        // If all of bricks have destroyed, player will earn 100 score and continue playing
        PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();

        if (player)
        {
            if (GameManager.instance.brickNumbers <= 0)
            {
                OnlySpawnBricks();
                GameManager.instance.score += 100;
                StartCoroutine(ShowWinScreen());
            }

            _elapsedTime = 0;
            _collisionMeter++;
            GameManager.instance.collisionSlider.value++;
            if (_collisionMeter % 20 == 0)
            {
                MoveDownSpawnBricks();
                GameManager.instance.collisionSlider.value = 0;
                StartCoroutine(GameManager.instance.RoundCompleteText());
                StartCoroutine(GameManager.instance.NewBricksSpawnedText());
            }
        }
    }
}