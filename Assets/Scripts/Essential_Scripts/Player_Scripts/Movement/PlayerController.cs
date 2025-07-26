using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarlar�")]
    public float moveSpeed = 5f;

    public Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A / D
        float vertical = Input.GetAxisRaw("Vertical");     // W / S

        moveInput = new Vector3(horizontal, 0f, vertical).normalized;
        Debug.Log(moveInput);
    }

    void FixedUpdate()
    {
        // D�nya y�n�nde hareket (kamera ba��ms�z)
        Vector3 moveVelocity = moveInput * moveSpeed;
        Vector3 currentVelocity = rb.linearVelocity;

        // Sadece yatay hareket uygula, d��ey hareketi (yer�ekimini) koru
        rb.linearVelocity = new Vector3(moveVelocity.x, currentVelocity.y, moveVelocity.z);
    }
}
