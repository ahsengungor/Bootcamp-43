using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public GameObject qtePrefab; // tek prefab
    public RectTransform spawnArea; // UI alaný

    public void SpawnQTE()
    {
        GameObject qte = Instantiate(qtePrefab, spawnArea);
        QTEButton qteScript = qte.GetComponent<QTEButton>();
        qteScript.StartQTE(); // özel hareket baþlatma
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnQTE();
        }
    }
}
