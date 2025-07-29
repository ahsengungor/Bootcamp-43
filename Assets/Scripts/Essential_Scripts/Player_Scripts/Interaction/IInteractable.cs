using UnityEngine;

public interface IInteractable
{
    string GetInteractionPrompt();
    void Interact();
    Transform Transform { get; }
}
