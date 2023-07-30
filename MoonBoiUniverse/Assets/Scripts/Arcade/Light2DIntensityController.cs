using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DIntensityController : MonoBehaviour
{
    public Light2D myLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float minWaitTime = 0.1f;
    public float maxWaitTime = 0.3f;

    private void Start()
    {
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // Randomly set light intensity
            myLight.intensity = Random.Range(minIntensity, maxIntensity);

            // Wait for a random amount of time
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }
}
