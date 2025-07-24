using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI instance;

    [SerializeField] private GameObject interaction_root;
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private GameObject progress_root;
    [SerializeField] public Image progressBar;

    private void Awake()
    {
        instance = this;

        // Null kontrolü ekle
        if (interaction_root != null)
            interaction_root.SetActive(false);
        else
            Debug.LogWarning("InteractionUI.root atanmadý!");

        if (promptText == null)
            Debug.LogWarning("InteractionUI.promptText atanmadý!");
    }

    public static void Show(string prompt)
    {
        if (instance == null || instance.promptText == null || instance.interaction_root == null)
        {
            return;
        }

        instance.promptText.text = $"[F] {prompt}"; // Interaction key : F
        instance.interaction_root.SetActive(true);
    }

    public static void Hide()
    {
        if (instance == null || instance.interaction_root == null)
        {
            return;
        }

        instance.interaction_root.SetActive(false);
    }

    public void SetProgressValue(float value)
    {
        SetProgressBarActive(true);
        progressBar.fillAmount = value;
    }

    public void SetProgressBarActive(bool active)
    {
        progress_root.SetActive(active);
    }
}
