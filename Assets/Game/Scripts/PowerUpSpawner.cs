using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerGoldenBall;
    public GameObject powerForce;
    private int randomSpawnPosY;
    private int randomSpawnPosX;
    private Vector3 spawnPos;


    private void Start()
    {
        SpawnGoldenBall();
    }


    private void SpawnGoldenBall()
    {
        for (int i = 0; i < 2; i++)
        {
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            Instantiate(powerGoldenBall, spawnPos, quaternion.identity);
            randomSpawnPosX = Random.Range(-2, 2);
            randomSpawnPosY = Random.Range(1, 3);
            spawnPos = new Vector3(randomSpawnPosX, randomSpawnPosY, 0);
            Instantiate(powerForce, spawnPos, quaternion.identity);
        }
    }
}