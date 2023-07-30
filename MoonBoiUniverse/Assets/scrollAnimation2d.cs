using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class scrollAnimation2d : MonoBehaviour
{
    [SerializeField] private Transform targetTransform; // Reference to the Transform component
    [SerializeField] private Image targetImage; // Reference to the Image component
    [SerializeField] private GameObject scrollUI;
    // Start is called before the first frame update


    private void Awake()
    {
        if (PlayerPrefs.GetInt("firstLoad", 0)==0)
        {
            PlayerPrefs.SetInt("firstLoad", 1);
            Invoke("AnimateTransformAndImage", 1f);
        }
        else
        {
            scrollUI.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        Invoke("AnimateTransformAndImage", 1f);
    }

    private void AnimateTransformAndImage()
    {
        // Animate the Y position of the Transform from 0 to 500
        targetTransform.DOLocalMoveY(1500, 30f) // 1 second duration
            .OnComplete(() => // Once the transform animation is complete
            {
                // Animate the alpha of the Image to zero
                targetImage.DOFade(0, 1f) // 1 second duration
                    .OnComplete(() => // Once the image fade is complete
                    {
                        // Set the GameObject active to false
                        scrollUI.gameObject.SetActive(false);
                    });
            });
    }
}