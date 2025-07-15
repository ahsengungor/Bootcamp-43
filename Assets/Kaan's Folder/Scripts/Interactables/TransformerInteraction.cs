using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformerInteraction : MonoBehaviour, IInteractable
{
    public Transform Transform => transform;
    private bool CanInteract = false;
    private bool IsSuccessed = false;
    public string promptMessage = "'ye 3 Saniye Basýlý Tut";
    public float holdDuration = 3.0f;
    public float holdTimer = 0.0f;
    public float fillAmount;


    public string GetInteractionPrompt()
    {
        return promptMessage;
    }
    private void Update()
    {
        if (IsSuccessed) InteractionUI.instance.SetProgressBarActive(false);
    }


    public void Interact()
    {
        if (CanInteract && Input.GetKey(KeyCode.F))
        {
            if(!IsSuccessed) MouseBasedMovement.Instance.SetCanMove(false);
            holdTimer += Time.deltaTime;
            InteractionUI.instance.SetProgressValue(holdTimer / holdDuration);

            if (holdTimer > holdDuration && !IsSuccessed)
            {
                IsSuccessed = true;
                InteractSuccess();
            }
        }
        else if (!CanInteract || Input.GetKeyUp(KeyCode.F))
        {
            ResetInteraction();
        }

    }

    private void ResetInteraction()
    {
        holdTimer = 0f;
        InteractionUI.instance.progressBar.fillAmount = 0f;
        InteractionUI.instance.SetProgressBarActive(false);

    }

    private void InteractSuccess()
    {
        if (IsSuccessed)
        {
            Debug.Log("InteractionSuccess");
            MouseBasedMovement.Instance.SetCanMove(true);
            InteractionUI.instance.SetProgressBarActive(!IsSuccessed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CanInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanInteract = false;
            holdTimer = 0f;
            InteractionUI.instance.progressBar.fillAmount = 0f;
            InteractionUI.instance.SetProgressBarActive(false);
        }
    }
}
