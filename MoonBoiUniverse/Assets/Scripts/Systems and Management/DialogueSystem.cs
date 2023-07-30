using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float proximity;
    public string[] dialogues;
    private Transform player;
    private int currentDialogue;
    bool triggeredDialogue;
    public GameObject interactButton;
    bool isClose;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDialogue = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the player is close to the NPC
        if (Vector2.Distance(player.position, transform.position) <= proximity + 1f)
        {
            isClose = true;
        }
        else
        {
            isClose = false;
        }

        if(isClose)
        {
            if (Vector2.Distance(player.position, transform.position) <= proximity)
        {
                interactButton.SetActive(true);
            }
        else
            {
                text.text = "";
                interactButton.SetActive(false);
            }
        }
        



            if (Vector2.Distance(player.position, transform.position) > proximity)
        {
            currentDialogue = -1;
        }
    }


        
    //triggers the current dialogue, depending on the current dialogue integer, which increases with every button press
    public void TriggerDialogue()
    {
        if (Vector2.Distance(player.position, transform.position) <= proximity)
        {
            
            currentDialogue++;

            if (currentDialogue> dialogues.Length - 1)
            {
                text.text = "";
            }
            else
            {
                triggeredDialogue = true;
                text.text = dialogues[currentDialogue];
                
            }
        }
        
    }
}
