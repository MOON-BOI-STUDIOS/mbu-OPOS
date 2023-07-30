using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arcadeCollisionDetection : MonoBehaviour
{
    public Animator _am;
    public bool isbroken=true;
    public ArcadeType _AT;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ArcadeMacineManager.CurrentAnimator = _am;
            ArcadeMacineManager.currentACD = this;

            if (ArcadeMacineManager.isUIopen)
                return;

            if (isbroken)
            {
                ArcadeMacineManager.Inst.openRepairUI();
                return;
            }
            ArcadeMacineManager.Inst.openPlayButton(_AT);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
           
            ArcadeMacineManager.CurrentAnimator = null;
            ArcadeMacineManager.currentACD = null;
            ArcadeMacineManager.Inst.CloseRepairUI();

            if (!isbroken)
            {
                ArcadeMacineManager.Inst.ClosePlayButton();
                return;
            }
        }
    }
}
