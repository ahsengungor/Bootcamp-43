using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [Header("Movement Variables")]
    public float horizontal;
    public float vertical;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovementController();
    }
    void MovementController()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        float speed = 0.2f;

        Vector3 moveVector = new Vector3(horizontal, 0f, vertical);
        _rb.AddForce(moveVector * speed , ForceMode.Impulse);
        Debug.Log($"MoveVector: {moveVector}, Velocity: {_rb.linearVelocity}");
    }
}
