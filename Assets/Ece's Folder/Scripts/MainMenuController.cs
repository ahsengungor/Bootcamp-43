using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    // Play butonuna bas�nca �a�r�lacak fonksiyon
    public void PlayGame()
    {
        // �rne�in "GameScene" adl� sahneyi a�ar
        SceneManager.LoadScene("GameScene");
    }

    // Ayarlar butonuna bas�nca �a�r�lacak fonksiyon
    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    // ��k�� butonuna bas�nca �a�r�lacak fonksiyon
    public void ExitGame()
    {
        Debug.Log("Oyun kapat�l�yor");
        Application.Quit();

        // Unity Editor'de deniyorsan bu sat�r i�e yaramayabilir
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

