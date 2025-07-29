using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterMotionDirectionController : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;
    private float speed;

    [Header("Hýz güncelleme ayarlarý")]
    public float smoothing = 50f;

    [Header("Yönlendirme ayarlarý")]
    public bool rotateTowardsMovement = true;
    public float rotationSpeed = 10f; // Dönme hýzý

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 delta = transform.position - lastPosition;
        float currentSpeed = delta.magnitude / Time.deltaTime;

        // Hýzý yumuþat
        speed = Mathf.Lerp(speed, currentSpeed, Time.deltaTime * smoothing);

        // Animator'a aktar
        animator.SetFloat("speed", speed);

        // Hareket yönüne döndür
        if (rotateTowardsMovement && delta.magnitude > 0.01f)
        {
            Vector3 direction = delta.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        lastPosition = transform.position;
    }
}
