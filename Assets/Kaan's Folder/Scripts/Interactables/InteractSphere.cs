using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    public string promptMessage = "Sphere Interacted";

    public Transform Transform => transform;

    public string GetInteractionPrompt()
    {
        return promptMessage;
    }

    public void Interact()
    {
        Debug.Log("Test Dogru!");
    }
}
