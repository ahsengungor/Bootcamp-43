using UnityEngine;

public class StopwatchController : MonoBehaviour
{
    public static StopwatchController Instance;
    public float time;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Eðer baþka bir örnek varsa, bunu yok et
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Sahne deðiþse bile bu nesne yok olmasýn
    }



    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
    }
}
