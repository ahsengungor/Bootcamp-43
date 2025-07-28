using System.Collections;
using UnityEngine;

public class HomeLampLamp : MonoBehaviour
{
    public Light lightSource;

    void Start()
    {
        lightSource = GetComponentInChildren<Light>();
        StartCoroutine(Selektor());
    }

    IEnumerator Selektor()
    {
        while (true)
        {
            // Kapat
            lightSource.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));

            // Aç
            lightSource.enabled = true;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
