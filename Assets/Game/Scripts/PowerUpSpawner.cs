using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUpSpawner : MonoBehaviour
{
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
        SpawnPowerUps();
    }


    private void SpawnPowerUps()
    {
        for (int i = 0; i < 2; i++)
        {
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            spawnedPowerUp = Instantiate(powerGoldenBall, spawnPos, quaternion.identity,powerUpsParent.gameObject.transform);
            spawnedPowerUps.Add(spawnedPowerUp);
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            spawnedPowerUp = Instantiate(powerForce, spawnPos, quaternion.identity,powerUpsParent.gameObject.transform);
            spawnedPowerUps.Add(spawnedPowerUp);
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            spawnedPowerUp = Instantiate(powerTaller, spawnPos, quaternion.identity,powerUpsParent.gameObject.transform);
            spawnedPowerUps.Add(spawnedPowerUp);
        }
    }

    public void SpawnPowerUpsEachRound()
    {
        Debug.Log("spawneachround");
        randomSpawnChance = Random.Range(1, 5);

        if (randomSpawnChance == 1)
        {
            randomSpawnPowerUp = Random.Range(1, 3);

            if (randomSpawnPowerUp == 1)
            {
                randomSpawnPosX = Random.Range(-2, 2);
                spawnPos = new Vector3(randomSpawnPosX, 3, 0);
                spawnedPowerUp =Instantiate(powerGoldenBall, spawnPos, quaternion.identity,powerUpsParent.gameObject.transform);
                spawnedPowerUps.Add(spawnedPowerUp);
            }

            if (randomSpawnPowerUp == 2)
            {
                randomSpawnPosX = Random.Range(-2, 2);
                spawnPos = new Vector3(randomSpawnPosX, 3, 0);
                spawnedPowerUp = Instantiate(powerForce, spawnPos, quaternion.identity,powerUpsParent.gameObject.transform);
                spawnedPowerUps.Add(spawnedPowerUp);
            }

            if (randomSpawnPowerUp == 3)
            {
                randomSpawnPosX = Random.Range(-2, 2);
                spawnPos = new Vector3(randomSpawnPosX, 3, 0);
                spawnedPowerUp = Instantiate(powerTaller, spawnPos, quaternion.identity,powerUpsParent.gameObject.transform);
                spawnedPowerUps.Add(spawnedPowerUp);
            }
        }
    }
}