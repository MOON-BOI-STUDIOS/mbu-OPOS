using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RaceGameUIManager : MonoBehaviour
{
    [SerializeField] Transform Lives;
    [SerializeField] TMP_Text _score;
    [SerializeField] GameObject Boost;
    [SerializeField] Image BoostFill;


    public static RaceGameUIManager Inst;

    private void Awake()
    {
        Inst = this;
    }

    public void UpdateLives()
    {
        // Enable the lives up to the LivesCount
        for (int i = 0; i < RaceGameManager.inst.LivesCount; i++)
        {
            Lives.GetChild(i).gameObject.SetActive(true);
        }

        // Disable the remaining lives
        for (int i = RaceGameManager.inst.LivesCount; i < Lives.childCount; i++)
        {
            Lives.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void UpdateScore()
    {
        _score.text = ((int)RaceGameManager.inst.score).ToString();
    }

    public IEnumerator BoostStart(float time)
    {
        Boost.SetActive(true);
        float elapsed = 0f;
        BoostFill.fillAmount = 1;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            BoostFill.fillAmount = Mathf.Lerp(1, 0, elapsed / time);
            yield return null;
        }

        BoostFill.fillAmount = 0;
    }

    public void boostStop()
    {
        Boost.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
