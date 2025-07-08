using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [Header("Camera Movement Variables")]
    public float moveSpeed = 5f;
    public float rotationStep = 45f;        // Her Q/E bas���nda hedef a��
    public float rotationSmoothSpeed = 5f;  // Yumu�akl�k derecesi (daha y�ksek = daha h�zl� d�ner)

    [Header("Camera Zoom Variables")]
    public float zoomSpeed = 5f;
    public float maxZoom = 50f;
    public float minZoom = 1f;

    [Header("Camera Rotation Variables")]
    private float currentYaw = 45f;         // Hedef a��
    private float actualYaw = 45f;          // Ger�ek d�n�� a��s�

    private float minYaw = -45f;
    private float maxYaw = 135f;

    [SerializeField] private CinemachineCamera virtualCam;


    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
        ApplySmoothRotation();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f && virtualCam != null)
        {
            var lens = virtualCam.Lens;
            
            lens.OrthographicSize -= scroll * zoomSpeed;
            lens.OrthographicSize = Mathf.Clamp(lens.OrthographicSize, minZoom, maxZoom);
            virtualCam.Lens = lens; // lens ayarlar�n� geri set etmen gerekir!
            if (lens.OrthographicSize <= minZoom)
            {
                lens.OrthographicSize = minZoom;
                return;
            }
;
        }
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        Quaternion yawRotation = Quaternion.Euler(0, actualYaw, 0); // NOT: actualYaw kullan
        Vector3 moveDir = yawRotation * inputDir;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentYaw -= rotationStep;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentYaw += rotationStep;
        }
        currentYaw = Mathf.Clamp(currentYaw, minYaw, maxYaw);
    }

    void ApplySmoothRotation()
    {
        // Smooth d�n�� (actualYaw zamanla currentYaw'e yakla��r)
        actualYaw = Mathf.LerpAngle(actualYaw, currentYaw, rotationSmoothSpeed * Time.deltaTime);

        // �zometrik a�� sabit, sadece Y d�n�yor
        transform.rotation = Quaternion.Euler(30f, actualYaw, 0f);
    }

}

