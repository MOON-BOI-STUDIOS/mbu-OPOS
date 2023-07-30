using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLocationManager : MonoBehaviour
{
    public Transform[] spawnLocations;
    public Transform player;
    public GameObject teleportDre;
    public SpriteRenderer dreSprite;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("LastLocation",0) == 0)
        {
            teleportDre.SetActive(true);
            player.GetComponent<PlayerController>().enabled = false;
            dreSprite.enabled = false;
            StartCoroutine(teleportDreSequence());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //sets the player location at the start of the scene, according to where they were before
        player.position = spawnLocations[PlayerPrefs.GetInt("LastLocation")].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator teleportDreSequence()
    {
        yield return new WaitForSeconds(2);
        player.GetComponent<PlayerController>().enabled = true;
        dreSprite.enabled = true;
        Destroy(teleportDre);
    }
}
