using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

   

    public GameObject LoadingScreenPrefab;
    public Image LoadingBarFill;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    } // Singelton


    public void LoadScene(int sceneId) => StartCoroutine(LoadSceneAsync(sceneId));

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        LoadingScreenPrefab.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}
