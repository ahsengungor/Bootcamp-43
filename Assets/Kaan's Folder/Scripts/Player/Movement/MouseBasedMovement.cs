using System;
using UnityEngine;
using UnityEngine.AI;

public class MouseBasedMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent _agent;
    public bool canMove = true;

    public float rotationSpeed = 5f;

    public GameObject clickVfxPrefab;

    [Tooltip("Raycast'in çarpabileceði katmanlarý seçin. 'InvisibleWall' katmanýný seçmeyin.")]
    public LayerMask clickableLayers;
    private Vector3? lookTarget = null;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();  
    }

    void Update()
    {
        if (!canMove) return;
        CastAgentDestinationRay();

        if (lookTarget.HasValue)
        {
            SmoothLookAt(lookTarget.Value);
        }
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
                if (hit.transform.gameObject.CompareTag("Ground"))
                     Instantiate(clickVfxPrefab, hit.point, Quaternion.identity);
                _agent.SetDestination(hit.point);

                Vector3 hitPoint = new Vector3(hit.point.x, 0.5f, hit.point.z);
                Instantiate(clickVfxPrefab, hitPoint, Quaternion.identity);

                // Sadece yatay düzlemde hedef belirleyelim
                Vector3 flatTarget = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                lookTarget = flatTarget;
            }
        }
    }

    private void SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction.magnitude < 0.01f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

}
