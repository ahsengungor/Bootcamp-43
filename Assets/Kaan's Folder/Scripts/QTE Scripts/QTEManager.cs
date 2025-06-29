using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public GameObject qtePrefab; // tek prefab
    public RectTransform spawnArea; // UI alan�

    public void SpawnQTE()
    {
        GameObject qte = Instantiate(qtePrefab, spawnArea);
        QTEButton qteScript = qte.GetComponent<QTEButton>();
        qteScript.StartQTE(); // �zel hareket ba�latma
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnQTE();
        }
    }
}
