using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MatrixRain : MonoBehaviour
{
    public enum GameMode { easy, hard};
    public GameObject columnPrefab;
    public int columnCount = 15;
    public float spawnInterval = 0.3f;
    public RectTransform spawnArea;
    [SerializeField] private float fallSpeed;

    public GameMode hardness;
    public string[] clues;
    [SerializeField] private TMP_FontAsset clueFont;
    [SerializeField] private float offset;

    void Start()
    {
        StartCoroutine(SpawnColumns());
        HardnessHandler();
    }

    private void HardnessHandler()
    {
        // Zorluk modu ayarlanacak
        hardness = GameMode.easy;
    }

    IEnumerator SpawnColumns()
    {
        for (int i = 0; i < columnCount; i++)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnColumn();
        }
    }

    void SpawnColumn()
    {
        GameObject newCol = Instantiate(columnPrefab, spawnArea);
        RectTransform rect = newCol.GetComponent<RectTransform>();

        // Panelin geniþliði kadar rastgele X pozisyonu
        float x = Random.Range((-spawnArea.rect.width / 2f) + offset, (spawnArea.rect.width / 2f) - offset);
        float startY = spawnArea.rect.height + 50f; // fazla yukarý gitmesin

        rect.anchoredPosition = new Vector2(x, startY);

        StartCoroutine(AnimateColumn(newCol));
    }

    private void Update()
    {
        Debug.Log($"Panel Rect Yukseklik : {spawnArea.rect.height} " +
            $"\nAnchor Pozisyonlarý : {spawnArea.anchoredPosition.y}");
    }

    IEnumerator AnimateColumn(GameObject column)
    {
        RectTransform rect = column.GetComponent<RectTransform>();
        if (hardness == GameMode.easy)
            fallSpeed = Random.Range(75f, 100f);
        else if (hardness == GameMode.hard)
            fallSpeed = Random.Range(160f, 180f);

        TMP_Text[] chars = column.GetComponentsInChildren<TMP_Text>();
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i].text = RandomChar();
        }

        // %15 ihtimalle kelime yerleþtir
        if (Random.value > 0.85f)
        {
            string word = clues[Random.Range(0, clues.Length)];
            int start = Random.Range(0, chars.Length - word.Length);

            for (int i = 0; i < word.Length; i++)
            {
                chars[start + i].text = word[i].ToString();
                chars[start + i].color = Color.green;
                chars[start + i].fontStyle = FontStyles.Bold;
                chars[start + i].font = clueFont;
            }
        }

        float columnHeight = rect.rect.height;
        float bottomLimit = -columnHeight * 40f; 

        while (rect.anchoredPosition.y > bottomLimit)
        {
            rect.anchoredPosition -= new Vector2(0, fallSpeed * Time.deltaTime);

            yield return null;
        }
            Debug.Log(column.gameObject.name + "Spawned at " + rect.anchoredPosition);

        Destroy(column);
    }


    string RandomChar()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return chars[Random.Range(0, chars.Length)].ToString();
    }
}

