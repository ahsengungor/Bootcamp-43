using UnityEngine;
using TMPro;
using System.Collections;
using System; // Enum.Parse için gerekli

public class QTEButton : MonoBehaviour
{
    public float enterSpeed = 800f;
    public float exitSpeed = 1000f;
    public float waitTime = 1.5f;

    private RectTransform rect;
    private Vector2 startPos;
    private Vector2 centerPos;
    private Vector2 endPos;

    public TextMeshProUGUI keyText;

    private string keyString; // Değişken adını daha anlaşılır yaptım
    private KeyCode keyCode;  // Kontrol için KeyCode tutacağız

    private bool qteFinished = false; // QTE'nin bittiğini (başarılı/başarısız) belirtmek için

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        keyText = GetComponentInChildren<TextMeshProUGUI>();

        // Harf atanması
        char randomChar = (char)UnityEngine.Random.Range(65, 91); // A-Z arası
        keyString = randomChar.ToString();
        keyText.text = keyString;

        // --- YENİ: String'i KeyCode'a çevirme ---
        // "A" string'ini KeyCode.A enum'una dönüştürüyoruz.
        try
        {
            keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), keyString);
        }
        catch (ArgumentException)
        {
            Debug.LogError("Geçersiz bir harf KeyCode'a çevrilemedi: " + keyString);
            Destroy(gameObject); // Hatalı QTE'yi yok et
        }
    }

    public void StartQTE()
    {
        StartCoroutine(QTEFlow());
        Debug.Log("Beklenen Tuş: " + keyString);
    }

    IEnumerator QTEFlow()
    {
        // Pozisyonları hesapla (Canvas boyutuna göre)
        float width = ((RectTransform)transform.parent).rect.width;
        startPos = new Vector2(-width / 2 - rect.rect.width, 0f); // Sol dışı
        centerPos = Vector2.zero;
        endPos = new Vector2(width / 2 + rect.rect.width, 0f); // Sağ dışı

        rect.anchoredPosition = startPos;

        // --- GİRİŞ ANİMASYONU ---
        while (Vector2.Distance(rect.anchoredPosition, centerPos) > 5f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, centerPos, enterSpeed * Time.deltaTime);
            yield return null;
        }
        rect.anchoredPosition = centerPos; // Tam merkeze oturt

        // --- YENİ ve DÜZELTİLMİŞ GİRDİ BEKLEME DÖNGÜSÜ ---
        float timer = 0f;
        while (timer < waitTime && !qteFinished)
        {
            // Eğer doğru tuşa basılırsa...
            if (Input.GetKeyDown(keyCode))
            {
                Success();
                qteFinished = true; // QTE'yi bitir
            }

            // Eğer doğru tuş dışında herhangi bir tuşa basılırsa başarısız saymak istersen (isteğe bağlı):
            // else if(Input.anyKeyDown)
            // {
            //     Failure();
            //     qteFinished = true;
            // }

            timer += Time.deltaTime;
            yield return null; // Bir sonraki frame'e geç
        }

        // Eğer süre dolduysa ve hala tuşa basılmadıysa (qteFinished hala false ise)
        if (!qteFinished)
        {
            Failure();
        }

        // --- ÇIKIŞ ANİMASYONU ---
        while (Vector2.Distance(rect.anchoredPosition, endPos) > 5f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, endPos, exitSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void Success()
    {
        Debug.Log("SUCCESS!");
        // Burada görsel/sesli bir efekt ekleyebilirsin. Örneğin rengini yeşil yap:
        // keyText.color = Color.green;
    }

    private void Failure()
    {
        Debug.Log("FAILURE!");
        // Burada görsel/sesli bir efekt ekleyebilirsin. Örneğin rengini kırmızı yap:
        // keyText.color = Color.red;
    }
}