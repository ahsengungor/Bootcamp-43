using UnityEngine;

public interface IInteractable
{
    void Interact();
    string GetInteractionPrompt(); // UI için
    Transform Transform { get; }   // Pozisyon eriþimi için
}
