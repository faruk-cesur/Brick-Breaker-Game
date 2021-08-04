using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public int health;
    public static int BlockNumbers;
    public bool breakingBlocks;
    void Start()
    {
        breakingBlocks = (this.tag == "Block");
        if(breakingBlocks) {
            BlockNumbers++;
        }

        GameManager.Instance = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        health -= 1;
        if(health <= 0) {
            BlockNumbers -= 1;
            Destroy(gameObject);
            GameManager.Instance.AfterScene();
        }
    }
}
