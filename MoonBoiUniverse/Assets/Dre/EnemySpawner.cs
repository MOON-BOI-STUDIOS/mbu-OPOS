using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GlobalEnemyManager _manager;
    public Transform player;
    public bool isPlayerOnTheLeft;
    public GameObject enemy;
    public GameObject bigEnemy;
    public Transform[] leftSpawnPoints;
    public Transform[] rightSpawnPoints;

    public float spawnRate = 1f;
    public float spawnRateIncrease = 0.1f; // the rate at which the spawn rate increases
    private float nextSpawnTime; // the time at which the next enemy will spawn

    void Start()
    {
        nextSpawnTime = Time.time + spawnRate; // set the initial spawn time
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x > transform.position.x)
        {
            isPlayerOnTheLeft = true;
        }
        else
        {
            isPlayerOnTheLeft = false;
        }

       
        // check if it's time to spawn a new enemy
        if (Time.time >= nextSpawnTime) 
        {

            // spawn a new enemy
            if (isPlayerOnTheLeft) spawnLeft();
            if (!isPlayerOnTheLeft) spawnRight();

            // increase the spawn rate
            
                spawnRate -= spawnRateIncrease;
            
            

            // set the time for the next enemy spawn
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    public void spawnLeft()
    {
        Instantiate(enemy, leftSpawnPoints[Random.Range(0, leftSpawnPoints.Length - 1)].position,Quaternion.identity, _manager.transform);
    }
    public void spawnRight()
    {
        Instantiate(enemy, rightSpawnPoints[Random.Range(0, rightSpawnPoints.Length - 1)].position, Quaternion.identity, _manager.transform);
    }

    public void spawnLeftBoss()
    {
        Instantiate(bigEnemy, leftSpawnPoints[Random.Range(0, leftSpawnPoints.Length - 1)].position, Quaternion.identity, _manager.transform);
    }
    public void spawnRightBoss()
    {
        Instantiate(bigEnemy, rightSpawnPoints[Random.Range(0, rightSpawnPoints.Length - 1)].position, Quaternion.identity, _manager.transform);
    }
}
