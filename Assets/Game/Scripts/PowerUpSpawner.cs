using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUpSpawner : MonoBehaviour
{
    // Variables Defined.
    public List<GameObject> spawnedPowerUps;
    [HideInInspector] public GameObject spawnedPowerUp;
    public GameObject powerGoldenBall;
    public GameObject powerForce;
    public GameObject powerTaller;
    public GameObject powerUpsParent;

    private int randomSpawnPosY;
    private int randomSpawnPosX;
    private int randomSpawnPowerUp;
    private int randomSpawnChance;
    private Vector3 spawnPos;


    private void Awake()
    {
        spawnedPowerUps = new List<GameObject>();
    }

    private void Start()
    {
        SpawnPowerUpsOnStart();
    }


    // PowerUps are spawning randomly in the bricks on start of the game
    private void SpawnPowerUpsOnStart()
    {
        for (int i = 0; i < 2; i++)
        {
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            spawnedPowerUp = Instantiate(powerGoldenBall, spawnPos, quaternion.identity,
                powerUpsParent.gameObject.transform);
            spawnedPowerUps.Add(spawnedPowerUp);
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            spawnedPowerUp = Instantiate(powerForce, spawnPos, quaternion.identity,
                powerUpsParent.gameObject.transform);
            spawnedPowerUps.Add(spawnedPowerUp);
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            spawnedPowerUp = Instantiate(powerTaller, spawnPos, quaternion.identity,
                powerUpsParent.gameObject.transform);
            spawnedPowerUps.Add(spawnedPowerUp);
        }
    }

    
    // Powerups are spawning for %20 chance for every single brick line.
    public void SpawnPowerUpsEachRound()
    {
        randomSpawnChance = Random.Range(1, 4);

        if (randomSpawnChance == 1)
        {
            randomSpawnPowerUp = Random.Range(1, 3);

            if (randomSpawnPowerUp == 1)
            {
                randomSpawnPosX = Random.Range(-2, 2);
                spawnPos = new Vector3(randomSpawnPosX, 3, 0);
                spawnedPowerUp = Instantiate(powerGoldenBall, spawnPos, quaternion.identity,
                    powerUpsParent.gameObject.transform);
                spawnedPowerUps.Add(spawnedPowerUp);
            }

            if (randomSpawnPowerUp == 2)
            {
                randomSpawnPosX = Random.Range(-2, 2);
                spawnPos = new Vector3(randomSpawnPosX, 3, 0);
                spawnedPowerUp = Instantiate(powerForce, spawnPos, quaternion.identity,
                    powerUpsParent.gameObject.transform);
                spawnedPowerUps.Add(spawnedPowerUp);
            }

            if (randomSpawnPowerUp == 3)
            {
                randomSpawnPosX = Random.Range(-2, 2);
                spawnPos = new Vector3(randomSpawnPosX, 3, 0);
                spawnedPowerUp = Instantiate(powerTaller, spawnPos, quaternion.identity,
                    powerUpsParent.gameObject.transform);
                spawnedPowerUps.Add(spawnedPowerUp);
            }
        }
    }
}