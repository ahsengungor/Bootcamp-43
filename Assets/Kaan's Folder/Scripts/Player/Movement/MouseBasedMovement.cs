using System;
using UnityEngine;
using UnityEngine.AI;

public class MouseBasedMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent _agent;
    public bool canMove = true;

    public GameObject clickVfxPrefab;

    [Tooltip("Raycast'in çarpabileceði katmanlarý seçin. 'InvisibleWall' katmanýný seçmeyin.")]
    public LayerMask clickableLayers;

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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayers))
            {
                // Unclickable tagi varsa týklanmýyor.
                if (!hit.transform.gameObject.CompareTag("Ground")) return;
                _agent.SetDestination(hit.point);
                Vector3 hitPoint = new Vector3(hit.point.x, 0.5f, hit.point.z);
                Instantiate(clickVfxPrefab, hitPoint, Quaternion.identity);

            }
        }
    }
}
