using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkSceneLoader : MonoBehaviour
{
    bool isCalled = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!isCalled)
            {
                GameManager.Inst.nextScene(4);
                isCalled = true;
            }

        }
    }
}
