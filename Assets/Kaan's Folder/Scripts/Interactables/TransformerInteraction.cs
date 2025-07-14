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
        if (Input.GetKey(KeyCode.F))
        {
            holdTimer += Time.deltaTime;
            InteractionUI.instance.SetProgressValue(holdTimer / holdDuration);

            if (holdTimer > holdDuration && !IsSuccessed)
            {
                IsSuccessed = true;
                InteractSuccess();
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            // Parmak kaldýrýlýrsa sýfýrla
            holdTimer = 0f;
            InteractionUI.instance.progressBar.fillAmount = 0f;
        }
    }

    private void InteractSuccess()
    {
        if (IsSuccessed)
        {
            Debug.Log("InteractionSuccess");
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
        }
    }
}
