using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    private void Awake()
    {
        Inst = this;
    }

    bool isPaused;
    public GameObject transitionIn;
    public int scene;
    
    
    public AudioClip transitionOutSound;
    bool isLevelSwitch = false;
    public AudioSource backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isLevelSwitch)
        {
            backgroundMusic.volume -= 1;
        }
    }

    //loads a given level. plays the transition animation
    public void nextScene(int sceneNumber)
    {
        StartCoroutine(loadScene(sceneNumber));
    }
     IEnumerator loadScene(int sceneNumber)
    {
        isLevelSwitch = true;
        transitionIn.GetComponent<AudioSource>().PlayOneShot(transitionOutSound);
        transitionIn.SetActive(true);
        transitionIn.transform.GetComponent<Animator>().SetBool("isExiting", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneNumber);
        
    }
}
