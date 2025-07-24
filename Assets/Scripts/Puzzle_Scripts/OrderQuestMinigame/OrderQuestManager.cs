using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OrderQuestManager : MonoBehaviour
{
    public List<GameButtons> iconButtons;
    public List<string> correctSequence = new List<string> { "lamp", "cog", "flash", "pulse", "brain" };

    public Button completeButton; //  Inspector’dan atanacak
    public string nextSceneName = "NextScene"; //  Geçilecek sahne ismini buraya yaz

    private int currentStep = 0;

    void Start()
    {
        foreach (var ib in iconButtons)
        {
            string capturedName = ib.iconName;
            ib.button.onClick.AddListener(() => OnIconButtonClick(capturedName));
        }

        TurnOffAllGlows();

        if (completeButton != null)
        {
            completeButton.gameObject.SetActive(false);
            completeButton.onClick.AddListener(LoadNextScene); // Týklanýnca sahne deðiþtir
        }
    }

    void OnIconButtonClick(string iconName)
    {
        if (iconName == correctSequence[currentStep])
        {
            Debug.Log("Doðru: " + iconName);

            foreach (var ib in iconButtons)
            {
                if (ib.iconName == iconName)
                {
                    ib.TurnOnGlow();
                    break;
                }
            }

            currentStep++;

            if (currentStep >= correctSequence.Count)
            {
                Debug.Log("Tebrikler! Tüm sýrayý doðru girdiniz.");

                if (completeButton != null)
                    completeButton.gameObject.SetActive(true); // Butonu göster
            }
        }
        else
        {
            Debug.Log("Yanlýþ! Baþtan baþla.");
            currentStep = 0;
            TurnOffAllGlows();

            if (completeButton != null)
                completeButton.gameObject.SetActive(false); // Yanlýþsa butonu gizle
        }
    }

    void TurnOffAllGlows()
    {
        foreach (var ib in iconButtons)
        {
            ib.TurnOffGlow();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}


