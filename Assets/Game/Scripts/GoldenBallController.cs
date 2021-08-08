using System;
using UnityEngine;

public class GoldenBallController : MonoBehaviour
{
    public BallController ballController;
    

    private Rigidbody2D _rb;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
        _rb.velocity = Vector2.up * 10f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DeathArea deathArea = other.gameObject.GetComponentInParent<DeathArea>();

        if (deathArea)
        {
            Destroy(gameObject);
        }

        PowerGoldenBall powerGoldenBall = other.gameObject.GetComponentInParent<PowerGoldenBall>();

        if (powerGoldenBall)
        {
            var newBall = Instantiate(ballController.goldenBall,transform.position,Quaternion.identity,null);
            newBall.GetComponent<SpriteRenderer>().color = Color.yellow;
            Destroy(other.gameObject);
        }
        
        PowerForce powerForce = other.gameObject.GetComponentInParent<PowerForce>();

        if (powerForce)
        {
            StartCoroutine(ballController.PowerForceDuration());
            Destroy(other.gameObject);
        }
    }
}