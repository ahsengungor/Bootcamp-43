using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    public string promptMessage = "Konuþmayý baþlat";
    private AudioSource _audioSource;
    [SerializeField] private DialogueData dialogue;

    public Transform Transform => transform;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public string GetInteractionPrompt() => promptMessage;

    public void Interact()
    {
        _audioSource.Play();
        DialogueController.Instance.LoadDialogue(dialogue);
    }
}
