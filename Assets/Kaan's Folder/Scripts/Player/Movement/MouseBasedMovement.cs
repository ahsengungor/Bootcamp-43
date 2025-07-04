using System;
using UnityEngine;
using UnityEngine.AI;

public class MouseBasedMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent _agent;
    public bool canMove = true;

    public GameObject clickVfxPrefab;

    [Tooltip("Raycast'in çarpabileceği katmanları seçin. 'InvisibleWall' katmanını seçmeyin.")]
    public LayerMask clickableLayers;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();  
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
                // Unclickable tagi varsa tıklanmıyor.
                if (hit.transform.gameObject.CompareTag("Ground"))
                     Instantiate(clickVfxPrefab, hit.point, Quaternion.identity);
                _agent.SetDestination(hit.point);

                Vector3 hitPoint = new Vector3(hit.point.x, 0.5f, hit.point.z);
                Instantiate(clickVfxPrefab, hitPoint, Quaternion.identity);

            }
        }
    }
}
