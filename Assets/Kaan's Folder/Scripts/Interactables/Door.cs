using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string promptMessage = "Ses Cal";
    private AudioSource _audioSource;
    public Transform Transform => transform;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        Debug.Log("Kapý açýldý!");
        _audioSource.Play();
    }

    public string GetInteractionPrompt()
    {
        return promptMessage;
    }
}
