using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    private List<IInteractable> nearby = new();

    public IInteractable GetClosestInteractable()
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
            nearby.Add(i);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable i))
            nearby.Remove(i);
    }
}
