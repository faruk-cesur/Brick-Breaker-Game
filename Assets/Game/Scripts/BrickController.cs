using TMPro;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    // Variables Defined.
    public int health;
    private bool _isCollide;


    private void OnCollisionEnter2D(Collision2D other)
    {
        // Calculating brick healths and setactive false when they out of HP
        
        BallController ball = other.gameObject.GetComponentInParent<BallController>();
        GoldenBallController goldenBall = other.gameObject.GetComponentInParent<GoldenBallController>();

        if (ball || goldenBall)
        {
            if (GameManager.instance.powerForce == false)
            {
                health -= 1;  // if we don't have powerForce Buff, we will decrease 1 HP for each hit
                GetComponentInChildren<TMP_Text>().text = health.ToString();
                if (health <= 0)
                {
                    GameManager.instance.score++;
                    GameManager.instance.brickNumbers -= 1;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                health -= 6; // if we have powerForce Buff, we will decrease 6 HP for each hit (it means one hit everything)
                GetComponentInChildren<TMP_Text>().text = health.ToString();
                if (health <= 0)
                {
                    GameManager.instance.score++;
                    GameManager.instance.brickNumbers -= 1;
                    gameObject.SetActive(false);
                }
            }
        }

        
        // When player (red line) touches the bricks, game is over.
        PlayerController playerController = other.gameObject.GetComponentInParent<PlayerController>();

        if (playerController)
        {
            if (_isCollide == false)
            {
                GameManager.instance.gameOverUI.SetActive(true);
                GameManager.instance.mainGameUI.SetActive(false);
                GameManager.instance.bricksUI.SetActive(false);
                playerController.gameObject.SetActive(false);
                ball.gameObject.SetActive(false);
                _isCollide = true;
                GameManager.instance.brickNumbers = 0;
            }
        }
    }
}