using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string promptMessage = "Kap�y� A�";
    public Transform Transform => transform;

    public void Interact()
    {
        Debug.Log("Kap� a��ld�!");
        // Kap� animasyonu, ses vs. burada
    }

    public string GetInteractionPrompt()
    {
        return promptMessage;
    }
}
