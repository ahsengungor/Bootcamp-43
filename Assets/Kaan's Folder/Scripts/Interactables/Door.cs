using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string promptMessage = "Kapýyý Aç";
    public Transform Transform => transform;

    public void Interact()
    {
        Debug.Log("Kapý açýldý!");
        // Kapý animasyonu, ses vs. burada
    }

    public string GetInteractionPrompt()
    {
        return promptMessage;
    }
}
