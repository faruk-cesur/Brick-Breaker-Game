using TMPro;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public int health;
    public static int brickNumbers;
    public bool breakingBricks;
    private bool _isCollide;


    void Start()
    {
        breakingBricks = (this.CompareTag("Brick"));
        if (breakingBricks)
        {
            brickNumbers++;
        }

        GameManager.instance = GameObject.FindObjectOfType<GameManager>();
    }   


    private void OnCollisionEnter2D(Collision2D other)
    {
        BallController ballController = other.gameObject.GetComponentInParent<BallController>();

        if (ballController)
        {
            health -= 1;
            GetComponentInChildren<TMP_Text>().text = health.ToString();
            if (health <= 0)
            {
                GameManager.instance.score++;
                brickNumbers -= 1;
                gameObject.SetActive(false);
                GameManager.instance.AfterScene();
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
                ballController.gameObject.SetActive(false);
                _isCollide = true;
            }
        }
    }
}