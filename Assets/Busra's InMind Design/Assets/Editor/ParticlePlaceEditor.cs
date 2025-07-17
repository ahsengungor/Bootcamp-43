using UnityEngine;
using UnityEditor;

public class ParticlePlacerEditor : EditorWindow
{
    GameObject particlePrefab;
    GameObject planeObject;
    Vector2 gridSize = new Vector2(10f, 3.5f); // X = 10m, Z = 3.5m
    float heightY = -0.6f;

    [MenuItem("Tools/Particle Placer")]
    public static void ShowWindow()
    {
        GetWindow<ParticlePlacerEditor>("Particle Placer");
    }

    void OnGUI()
    {
        GUILayout.Label("Particle Prefab Placer", EditorStyles.boldLabel);

        particlePrefab = (GameObject)EditorGUILayout.ObjectField("Particle Prefab", particlePrefab, typeof(GameObject), false);
        planeObject = (GameObject)EditorGUILayout.ObjectField("Plane Object", planeObject, typeof(GameObject), true);
        
        EditorGUILayout.LabelField("Grid Spacing:");
        gridSize.x = EditorGUILayout.FloatField("X Spacing (m)", gridSize.x);
        gridSize.y = EditorGUILayout.FloatField("Z Spacing (m)", gridSize.y);

        heightY = EditorGUILayout.FloatField("Particle Y Height", heightY);

        if (GUILayout.Button("Place Particles"))
        {
            if (particlePrefab == null || planeObject == null)
            {
                Debug.LogError("Particle prefab ve plane objesi atanmalı.");
                return;
            }

            PlaceParticles();
        }
    }

    void PlaceParticles()
    {
        Renderer planeRenderer = planeObject.GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            Debug.LogError("Plane objesinde Renderer yok.");
            return;
        }

        Bounds bounds = planeRenderer.bounds;

        float startX = bounds.min.x;
        float endX = bounds.max.x;
        float startZ = bounds.min.z;
        float endZ = bounds.max.z;

        GameObject parent = new GameObject("PlacedParticles");

        for (float x = startX; x < endX; x += gridSize.x)
        {
            for (float z = startZ; z < endZ; z += gridSize.y)
            {
                Vector3 position = new Vector3(x, heightY, z);
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(particlePrefab);
                instance.transform.position = position;
                instance.transform.SetParent(parent.transform);
            }
        }

        Debug.Log("Particle efektleri yerleştirildi.");
    }
}
