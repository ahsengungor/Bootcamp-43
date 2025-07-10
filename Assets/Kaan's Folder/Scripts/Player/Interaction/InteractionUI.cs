using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI instance;

    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI promptText;

    private void Awake()
    {
        instance = this;

        // Null kontrolü ekle
        if (root != null)
            root.SetActive(false);
        else
            Debug.LogWarning("InteractionUI.root atanmadý!");

        if (promptText == null)
            Debug.LogWarning("InteractionUI.promptText atanmadý!");
    }

    public static void Show(string prompt)
    {
        if (instance == null || instance.promptText == null || instance.root == null)
        {
            return;
        }

        instance.promptText.text = $"[F] {prompt}"; // Interaction key : F
        instance.root.SetActive(true);
    }

    public static void Hide()
    {
        if (instance == null || instance.root == null)
        {
            return;
        }

        instance.root.SetActive(false);
    }
}
