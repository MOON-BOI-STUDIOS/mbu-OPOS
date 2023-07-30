using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManaager : MonoBehaviour
{
    public Transform DreAnimation;
    public GameObject startButton;
    public Transform transition;
    public AudioClip powerUpSound;
    public AudioClip startButtonSound;
    public AudioClip transitionOutSound;
    public AudioSource musicPlayer;
    bool isLevelLoading = false;

    public GameObject moonboiStudioLogo, moonboiUniverseLogo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("firstLoad", 0);
        //sets the default values at the start of the game
        PlayerPrefs.SetInt("Coins", 20);
        PlayerPrefs.SetInt("MaxHealth", 500);
        PlayerPrefs.SetInt("SwordPower", 0);
        PlayerPrefs.SetInt("SpecialPower", 0);
        PlayerPrefs.SetInt("Fishes", 0);
        PlayerPrefs.SetInt("Round", 0);
        PlayerPrefs.SetInt("LastLocation", 0);

        StartCoroutine(loadMenu());
    }

    // Update is called once per frame
    void Update()
    {
        if(isLevelLoading)
        {
            musicPlayer.volume -= 1;
        }
        
        
    }

    //triggers powerup animation, disables start menu buttons, plays select sound as well as powrup sound
    public void startGame()
    {
        Destroy(startButton);
        DreAnimation.GetComponent<Animator>().SetTrigger("PowerUp");
        GetComponent<AudioSource>().PlayOneShot(powerUpSound);
        GetComponent<AudioSource>().PlayOneShot(startButtonSound);
    }


    //loads scene 1. triggers transition animation, plays transitionOut audio
    public void loadLevel()
    {
        StartCoroutine(nextLevel());
        GetComponent<AudioSource>().PlayOneShot(transitionOutSound);
        isLevelLoading = true;
    }

    
    IEnumerator nextLevel()
    {
        transition.gameObject.SetActive(true);
        transition.GetComponent<Animator>().SetBool("isExiting", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    //start funtion. triggers the studio logo off, after it is played through. enables background music, turns on the start buttons
    IEnumerator loadMenu()
    {
        
        yield return new WaitForSeconds(5f);
        moonboiStudioLogo.SetActive(false);
        moonboiUniverseLogo.SetActive(true);
        Camera.main.transform.GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(3.5f);
        moonboiUniverseLogo.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        startButton.SetActive(true);
    }
}
