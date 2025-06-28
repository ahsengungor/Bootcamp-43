using System;
using UnityEngine;
using UnityEngine.AI;

public class MouseBasedMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent _agent;
    public bool canMove = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (!canMove) return;
        CastAgentDestinationRay();
    }

    private void CastAgentDestinationRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
            {
                // Unclickable tagi varsa týklanmýyor.
                if (hit.transform.gameObject.CompareTag("Unclickable")) return;
                _agent.SetDestination(hit.point);
            }
        }
    }
}
