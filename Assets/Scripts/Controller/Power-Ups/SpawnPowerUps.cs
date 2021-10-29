using ComunixTest.Controller;
using ComunixTest.Model;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField] Transform powerUpsParent;
    [SerializeField] float timeBetweenSpawns = 10f;
    float timer = 0;
    [SerializeField] List<PowerUp> powerUps = new List<PowerUp>();
    public static bool isSpawned = false;

    void Start()
    {
        timer = timeBetweenSpawns;
    }

    void Update()
    {
        if (!isSpawned)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnRandomly();
                timer = timeBetweenSpawns;
                isSpawned = true;
            }
        }
    }

    void SpawnRandomly()
    {
        Vector2 spawnPos = new Vector2(Random.Range(GameManager.leftBound - 2, GameManager.rightBound - 2), powerUpsParent.position.y);
        GameObject spawnedItem = Instantiate(powerUps[Random.Range(0, powerUps.Count)].gameObject, spawnPos, Quaternion.identity, powerUpsParent);
    }
}