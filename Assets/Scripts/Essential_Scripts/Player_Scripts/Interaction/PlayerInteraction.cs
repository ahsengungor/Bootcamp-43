using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private InteractionHandler handler;
    private IHoldableInteractable currentHoldable;

    private void Start()
    {
        handler = GetComponent<InteractionHandler>();
    }

    void Update()
    {
        IInteractable current = handler.GetClosestInteractable();

        if (current != null)
        {
            InteractionUI.Show(current.GetInteractionPrompt());
        }
        else
        {
            InteractionUI.Hide();
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (current is IHoldableInteractable holdable)
            {
                currentHoldable = holdable;
                holdable.HoldUpdate();
            }
            else
            {
                current?.Interact();
            }
        }

        if (Input.GetKeyUp(KeyCode.F) && currentHoldable != null)
        {
            currentHoldable.HoldExit();
            currentHoldable = null;
        }
    }
}
