using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    private PlayerManager _manager;
    public float comboTimer = 1;
    public int comboCounter;
    public int numberOfComboHits;
    public float comboIntervalMax;
    public TextMeshProUGUI comboText;
    public Transform comboIndicatorParent;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(comboTimer, 0, 1);
        comboTimer += comboIntervalMax * Time.deltaTime;
        if (comboTimer >= 1) comboCounter = 0;

       
        if (comboCounter < 10) comboText.text = "0" + comboCounter.ToString();
        if (comboCounter >= 10) comboText.text =  comboCounter.ToString();

        comboIndicatorParent.localScale = new Vector3((float)comboCounter / (float)numberOfComboHits * 1, comboIndicatorParent.localScale.y, comboIndicatorParent.localScale.z);

        //PC Controls
#if UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetMouseButtonDown(0))
        {
            onAttack();
        }
#endif
    }

    public void onAttack()
    {
       // if (comboTimer <= 1) comboCounter += 1;
        comboTimer = 0;
        _manager._animator.GetComponent<Animator>().SetTrigger("Attack");
    }
}
