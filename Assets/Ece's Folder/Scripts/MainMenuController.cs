using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    // Play butonuna basýnca çaðrýlacak fonksiyon
    public void PlayGame()
    {
        // Örneðin "GameScene" adlý sahneyi açar
        SceneManager.LoadScene("GameScene");
    }

    // Ayarlar butonuna basýnca çaðrýlacak fonksiyon
    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    // Çýkýþ butonuna basýnca çaðrýlacak fonksiyon
    public void ExitGame()
    {
        Debug.Log("Oyun kapatýlýyor");
        Application.Quit();

        // Unity Editor'de deniyorsan bu satýr iþe yaramayabilir
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

