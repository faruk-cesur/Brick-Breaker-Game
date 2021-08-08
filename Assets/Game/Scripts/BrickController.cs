using TMPro;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public int health;
    private bool _isCollide;
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        BallController ball = other.gameObject.GetComponentInParent<BallController>();
        GoldenBallController goldenBall = other.gameObject.GetComponentInParent<GoldenBallController>();

        if (ball ||goldenBall)
        {
            health -= 1;
            GetComponentInChildren<TMP_Text>().text = health.ToString();
            if (health <= 0)
            {
                GameManager.instance.score++;
                GameManager.instance.brickNumbers -= 1;
                gameObject.SetActive(false);
            }
        }

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