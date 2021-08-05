using TMPro;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public int health;
    public static int brickNumbers;
    public bool breakingBricks;
    private bool isCollide;


    void Start()
    {
        breakingBricks = (this.tag == "Block");
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
                brickNumbers -= 1;
                gameObject.SetActive(false);
                GameManager.instance.AfterScene();
            }
        }

        PlayerController playerController = other.gameObject.GetComponentInParent<PlayerController>();

        if (playerController)
        {
            if (isCollide == false)
            {
                Debug.Log("player öldü");
                isCollide = true;
            }
            // todo game over ekranını göster ve playerprefs highscore göster & player setactive false yap
        }
    }
}