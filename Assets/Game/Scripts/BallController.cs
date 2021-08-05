using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public PlayerController playerController;
    public BrickSpawner brickSpawner;
    private bool isStarted;
    private Rigidbody2D rb;
    [HideInInspector] public Vector3 ballStartPos;


    void Start()
    {
        ballStartPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.SetParent(playerController.gameObject.transform);
    }

    private void Update()
    {
        if (isStarted == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.transform.SetParent(null);
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
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isStarted = false;
        }
    }
}