using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public float targetAlpha = 1f;
    public float fadeSpeed = 2f;

    private Material material;
    private float currentAlpha;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        currentAlpha = material.color.a;
    }

    void Update()
    {
        Color col = material.color;
        col.a = Mathf.Lerp(col.a, targetAlpha, Time.deltaTime * fadeSpeed);
        material.color = col;
    }

    public void FadeTo(float alpha)
    {
        targetAlpha = alpha;
    }

    public void Restore()
    {
        targetAlpha = 1f;
    }
}
