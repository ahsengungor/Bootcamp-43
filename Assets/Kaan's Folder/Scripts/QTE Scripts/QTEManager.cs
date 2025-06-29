using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;
    public GameObject qtePrefab; // tek prefab
    public RectTransform spawnArea; // UI alaný

    public enum GameMode { easy, hard }
    public GameMode hardness;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        hardness = GameMode.hard;
    }

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
