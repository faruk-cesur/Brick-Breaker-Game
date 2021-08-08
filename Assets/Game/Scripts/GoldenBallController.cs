using System;
using System.Collections;
using UnityEngine;

public class GoldenBallController : MonoBehaviour
{
    // Variables Defined.
    public BallController ballController;
    private Rigidbody2D _rb;


    // Rigidbody is defining in awake method
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Golden Ball will move up when it spawns.
    private void Start()
    {
        _rb.velocity = Vector2.up * 10f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Golden Ball game object is SettingActive to false when it dies.
        DeathArea deathArea = other.gameObject.GetComponentInParent<DeathArea>();

        if (deathArea)
        {
            gameObject.SetActive(false);
        }

        // Spawning a golden ball, so we will have one more ball to destroy bricks until it falls 
        PowerGoldenBall powerGoldenBall = other.gameObject.GetComponentInParent<PowerGoldenBall>();

        if (powerGoldenBall)
        {
            var newBall = Instantiate(ballController.goldenBall, transform.position, Quaternion.identity, null);
            newBall.GetComponent<SpriteRenderer>().color = Color.yellow;
            other.gameObject.SetActive(false);
        }

        // Ball is scaling up, changing color to red and got a new power that destroys bricks with one shot. For 10 seconds.
        PowerForce powerForce = other.gameObject.GetComponentInParent<PowerForce>();

        if (powerForce)
        {
            StartCoroutine(ballController.PowerForceDuration());
            other.gameObject.SetActive(false);
        }

        // Player (red line) is getting bigger for 10 seconds.
        PowerTallerPlayer powerTallerPlayer = other.gameObject.GetComponentInParent<PowerTallerPlayer>();

        if (powerTallerPlayer)
        {
            StartCoroutine(ballController.PowerTallerDuration());
            other.gameObject.SetActive(false);
        }
    }
}