using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public bool isGreenArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "greenArea")
        {
            isGreenArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "greenArea")
        {
            isGreenArea = false;
        }
    }
}
