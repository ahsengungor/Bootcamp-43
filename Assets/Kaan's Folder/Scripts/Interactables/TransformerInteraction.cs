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
    public string GetInteractionPrompt()
    {
        return promptMessage;
    }

    public void Interact()
    {
        if (Input.GetKey(KeyCode.F))
        {
            holdTimer += Time.deltaTime;
            //progressBar.fillAmount = holdTimer / holdDuration;

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
            //progressBar.fillAmount = 0f;
        }
    }

    private void InteractSuccess()
    {
        if (IsSuccessed)
        {
            Debug.Log("InteractionSuccess");
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
            //progressBar.fillAmount = 0f;
        }
    }
}
