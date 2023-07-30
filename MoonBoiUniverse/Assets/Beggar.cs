using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Beggar : MonoBehaviour
{

    public string[] jokes;
    public TextMeshProUGUI text;
    public GameObject InteractButton, buyJokesButton;
    Transform player;
    public float proximity;
    bool endConversation;
    public AudioClip cursorSound;


    private void Start()
    {
        //gets the transform of the player object
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //PC Controls
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }


        //enables/disables the interact button depending on how close the player is to this object. Also removes all text from the dialogue subtitle if the player is far enough
        if (Vector2.Distance(player.position, transform.position) <= proximity)
        {
            InteractButton.SetActive(true);
            
        }
        else
        {
            InteractButton.SetActive(false);
            buyJokesButton.SetActive(false);
            text.text = "";
            endConversation = false;
        }
    }
    public void Interact()
    {
        GetComponent<AudioSource>().PlayOneShot(cursorSound);
        if (!endConversation)
        {
            //used to trigger first dialogue
            text.text = "Excuse me, stranger. Could you spare some change for a hungry old geezer? I'll make you laugh in return. Just spare me 5 coins";
            buyJokesButton.SetActive(true);
            endConversation = true;
        }
        else
        {
            //used to end the interaction
            endConversation = false;
            buyJokesButton.SetActive(false);
            text.text = "";
        }
        
    }


    public void tellJoke()
    {
        //when the player buys a joke. selects a randon joke from an array of strings 
        GetComponent<AudioSource>().PlayOneShot(cursorSound);
        buyJokesButton.SetActive(false);

        if(PlayerPrefs.GetInt("Coins") >= 5)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 5);
            text.text = jokes[Random.Range(0, jokes.Length)];
        }
        else
        {
            //used to end the interaction
            endConversation = false;
            buyJokesButton.SetActive(false);
            text.text = "";
        }
        
        
    }
}
