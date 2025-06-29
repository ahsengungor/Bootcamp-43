using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;
    public GameObject qtePrefab; // tek prefab
    public RectTransform spawnArea; // UI alan�

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
