using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public GameObject boosterPrefab;
    public BikeController bikeController;
    public RaceGameManager raceGameManager; // Reference to the RaceGameManager script

    public int poolSize = 20; // Size of the object pool
    private Queue<GameObject> boosterPool; // The object pool

    public float intervalTime = 5.0f; // You can set this to the desired time interval in seconds

    public static BoosterManager Inst;
    private void Awake()
    {
        
            Inst = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the object pool
        boosterPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject booster = Instantiate(boosterPrefab, this.transform);
            booster.SetActive(false);
            boosterPool.Enqueue(booster);
        }

        StartCoroutine(PopulateBooster(Random.value < 0.5f));
    }

    public IEnumerator PopulateBooster(bool isRule2)
    {
        yield return new WaitForSeconds(15f);
        while (true)
        {
            // Generate a Booster
            GameObject booster = GetBoosterFromPool();
            if (booster != null)
            {
                float randomX = Random.Range(-4, 4);
                float y = bikeController.transform.position.y + 20;
                booster.transform.position = new Vector3(randomX, y, 0);
                booster.SetActive(true);
            }

            // If it's Rule 2, generate another Booster after boostTime
            if (isRule2)
            {
                float boostTime = raceGameManager.BoostTime; // Assuming boostTime is a public variable in RaceGameManager
                yield return new WaitForSeconds(boostTime);
                booster = GetBoosterFromPool();
                if (booster != null)
                {
                    float randomX = Random.Range(-4, 4);
                    float y = bikeController.transform.position.y + 20;
                    booster.transform.position = new Vector3(randomX, y, 0);
                    booster.SetActive(true);
                }

                // Wait for the intervalTime
                yield return new WaitForSeconds(intervalTime);
            }
            else
            {
                // Wait for the intervalTime
                yield return new WaitForSeconds(intervalTime);
            }

            // Decide the next rule: Rule 1 or Rule 2
            isRule2 = Random.value < 0.5f;
        }
    }


    // Get a Booster from the object pool
    private GameObject GetBoosterFromPool()
    {
        // If the pool is not empty, dequeue a Booster
        if (boosterPool.Count > 0)
        {
            return boosterPool.Dequeue();
        }

        // If the pool is empty, return null
        return null;
    }

    // Put a Booster back into the object pool
    public void PutBoosterBackInPool(GameObject booster)
    {
        booster.SetActive(false);
        boosterPool.Enqueue(booster);
    }
}
