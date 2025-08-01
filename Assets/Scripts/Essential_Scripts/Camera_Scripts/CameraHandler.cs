using UnityEngine;
using Unity.Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public CinemachineCamera cineCam;
    public float rotationSpeed = 120f;
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 8f;

    private CinemachineThirdPersonFollow follow;
    private float currentAngle = 0f;
    private float currentZoom;

    void Start()
    {
        follow = cineCam.GetComponent<CinemachineThirdPersonFollow>();
        currentZoom = follow.CameraDistance;
    }

    void Update()
    {
        // ✅ Sağ tuş basılıyken kamera yatay döner
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            currentAngle += mouseX * rotationSpeed * Time.deltaTime;

            // FollowOffset değerini Y ekseninde döndür
            Quaternion rot = Quaternion.Euler(0, currentAngle, 0);
            Vector3 offset = rot * new Vector3(0, follow.ShoulderOffset.y, -currentZoom);
            follow.CameraSide = 0; // tam ortada tut
            follow.CameraDistance = currentZoom;
            follow.ShoulderOffset = new Vector3(offset.x, follow.ShoulderOffset.y, offset.z);
        }

        // ✅ Zoom (Mouse Wheel)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentZoom -= scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            follow.CameraDistance = currentZoom;
        }
    }
}
