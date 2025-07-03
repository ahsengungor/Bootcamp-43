using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    public string promptMessage = "Sphere Interacted";
    private AudioSource _audioSource;

    public Transform Transform => transform;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public string GetInteractionPrompt()
    {
        return promptMessage;
    }

    public void Interact()
    {
        Debug.Log("Test Dogru!");
        _audioSource.Play();
    }
}
