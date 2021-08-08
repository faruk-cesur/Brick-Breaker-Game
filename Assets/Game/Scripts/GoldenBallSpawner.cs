using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class GoldenBallSpawner : MonoBehaviour
{
    public GameObject powerGoldenBall;
    public int randomSpawnPosY;
    public int randomSpawnPosX;
    public Vector3 spawnPos;


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
        } 
    }
}