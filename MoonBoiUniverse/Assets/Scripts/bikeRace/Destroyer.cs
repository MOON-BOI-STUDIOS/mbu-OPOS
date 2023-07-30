using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            AICarController carController = collision.gameObject.GetComponent<AICarController>();
            if (!AICarManager.Inst.aiCarControllers.Contains(carController))
            {
                AICarManager.Inst.aiCarControllers.Add(carController);
            }
        }
        if(collision.gameObject.layer == 8)
        {
            BoosterManager.Inst.PutBoosterBackInPool(collision.gameObject);
        }
    }

}
