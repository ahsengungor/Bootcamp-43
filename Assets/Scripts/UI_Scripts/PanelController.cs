using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject panel;
    public GameObject playButton;
    

    
    public void LoadGameScene()
    {
        SceneManager.LoadScene("xxx");
    }

    // Paneli açar
    public void OpenPanel()
    {
        panel.SetActive(true);
        playButton.SetActive(false);
    }

    // Paneli kapatır
    public void ClosePanel()
    {
        panel.SetActive(false);
        playButton.SetActive(true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
    }
    
    
}


