using UnityEngine;

public class Temp_NPC : MonoBehaviour, IInteractable
{
    public Transform Transform => transform;

    public string promptMessage = "Test123123";

    public string GetInteractionPrompt()
    {
        return promptMessage;
    }

    public void Interact()
    {
        Debug.Log("NPC Interact");
    }
}
