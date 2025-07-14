using UnityEngine;

public class NeuralAreaIndicator : MonoBehaviour
{
    public SpriteRenderer auraRenderer;  // Circle sprite
    public SphereCollider neuralArea;          // Trigger alan
    public string targetTag = "TargetNPC";

    public float fadeSpeed = 1f;
    private bool shouldFadeIn = false;
    private float currentAlpha = 0f;

    void Start()
    {
        SetAlpha(0f); // baþlangýçta görünmez
    }

    void Update()
    {
        if (shouldFadeIn && currentAlpha < 1f)
        {
            currentAlpha += Time.deltaTime * fadeSpeed;
            SetAlpha(currentAlpha);
        }

        transform.Rotate(0f, 0f, 50f * Time.deltaTime);
    }

    void SetAlpha(float alpha)
    {
        Color c = auraRenderer.color;
        c.a = Mathf.Clamp01(alpha);
        auraRenderer.color = c;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            shouldFadeIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            shouldFadeIn = false;
            currentAlpha = 0f;
            SetAlpha(0f);
        }
    }
}
