using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
   
    public Transform[] spawnLocations;
    public GameObject[] voids;
    public Transform enemiesParent;
    bool waveSwitch;
    public GameObject powerUp;
    public GameObject endOfRoundScreen, controls;
    // Start is called before the first frame update
    private void Awake()
    {
        nextRound();
    }
    void Start()
    {

        PlayerPrefs.SetInt("LastLocation", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (enemiesParent.childCount <= 0 && waveSwitch == false )
        {
           
                PlayerPrefs.SetInt("Round", PlayerPrefs.GetInt("Round") + 1); 
                endOfRoundScreen.SetActive(true);
                waveSwitch = true;
            Destroy(GameObject.FindGameObjectWithTag("PowerUp"));

        }
    }

   public void spawnNormalVoids()
    {
        waveSwitch = false;
        Instantiate(voids[0], spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position, Quaternion.identity, enemiesParent);
    }

    public void spawnMixedVoids()
    {
        waveSwitch = false;
        Instantiate(voids[Random.Range(0, 5)], spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position, Quaternion.identity, enemiesParent);
    }

    public void nextRound()
    {
        endOfRoundScreen.SetActive(false);

        for (int i = 0; i < 2 + 2 * PlayerPrefs.GetInt("Round"); i++)
        {
            if (PlayerPrefs.GetInt("Round") <= 2)
            {
                Invoke("spawnNormalVoids", 0.0f);
            }
            else
            {
                Invoke("spawnMixedVoids", 0.0f);
            }

        }
    }

    //Spawn PowerUp Can at rounds 3,6 and every alternate round after round 8 onwards
    public void spawnPowerUp()
    {
        if(PlayerPrefs.GetInt("Round") == 3)
        {
            Instantiate(powerUp, spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("Round") == 6)
        {
            Instantiate(powerUp, spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position, Quaternion.identity);
        }

        if (PlayerPrefs.GetInt("Round") >= 8 && PlayerPrefs.GetInt("Round") % 2 == 0)
        {
            Instantiate(powerUp, spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position, Quaternion.identity);
        }
    }
}
