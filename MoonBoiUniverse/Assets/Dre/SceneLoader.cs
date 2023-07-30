using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int scene;
    public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {

            StartCoroutine(loadScene());
        }
    }

    IEnumerator loadScene()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
