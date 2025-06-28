using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;          // Döndürmek istediðin obje (örneðin odak noktasý)
    public float rotationSpeed = 5f;  // Döndürme hýzý
    public float distance = 5f;       // Objeye olan mesafe

    private float yaw = 0f;  // Yatay açý
    private float pitch = 0f; // Dikey açý

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target atanmadý!");
            enabled = false;
            return;
        }

        // Baþlangýç pozisyonu
        Vector3 direction = new Vector3(0, 0, -distance);
        transform.position = target.position + direction;
        transform.LookAt(target);
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) // Sað týk basýlý mý
        {
            // Fare hareketi
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

            // Pitch açýsýný sýnýrla (mesela -80 ile 80 derece arasý)
            pitch = Mathf.Clamp(pitch, -80f, 80f);

            // Hesaplanan açýlarla yeni pozisyonu ayarla
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 offset = rotation * new Vector3(0, 0, -distance);

            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
