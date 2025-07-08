using UnityEngine;
using System.Collections.Generic;

public class ObstacleTransparency : MonoBehaviour
{
    public Transform player;
    public LayerMask obstacleLayer;
    public float transparentAlpha = 0.3f;
    public float fadeSpeed = 5f;

    private List<Renderer> transparentObjects = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    void Update()
    {
        // Önceki frame’de þeffaf olan objeleri eski haline getir
        foreach (Renderer r in transparentObjects)
        {
            if (r != null && originalMaterials.ContainsKey(r))
            {
                r.materials = originalMaterials[r];
            }
        }

        transparentObjects.Clear();
        originalMaterials.Clear();

        // Kamera ile oyuncu arasýndaki doðrultu
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(transform.position, player.position);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstacleLayer);

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                if (!originalMaterials.ContainsKey(rend))
                    originalMaterials[rend] = rend.materials;

                // Yeni Material kopyasý
                Material[] newMats = new Material[rend.materials.Length];
                for (int i = 0; i < newMats.Length; i++)
                {
                    newMats[i] = new Material(rend.materials[i]);
                    Color col = newMats[i].color;
                    col.a = transparentAlpha;
                    newMats[i].color = col;
                    newMats[i].SetFloat("_Mode", 2); // Fade mode
                    newMats[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    newMats[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    newMats[i].SetInt("_ZWrite", 0);
                    newMats[i].DisableKeyword("_ALPHATEST_ON");
                    newMats[i].EnableKeyword("_ALPHABLEND_ON");
                    newMats[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    newMats[i].renderQueue = 3000;
                }

                rend.materials = newMats;
                transparentObjects.Add(rend);
            }
        }
    }
}
