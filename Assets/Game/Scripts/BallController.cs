using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public PlayerController playerController;
    public BrickSpawner brickSpawner;
    private bool _isStarted;
    private Rigidbody2D _rb;
    private int _collisionMeter;
    [HideInInspector] public Vector3 ballStartPos;


    void Start()
    {
        ballStartPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        gameObject.transform.SetParent(playerController.gameObject.transform);
    }

    private void Update()
    {
        if (_isStarted == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.transform.SetParent(null);
                _rb.constraints = RigidbodyConstraints2D.None;
                _rb.AddForce(Vector3.up * 300f);
                _isStarted = true;
            }
        }
    }

    private void MoveDownSpawnBricks()
    {
        foreach (var brick in brickSpawner.spawnedBricks)
        {
            brick.transform.position -= new Vector3(0, 0.5f, 0);
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

        transform.position = ballStartPos;
        playerController.gameObject.transform.position = new Vector3(0, -4, 0);
        gameObject.transform.SetParent(playerController.gameObject.transform);
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _isStarted = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DeathArea deathArea = other.gameObject.GetComponentInParent<DeathArea>();

        if (deathArea)
        {
            MoveDownSpawnBricks();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();

        if (player)
        {
            _collisionMeter++;
            if (_collisionMeter % 25 == 0)
            {
                MoveDownSpawnBricks();
            }
        }
    }
}