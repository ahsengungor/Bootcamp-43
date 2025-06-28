using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationStep = 45f;        // Her Q/E basýþýnda hedef açý
    public float rotationSmoothSpeed = 5f;  // Yumuþaklýk derecesi (daha yüksek = daha hýzlý döner)

    private float currentYaw = 45f;         // Hedef açý
    private float actualYaw = 45f;          // Gerçek dönüþ açýsý

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
        // Smooth dönüþ (actualYaw zamanla currentYaw'e yaklaþýr)
        actualYaw = Mathf.LerpAngle(actualYaw, currentYaw, rotationSmoothSpeed * Time.deltaTime);

        // Ýzometrik açý sabit, sadece Y dönüyor
        transform.rotation = Quaternion.Euler(30f, actualYaw, 0f);
    }

}

