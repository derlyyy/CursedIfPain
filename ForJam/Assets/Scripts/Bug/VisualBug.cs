using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VisualBug : MonoBehaviour
{
    public Material defaultMaterial;
    public Renderer renderer;

    [Header("Glitch")] 
    public ScriptableRendererFeature fullscreenGlitch;
    public Material glitchMaterial;

    [Header("Noise Amount Settings")]
    public float minNoiseAmount = 0.1f;
    public float maxNoiseAmount = 0.5f;

    [Header("Glitch Strength Settings")]
    public float minGlitchStrength = 0.1f;
    public float maxGlitchStrength = 0.5f;

    [Header("Scan Lines Strength Settings")]
    public float minScanLinesStrength = 0.1f;
    public float maxScanLinesStrength = 0.5f;

    private void Start()
    {
        fullscreenGlitch.SetActive(false);
    }

    private void Update()
    {
        // You can trigger ActivateBug() method based on some condition or input here if needed.
    }

    public void ActivateBug()
    {
        FullscreenGlitch();
    }

    private void DeactivateBug()
    {
        fullscreenGlitch.SetActive(false);
    }

    private void FullscreenGlitch()
    {
        fullscreenGlitch.SetActive(true);

        // Generate random values within specified ranges
        float noiseAmount = Random.Range(minNoiseAmount, maxNoiseAmount);
        float glitchStrength = Random.Range(minGlitchStrength, maxGlitchStrength);
        float scanLinesStrength = Random.Range(minScanLinesStrength, maxScanLinesStrength);

        // Set the random values to the glitch material
        glitchMaterial.SetFloat("_NoiseAmount", noiseAmount);
        glitchMaterial.SetFloat("_GlitchStrength", glitchStrength);
        glitchMaterial.SetFloat("_ScanLinesStrength", scanLinesStrength);

        Invoke("DeactivateBug", 2f);
    }
}