using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            AICarController carController = collision.gameObject.GetComponent<AICarController>();
            if (!AICarManager.Inst.activeAICarControllers.Contains(carController))
                AICarManager.Inst.activeAICarControllers.Add(carController);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            AICarController carController = collision.gameObject.GetComponent<AICarController>();
                AICarManager.Inst.activeAICarControllers.Remove(carController);
            //StartCoroutine(DelaysetActive(collision.gameObject));
        }
    }
}
