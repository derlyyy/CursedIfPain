using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public Light2D light2D;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    private float targetIntensity;
    private float currentIntensity;
    private float t;

    void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }
        currentIntensity = light2D.intensity;
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        t += Time.deltaTime * flickerSpeed;
        light2D.intensity = Mathf.Lerp(currentIntensity, targetIntensity, t);

        if (t >= 1)
        {
            currentIntensity = targetIntensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            t = 0;
        }
    }
}
