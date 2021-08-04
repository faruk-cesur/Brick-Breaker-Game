using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [Header("Components")] private Rigidbody2D rb;
    private bool isStarted;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void Update()
    {
        if (isStarted == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddForce(Vector3.up * 300f);
                isStarted = true;
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DeathArea deathArea = other.gameObject.GetComponentInParent<DeathArea>();
        
        if (deathArea)
        {
            foreach (var brick in GameManager.Instance.spawnedBricks)
            {
                brick.transform.position -= new Vector3(0, 0.5f, 0);
            }
            GameManager.Instance.brickPos = GameManager.Instance.tempBrickPos;
            
            for (int i = 0; i < 5; i++)
            {
                GameManager.Instance.randomBrick = Random.Range(0, 6);
                GameManager.Instance.spawnedBrick = Instantiate(GameManager.Instance.bricks[GameManager.Instance.randomBrick], GameManager.Instance.brickPos, Quaternion.identity);
                GameManager.Instance.brickPos = GameManager.Instance.brickPos + new Vector3(1,0,0);
                GameManager.Instance.spawnedBricks.Add(GameManager.Instance.spawnedBrick);
            }

            transform.position = GameManager.Instance.ballStartPos;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isStarted = false;
        }
    }
}