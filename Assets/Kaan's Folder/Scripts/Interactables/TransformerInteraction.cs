using UnityEngine;

public class TransformerInteraction : MonoBehaviour, IHoldableInteractable
{
    public Transform Transform => transform;

    private float holdTimer;
    private bool isDone = false;
    private bool inRange = false;

    public float holdDuration = 3f;
    public string promptMessage = "3 saniye basýlý tut";

    public string GetInteractionPrompt() => promptMessage;

    public void Interact() { } // kullanýlmaz

    public void HoldUpdate()
    {
        if (isDone || !inRange) return;

        holdTimer += Time.deltaTime;
        InteractionUI.instance.SetProgressBarActive(true);
        InteractionUI.instance.SetProgressValue(holdTimer / holdDuration);

        if (holdTimer >= holdDuration)
        {
            isDone = true;
            InteractionUI.instance.SetProgressBarActive(false);
            Debug.Log("Þalter baþarýyla çalýþtý!");
        }
    }

    public void HoldExit()
    {
        if (isDone) return;

        holdTimer = 0f;
        InteractionUI.instance.SetProgressValue(0f);
        InteractionUI.instance.SetProgressBarActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            HoldExit();
        }
    }
}
