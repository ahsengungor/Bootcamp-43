using UnityEngine;
using System.Collections.Generic;

public class LightManager : MonoBehaviour
{
    [Header("Spotlight Objeleri")]
    public List<GameObject> spotlights;

    [Header("MaterialChanger Referansý")]
    public MaterialChanger materialChanger;

    [Header("Test Modu")]
    public bool testMode = false;

    private bool lastToggleState = false;

    void Start()
    {
        lastToggleState = testMode;
    }

    void Update()
    {
        if (testMode != lastToggleState)
        {
            if (testMode)
            {
                DisableSpotlightsAndChangeMaterials();
            }
            else
            {
                EnableSpotlightsAndRestoreMaterials();
            }
            lastToggleState = testMode;
        }
    }

    public void DisableSpotlightsAndChangeMaterials()
    {
        foreach (GameObject spotlight in spotlights)
        {
            if (spotlight != null)
                spotlight.SetActive(!spotlight.activeSelf);  // aktifliði tersine çevir
        }

        if (materialChanger != null)
        {
            materialChanger.ChangeMaterials();
        }
        else
        {
            Debug.LogWarning("MaterialChanger referansý atanmadý!");
        }
    }

    public void EnableSpotlightsAndRestoreMaterials()
    {
        foreach (GameObject spotlight in spotlights)
        {
            if (spotlight != null)
                spotlight.SetActive(!spotlight.activeSelf);  // aktifliði tersine çevir
        }

        if (materialChanger != null)
        {
            materialChanger.RestoreOriginalMaterials();
        }
        else
        {
            Debug.LogWarning("MaterialChanger referansý atanmadý!");
        }
    }

}