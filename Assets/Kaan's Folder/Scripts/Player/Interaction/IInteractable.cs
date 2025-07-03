using UnityEngine;

public interface IInteractable
{
    void Interact();
    string GetInteractionPrompt(); // UI i�in
    Transform Transform { get; }   // Pozisyon eri�imi i�in
}
