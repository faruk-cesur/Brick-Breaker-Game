using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickSpawner : MonoBehaviour
{
    
    // Variables Defined.
    public List<GameObject> bricks;
    public List<GameObject> spawnedBricks;
    [HideInInspector] public GameObject spawnedBrick;
    [HideInInspector] public int randomBrick;
    [HideInInspector] public int verticalBricks = 0;
    [HideInInspector] public Vector3 brickPos;
    [HideInInspector] public Vector3 tempBrickPos;
    [HideInInspector] public Vector3 brickPosY;
    [HideInInspector] public Vector3 brickPosX;
    public GameObject parentBrick;


    // Keeping brick positions on a temp variable so we could get back to it.
    private void Awake()
    {
        spawnedBricks = new List<GameObject>();
        brickPos = new Vector3(-2f, 3f, 0f);
        tempBrickPos = brickPos;
        brickPosY = new Vector3(0f, 0.5f, 0f);
        brickPosX = new Vector3(1f, 0f, 0f);
    }

    private void Start()
    {
        StartingBricks();
    }


    // Spawning 5 * 5 total 25 bricks on game start.
    private void StartingBricks()
    {
        for (int i = 0; i < 5; i++)
        {
            if (verticalBricks < 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    randomBrick = Random.Range(0, 6);
                    spawnedBrick = Instantiate(bricks[randomBrick], brickPos, Quaternion.identity,
                        parentBrick.transform);
                    brickPos = brickPos - brickPosY;
                    spawnedBricks.Add(spawnedBrick);
                }
            }

            verticalBricks++;
            brickPos = tempBrickPos;
            brickPos = brickPos + brickPosX;
            brickPosX = brickPosX + new Vector3(1, 0, 0);
        }
    }
}