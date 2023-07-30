using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalEnemyManager : MonoBehaviour
{
    public int enemiesKilled;
    public TextMeshProUGUI text;
    public EnemySpawner spawner;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesKilled >= 21)
        {
            spawner.enabled = true;
        }
        text.text = "Monsters Killed : " + enemiesKilled;
    }
}
