//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class MatrixRain : MonoBehaviour
//{
//    public GameObject columnPrefab;
//    public int columnCount = 15;
//    public float spawnInterval = 0.3f;
//    public RectTransform spawnArea;

//    public string[] clues;

//    void Start()
//    {
//        StartCoroutine(SpawnColumns());
//    }

//    IEnumerator SpawnColumns()
//    {
//        for (int i = 0; i < columnCount; i++)
//        {
//            yield return new WaitForSeconds(spawnInterval);
//            SpawnColumn();
//        }
//    }

//    void SpawnColumn()
//    {
//        GameObject newCol = Instantiate(columnPrefab, spawnArea);
//        RectTransform rect = newCol.GetComponent<RectTransform>();

//        // Rastgele X konumu (ekran içinde)
//        float x = Random.Range(0f, spawnArea.rect.width);
//        rect.anchoredPosition = new Vector2(x, spawnArea.rect.height + 200);

//        StartCoroutine(AnimateColumn(newCol));
//    }

//    IEnumerator AnimateColumn(GameObject column)
//    {
//        RectTransform rect = column.GetComponent<RectTransform>();
//        float fallSpeed = Random.Range(20f, 60f);

//        TMP_Text[] chars = column.GetComponentsInChildren<TMP_Text>();
//        for (int i = 0; i < chars.Length; i++)
//        {
//            chars[i].text = RandomChar();
//            chars[i].color = Color.green;
//        }

//        // %15 ihtimalle kelime yerleþtir
//        if (Random.value > 0.85f)
//        {
//            string word = clues[Random.Range(0, clues.Length)];
//            int start = Random.Range(0, chars.Length - word.Length);

//            for (int i = 0; i < word.Length; i++)
//            {
//                chars[start + i].text = word[i].ToString();
//                chars[start + i].color = new Color(0, 1, 0, 1);
//                chars[start + i].fontStyle = FontStyles.Bold;
//            }
//        }

//        while (rect.anchoredPosition.y > -200)
//        {
//            rect.anchoredPosition -= new Vector2(0, fallSpeed * Time.deltaTime);
//            yield return null;
//        }

//        Destroy(column);
//    }

//    string RandomChar()
//    {
//        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//        return chars[Random.Range(0, chars.Length)].ToString();
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MatrixRain : MonoBehaviour
{
    public GameObject columnPrefab;
    public int columnCount = 15;
    public float spawnInterval = 0.3f;
    public RectTransform spawnArea;

    public string[] clues;
    [SerializeField] private TMP_FontAsset clueFont;
    [SerializeField] private float offset;

    void Start()
    {
        StartCoroutine(SpawnColumns());
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
        float fallSpeed = Random.Range(20f, 60f);

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

        float bottomLimit = -200f; // daha aþaðý kadar inmesini istiyorsan bunu büyüt

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

