using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationStep = 45f;        // Her Q/E bas���nda hedef a��
    public float rotationSmoothSpeed = 5f;  // Yumu�akl�k derecesi (daha y�ksek = daha h�zl� d�ner)

    private float currentYaw = 45f;         // Hedef a��
    private float actualYaw = 45f;          // Ger�ek d�n�� a��s�

    void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplySmoothRotation();
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
    }

    void ApplySmoothRotation()
    {
        // Smooth d�n�� (actualYaw zamanla currentYaw'e yakla��r)
        actualYaw = Mathf.LerpAngle(actualYaw, currentYaw, rotationSmoothSpeed * Time.deltaTime);

        // �zometrik a�� sabit, sadece Y d�n�yor
        transform.rotation = Quaternion.Euler(30f, actualYaw, 0f);
    }

}

