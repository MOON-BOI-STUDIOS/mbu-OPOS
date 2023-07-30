using UnityEngine;

public class raceAnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    public static raceAnimationManager Inst;
    private void Awake()
    {
        Inst = this;
    }

    public void PlayBlinking()
    {
        if (animator != null)
        {
            animator.SetTrigger("isBlink");
        }
    }

    public void PlayBoost()
    {
        animator.SetBool("isBoost", true);
        Invoke("TriggerAnimation", 0.1f);
    }

    
    public void StopBoost()
    {
        animator.SetBool("isBoost", false);
    }

    public void TriggerAnimation()
    {
        int value = PlayerPrefs.GetInt("CurrentPlayBikeIndex", 0);
        // Set the respective animation trigger based on the input value
        switch (value)
        {
            case 0:
                animator.SetTrigger("isone");
                break;
            case 1:
                animator.SetTrigger("istwo");
                break;
            case 2:
                animator.SetTrigger("isthree");
                break;
            case 3:
                animator.SetTrigger("isfour");
                break;
            case 4:
                animator.SetTrigger("isfive");
                break;
            default:
                Debug.LogWarning("Invalid value! Please enter a value between 1 and 5.");
                break;
        }

    }
}
