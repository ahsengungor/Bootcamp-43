using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    [SerializeField] private List<IInteractable> nearby = new();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            IInteractable closest = GetClosestInteractable();
            closest?.Interact();
        }

        // UI için:
        IInteractable current = GetClosestInteractable();
        if (current != null)
        {
            InteractionUI.Show(current.GetInteractionPrompt());
        }
        else
        {
            InteractionUI.Hide();
        }
    }

    IInteractable GetClosestInteractable()
    {
        IInteractable closest = null;
        float minDist = float.MaxValue;

        foreach (var obj in nearby)
        {
            float dist = Vector3.Distance(transform.position, obj.Transform.position);
            if (dist < interactRange && dist < minDist)
            {
                minDist = dist;
                closest = obj;
            }
        }

        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable i) && !nearby.Contains(i))
        {
            nearby.Add(i);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable i))
        {
            nearby.Remove(i);
        }
    }
}
