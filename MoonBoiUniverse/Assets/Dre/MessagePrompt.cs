using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessagePrompt : MonoBehaviour
{
    public Transform player;
    public float minDistance;
    private bool hasAppeared = false;
    public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < minDistance)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            hasAppeared = true;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            if(hasAppeared)
            {
                Destroy(gameObject);
            }
        }
    }

    public void startCor()
    {
        StartCoroutine(changeScene());
    }
    public IEnumerator changeScene()
    {
        yield return new WaitForSeconds(3f);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
