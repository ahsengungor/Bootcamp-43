using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public float targetAlpha = 1f;
    public float fadeSpeed = 2f;

    private Material material;
    private float currentAlpha;
    private bool isTransparent = false;

    private int originalRenderQueue;
    private Material originalMaterial;

    void Start()
    {
        // Instancing deðil, direkt clone yerine sharedMaterial kullanmak istersek dikkatli olmak lazým
        material = GetComponent<Renderer>().material;
        originalMaterial = new Material(material);
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
        if (!isTransparent)
        {
            SetMaterialToTransparent();
        }
        targetAlpha = alpha;
    }

    public void Restore()
    {
        targetAlpha = 1f;

        // Tamamen opak olduðunda geri eski haline döndür
        if (Mathf.Abs(material.color.a - 1f) < 0.01f)
        {
            RestoreMaterial();
        }
    }

    void SetMaterialToTransparent()
    {
        // Unity Standard Shader için çalýþýr (URP/HDRP için ShaderGraph veya Lit ayarý deðiþir)
        material.SetFloat("_Mode", 2); // Fade
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        isTransparent = true;
    }

    void RestoreMaterial()
    {
        // Orijinal ayarlarý geri yükle
        material.CopyPropertiesFromMaterial(originalMaterial);
        isTransparent = false;
    }
}
